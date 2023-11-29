namespace EasyPlc.Application;

/// <inheritdoc cref="IResourceService"/>
public class PlcResourceService : DbRepository<PlcResource>, IPlcResourceService
{
    private readonly ILogger<PlcResourceService> _logger;
    private readonly ISimpleCacheService _simpleCacheService;

    public PlcResourceService(
        ILogger<PlcResourceService> logger,
        ISimpleCacheService simpleCacheService
        )
    {
        _logger = logger;
        _simpleCacheService = simpleCacheService;
    }
    public async Task<SqlSugarPagedList<PlcResource>> Page(PlcResourcePageInput input)
    {
        var list = await GetListAsync();
        var pIds = list.Select(x => x.ParentId).ToList();
        var query = Context.Queryable<PlcResource>()
            .WhereIF(pIds.Contains(input.ParentId), it => it.ParentId == input.ParentId)
            .WhereIF(!pIds.Contains(input.ParentId), it => it.Id == input.ParentId)
            .WhereIF(!string.IsNullOrEmpty(input.SearchKey), it => it.Title.Contains(input.SearchKey))//根据关键字查询
            .OrderByIF(!string.IsNullOrEmpty(input.SortField), $"{input.SortField} {input.SortOrder}")
            .OrderBy(it => it.SortCode);//排序
        var pageInfo = await query.ToPagedListAsync(input.Current, input.Size);//分页
        return pageInfo;
    }
    public async Task<List<PlcResource>> Tree(List<long> resourceIds = null, PlcResourceTreeInput treeInput = null, bool isContainOneself = true)
    {
        long parentId = EasyPlcConst.Zero;//父级ID
        //获取所有PLC资源
        var list = await GetListAsync();
        var treeList = new List<PlcResource>();
        if (resourceIds != null)
            treeList = GetParentListByIds(list, resourceIds);//如果PLC资源ID不为空则获取PLC资源ID列表的所有父节点
        //如果选择器ID不为空则表示是懒加载,只加载子节点
        if (treeInput != null && treeInput.ParentId != null)
        {
            parentId = treeInput.ParentId.Value;
            treeList = GetPlcResourceChilden(list, treeInput.ParentId.Value);//获取下级
            if (isContainOneself)//如果包含自己
            {
                //获取自己的PLC信息
                var self = list.Where(it => it.Id == parentId).FirstOrDefault();
                if (self != null)
                {
                    parentId = self.ParentId.Value;
                    treeList.Insert(0, self);
                }//如果PLC不为空就插到第一个
            }
        }
        treeList = treeList.OrderBy(it => it.SortCode).ToList();//排序
        //构建PLC树
        var result = ConstrucTrees(treeList, parentId);
        return result;
    }
    /// <inheritdoc />
    public async Task<List<string>> GetCodeByIds(List<long> ids, string category)
    {
        //根据分类获取所有
        var plcResources = await GetListByCategory(category);
        //条件查询
        var result = plcResources.Where(it => ids.Contains(it.Id)).Select(it => it.Code).ToList();
        return result;
    }

    public async Task<PlcResource> GetResurceById(long id)
    {
        //获取所有型号
        var resources = await GetListAsync();
        return resources.Where(it => it.Id == id).FirstOrDefault();
    }
    /// <inheritdoc/>
    public async Task<List<PlcResource>> GetListAsync(List<string> categoryList = null)
    {
        //定义结果
        var plcResources = new List<PlcResource>();

        //定义资源分类列表,如果是空的则获取全部资源
        categoryList = categoryList != null ? categoryList : new List<string> {
            CateGoryConst.Resource_BaseData,
            CateGoryConst.Resource_StructData,
            CateGoryConst.Resource_ArrData
        };
        //遍历列表
        foreach (var category in categoryList)
        {
            //根据分类获取到资源列表
            var data = await GetListByCategory(category);
            //添加到结果集
            plcResources.AddRange(data);
        }
        return plcResources;
    }
    public async Task<List<PlcResource>> GetListBySortCodeAsync()
    {
        //获取所有型号
        var resources = await GetListAsync();
        //排序
        resources = resources.OrderBy(it => it.ParentId).OrderBy(it => it.SortCode).ToList();
        return resources;
    }

    //public async Task<List<PlcResource>> GetListContainAddr()
    //{

    //}

    public async Task<int> GetLenghAsync(long id)
    {
        int lengh = 0;
        var childList = await GetChildListById(id, false, false);
        foreach (var child in childList)
        {
            if (child.Category == "BASEDATA")
            {
                lengh += child.ByteCount;
            }
            if (child.Category == "STRUCTDATA")
            {

                lengh += await GetLenghAsync(child.Id);
            }
            if (child.Category == "ARRDATA")
            {

                lengh += await GetLenghAsync(child.Id);

            }
        }
        return lengh;
    }

    public async Task<int> GetLenghOneAsync(long id)
    {
        int lengh = 0;
        var plcResour = await GetResurceById(id);

        if (plcResour.Category == "BASEDATA")
        {
            lengh = plcResour.ByteCount;
        }
        if (plcResour.Category == "ARRDATA")
        {
           
        }
        if (plcResour.Category == "STRUCTDATA")
        {

        }
        return lengh;
    }

    /// <inheritdoc/>
    public async Task RefreshCache(string category = null)
    {
        //如果分类是空的
        if (category == null)
        {
            var listCategory = new List<string> {
                CateGoryConst.Resource_BaseData,
                CateGoryConst.Resource_StructData,
                CateGoryConst.Resource_ArrData
            };
            listCategory.ForEach(c =>
            {
                _simpleCacheService.Remove(CacheConst.Cache_PlcResource + c);
            });
            ////删除全部key
            //_simpleCacheService.DelByPattern(CacheConst.Cache_PlcResource);
            await GetListAsync();
        }
        else
        {
            //否则只删除一个Key
            _simpleCacheService.Remove(CacheConst.Cache_PlcResource + category);
            await GetListByCategory(category);
        }
    }

    /// <inheritdoc />
    public async Task<List<PlcResource>> GetChildListById(long resId, bool isContainOneself = true, bool depth = true)
    {
        //获取所有资源
        var plcResources = await GetListAsync();
        //查找下级
        var childLsit = GetResourceChilden(plcResources, resId, depth);
        if (isContainOneself)//如果包含自己
        {
            //获取自己的资源信息
            var self = plcResources.Where(it => it.Id == resId).FirstOrDefault();
            if (self != null) childLsit.Insert(0, self);//如果资源不为空就插到第一个
        }
        return childLsit;
    }

    /// <inheritdoc />
    public List<PlcResource> GetChildListById(List<PlcResource> plcResources, long resId, bool isContainOneself = true)
    {
        //查找下级
        var childLsit = GetResourceChilden(plcResources, resId);
        if (isContainOneself)//如果包含自己
        {
            //获取自己的资源信息
            var self = plcResources.Where(it => it.Id == resId).FirstOrDefault();
            if (self != null) childLsit.Insert(0, self);//如果资源不为空就插到第一个
        }
        return childLsit;
    }

    public async Task<List<PlcResource>> GetBrotherListById(long resId, bool isContainOneself = true)
    {
        //获取所有资源
        var plcResources = await GetListAsync();
        //查找下级
        var brotherLsit = GetResourceBrothers(plcResources, resId);
        if (!isContainOneself)//如果不包含自己
        {
            //获取自己的资源信息
            var self = plcResources.Where(it => it.Id == resId).FirstOrDefault();
            if (self != null) brotherLsit.Remove(self);//把自己去掉
        }
        return brotherLsit;
    }

    public List<PlcResource> GetBrotherListById(List<PlcResource> plcResources, long resId, bool isContainOneself = true)
    {
        //查找下级
        var brotherLsit = GetResourceBrothers(plcResources, resId);
        if (!isContainOneself)//如果不包含自己
        {
            //获取自己的信息
            var self = plcResources.Where(it => it.Id == resId).FirstOrDefault();
            if (self != null) brotherLsit.Remove(self);//把自己去掉
        }
        return brotherLsit;
    }


    /// <inheritdoc />

    public async Task<List<PlcResource>> GetListByCategory(string category)
    {
        //先从Redis拿
        var plcResources = _simpleCacheService.Get<List<PlcResource>>(CacheConst.Cache_PlcResource + category);
        if (plcResources == null)
        {
            //redis没有就去数据库拿
            plcResources = await base.GetListAsync(it => it.Category == category);
            if (plcResources.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(CacheConst.Cache_PlcResource + category, plcResources);
            }
        }
        return plcResources;
    }

    public async Task Copy(PlcResourceCopyInput input)
    {
        await CheckInput(input);

        var resourceList = await GetListAsync();//获取所有
        var ids = new HashSet<long>();//定义不重复Id集合
        var addResourceList = new List<PlcResource>();//添加资源列表
        var alreadyIds = new HashSet<long>();//定义已经复制过得资源Id
        ids.AddRange(input.Ids);//加到集合
        if (ids.Contains(input.TargetId))
            throw Oops.Bah($"不能包含自己");
        //获取目标资源
        var target = resourceList.Where(it => it.Id == input.TargetId).FirstOrDefault();
        if (target != null || input.TargetId == EasyPlcConst.Zero)
        {
            //需要复制的资源编码列表
            var orgCodes = resourceList.Where(it => ids.Contains(it.Id)).Select(it => it.Code).ToList();
            //目标资源的一级子资源名称列表
            var targetChildCodes = resourceList.Where(it => it.ParentId == input.TargetId).Select(it => it.Code).ToList();
            orgCodes.ForEach(it =>
            {
                if (targetChildCodes.Contains(it)) throw Oops.Bah($"已存在{it}");
            });

            foreach (var id in input.Ids)
            {
                var res = resourceList.Where(o => o.Id == id).FirstOrDefault();//获取下级
                if (res != null && !alreadyIds.Contains(id))
                {
                    alreadyIds.Add(id);//添加到已复制列表
                    RedirectPlcResource(res);//生成新的实体
                    res.ParentId = input.TargetId;//父id为目标Id
                    addResourceList.Add(res);
                    //是否包含下级
                    if (input.ContainsChild)
                    {
                        var childIds = await GetResouceChildIds(id, false);//获取下级id列表
                        alreadyIds.AddRange(childIds);//添加到已复制id
                        var childList = resourceList.Where(c => childIds.Contains(c.Id)).ToList();//获取下级
                        var addResources = CopyPlcResourceChilden(childList, id, res.Id);//赋值下级资源
                        addResourceList.AddRange(addResources);
                    }
                }
            }

            if (await InsertRangeAsync(addResourceList))//插入数据
                await RefreshCache();//刷新缓存
        }
    }
    public async Task ChangedNameCopy(PlcResourceCopyInput input)
    {
        await CheckInput(input);

        var resourceList = await GetListAsync();//获取所有
        var ids = new HashSet<long>();//定义不重复Id集合
        var addResourceList = new List<PlcResource>();//添加资源列表
        var alreadyIds = new HashSet<long>();//定义已经复制过得资源Id
        ids.AddRange(input.Ids);//加到集合
        if (ids.Contains(input.TargetId))
            throw Oops.Bah($"不能包含自己");
        //获取目标资源
        var target = resourceList.Where(it => it.Id == input.TargetId).FirstOrDefault();
        if (target != null || input.TargetId == EasyPlcConst.Zero)
        {
            int count = input.len == 0 ? 1 : input.len;
            for (int i = 0; i < count; i++)
            {
                if (input.Ids.Any(it => it < 0))
                {
                    //基础对象 -1 bool -2 int16 -3 int32 -4 float -5 string -6 wstring
                    var vt = string.Empty;
                    var code = string.Empty;
                    switch(input.Ids[0])
                    {
                        case -1:
                            vt = "Bool";
                            code = "B";
                            break;
                        case -2:
                            vt = "Int16";
                            code = "S";
                            break;
                        case -3:
                            vt = "Int32";
                            code = "I";
                            break;
                        case -4:
                            vt = "Float";
                            code = "F";
                            break;
                        case -5:
                            vt = "String";
                            code = "Str";
                            break;
                        case -6:
                            vt = "WString";
                            code = "WStr";
                            break;
                    }
                    
                    var r = new PlcResource();

                    r.Id = CommonUtils.GetSingleId();
                    r.ParentId = input.TargetId;
                    r.Title = $"[{i}]";
                    r.Code = $"{code}{i}";
                    r.ValueType = vt;
                    r.Category = "BASEDATA";
                    r.ValueLength = (int)input.Ids[1];
                    r.SortCode = i + 1;
                    addResourceList.Add(r);
                    
                }
                else
                {
                    alreadyIds.Clear();
                    var suffix = input.len == 0 ? "_Copy" : $"[{i}]";//针对于数组特殊处理

                    //需要复制的资源编码列表
                    var orgCodes = resourceList.Where(it => ids.Contains(it.Id)).Select(it => it.Code).ToList();
                    //目标资源的一级子资源名称列表
                    var targetChildCodes = resourceList.Where(it => it.ParentId == input.TargetId).Select(it => it.Code + suffix).ToList();
                    orgCodes.ForEach(it =>
                    {
                        if (targetChildCodes.Contains(it)) throw Oops.Bah($"已存在{it}");
                    });
                
                    //结构体对象
                    foreach (var id in input.Ids)
                    {
                        var res = resourceList.Where(o => o.Id == id).FirstOrDefault().DeepClone();//获取下级
                        if (res != null && !alreadyIds.Contains(id))
                        {
                            alreadyIds.Add(id);//添加到已复制列表
                            RedirectPlcResourceChangedName(res, null, suffix);//生成新的实体  只有顶级需要加 _Copy
                            res.ParentId = input.TargetId;//父id为目标Id
                            if(suffix.Contains("[")) res.SortCode = i + 1;
                            addResourceList.Add(res);
                            //是否包含下级
                            if (input.ContainsChild)
                            {
                                var childIds = await GetResouceChildIds(id, false);//获取下级id列表
                                alreadyIds.AddRange(childIds);//添加到已复制id
                                var childList = resourceList.Where(c => childIds.Contains(c.Id)).ToList();//获取下级
                                var addResources = CopyPlcResourceChilden(childList, id, res.Id);//赋值下级资源
                                addResourceList.AddRange(addResources);
                            }
                        }
                    }
                }
            }
            if (await InsertRangeAsync(addResourceList))//插入数据
                await RefreshCache();//刷新缓存
        }
    }
    public async Task<List<PlcResource>> GetCopy(PlcResourceCopyInput input)
    {
        var resourceList = await GetListAsync();//获取所有
        var ids = new HashSet<long>();//定义不重复Id集合
        var addResourceList = new List<PlcResource>();//添加资源列表
        var alreadyIds = new HashSet<long>();//定义已经复制过得资源Id
        ids.AddRange(input.Ids);//加到集合
        if (ids.Contains(input.TargetId))
            throw Oops.Bah($"不能包含自己");

        foreach (var id in input.Ids)
        {
            var res = resourceList.Where(o => o.Id == id).FirstOrDefault();//获取下级
            if (res != null && !alreadyIds.Contains(id))
            {
                alreadyIds.Add(id);//添加到已复制列表
                RedirectPlcResource(res);//生成新的实体
                res.ParentId = input.TargetId;//父id为目标Id
                var sps = input.StartAddr.Split('.');
                res.StartAdrr = $"{sps[0]}.{sps[1].ToInt() + input.len}";
                input.len += await GetLenghOneAsync(id);//长度增加
                addResourceList.Add(res);
                //是否包含下级
                if (input.ContainsChild)
                {
                    var childIds = await GetResouceChildIds(id, false);//获取下级id列表
                    alreadyIds.AddRange(childIds);//添加到已复制id
                    var childList = resourceList.Where(c => childIds.Contains(c.Id)).OrderBy(it => it.SortCode).ToList();//获取下级
                    var addResources = await CopyPlcResourceChildenAndLen(input, childList, id, res.Id);//赋值下级资源
                    addResourceList.AddRange(addResources);
                }
            }

        }
        return addResourceList;
    }
    public async Task Cut(PlcResourceCopyInput input)
    {
        await CheckInput(input);

        var resourceList = await GetListAsync();//获取所有
        var ids = new HashSet<long>();//定义不重复Id集合
        var addResourceList = new List<PlcResource>();//添加资源列表
        var alreadyIds = new HashSet<long>();//定义已经复制过得资源Id
        ids.AddRange(input.Ids);//加到集合
        if (ids.Contains(input.TargetId))
            throw Oops.Bah($"不能包含自己");
        //获取目标资源
        var target = resourceList.Where(it => it.Id == input.TargetId).FirstOrDefault();
        if (target != null || input.TargetId == EasyPlcConst.Zero)
        {
            //需要复制的资源编码列表
            var rCodes = resourceList.Where(it => ids.Contains(it.Id)).Select(it => it.Code).ToList();
            //目标资源的一级子资源名称列表
            var targetChildCodes = resourceList.Where(it => it.ParentId == input.TargetId).Select(it => it.Code).ToList();
            rCodes.ForEach(it =>
            {
                if (targetChildCodes.Contains(it)) throw Oops.Bah($"已存在{it}");
            });

            foreach (var id in input.Ids)
            {
                var res = resourceList.Where(o => o.Id == id).FirstOrDefault();//获取下级
                if (res != null && !alreadyIds.Contains(id))
                {
                    alreadyIds.Add(id);//添加到已复制列表
                    RedirectPlcResource(res);//生成新的实体
                    res.ParentId = input.TargetId;//父id为目标Id
                    addResourceList.Add(res);
                    //是否包含下级
                    if (input.ContainsChild)
                    {
                        var childIds = await GetResouceChildIds(id, false);//获取下级id列表
                        alreadyIds.AddRange(childIds);//添加到已复制id
                        var childList = resourceList.Where(c => childIds.Contains(c.Id)).ToList();//获取下级
                        var addResources = CopyPlcResourceChilden(childList, id, res.Id);//赋值下级资源
                        addResourceList.AddRange(addResources);
                    }
                }
            }
            //事务
            var result = await itenant.UseTranAsync(async () =>
            {
                //删除原有的
                var deleteIds = new List<long>();
                foreach (var id in input.Ids)
                {
                    deleteIds.AddRange((await GetChildListById(id)).Select(it => it.Id).ToList());
                }
                await base.DeleteByIdAsync(deleteIds.Cast<object>().ToArray());
                //插入指定位置
                await InsertRangeAsync(addResourceList);//插入数据
            });
            if (result.IsSuccess)
            {
                await RefreshCache();//刷新缓存
            }
            else
            {
                //写日志
                _logger.LogError(result.ErrorMessage, result.ErrorException);
                throw Oops.Oh(ErrorCodeEnum.A0002);
            }
        }
    }

    #region 方法

    /// <summary>
    /// 重新生成资源实体
    /// </summary>
    /// <param orgName="org"></param>
    private void RedirectPlcResource(PlcResource res, long? newId = null)
    {
        //重新生成ID并赋值
        if (newId == null) newId = CommonUtils.GetSingleId();
        res.Id = newId.Value;
        res.CreateTime = DateTime.Now;
        res.CreateUser = UserManager.UserAccount;
        res.CreateUserId = UserManager.UserId;
    }


    /// <summary>
    /// 重命名重新生成资源实体
    /// </summary>
    /// <param orgName="org"></param>
    private void RedirectPlcResourceChangedName(PlcResource res, long? newId = null, string suffix = "_Copy")
    {
        //重新生成ID并赋值
        if (newId == null) newId = CommonUtils.GetSingleId();
        res.Id = newId.Value;
        res.CreateTime = DateTime.Now;
        res.CreateUser = UserManager.UserAccount;
        res.CreateUserId = UserManager.UserId;
        //重命名+_Copy
        //res.Title += suffix;
        res.Code += suffix;

        res.SortCode = 9999;//复制的都放在最后面
    }


    public async Task<List<long>> GetResouceChildIds(long resId, bool isContainOneself = true)
    {
        var orgIds = new List<long>();//资源列表
        if (resId > 0)//如果orgid有值
        {
            //获取所有子集
            var sysOrgs = await GetChildListById(resId, isContainOneself);
            orgIds = sysOrgs.Select(x => x.Id).ToList();//提取ID列表
        }
        return orgIds;
    }

    /// <summary>
    /// 赋值资源的所有下级
    /// </summary>
    /// <param orgName="orgList">资源列表</param>
    /// <param orgName="parentId">父Id</param>
    /// <param orgName="newParentId">新父Id</param>
    /// <returns></returns>
    public List<PlcResource> CopyPlcResourceChilden(List<PlcResource> resList, long parentId, long newParentId)
    {
        //找下级资源列表
        var resources = resList.Where(it => it.ParentId == parentId).ToList().DeepClone();
        if (resources.Count > 0)//如果数量大于0
        {
            var data = new List<PlcResource>();
            foreach (var item in resources)//遍历资源
            {
                long id = item.Id;

                RedirectPlcResource(item);//实体重新赋值
                item.ParentId = newParentId;//赋值父Id
                data.Add(item);//添加到列表

                var childen = CopyPlcResourceChilden(resList, id, item.Id);//获取子节点
                data.AddRange(childen);//添加子节点);
            }
            return data;//返回结果
        }
        return new List<PlcResource>();
    }

    public async Task<List<PlcResource>> CopyPlcResourceChildenAndLen(PlcResourceCopyInput input, List<PlcResource> resList, long parentId, long newParentId)
    {
        //找下级资源列表
        var resources = resList.Where(it => it.ParentId == parentId).ToList();
        if (resources.Count > 0)//如果数量大于0
        {
            var data = new List<PlcResource>();
            foreach (var item in resources)//遍历资源
            {
                long id = item.Id;
                RedirectPlcResource(item);//实体重新赋值
                item.ParentId = newParentId;//赋值父Id
                var sps = input.StartAddr.Split('.');
                item.StartAdrr = $"{sps[0]}.{sps[1].ToInt() + input.len}";
                //地址对其
                var y = input.len % 2;
                if (y > 0) input.len += 2 - y;
                input.len += await GetLenghOneAsync(id);//计算
                data.Add(item);//添加到列表

                var childen = await CopyPlcResourceChildenAndLen(input, resList, id, item.Id);//获取子节点
                data.AddRange(childen);//添加子节点);
            }
            return data;//返回结果
        }
        return new List<PlcResource>();
    }

    /// <summary>
    /// 获取资源所有下级
    /// </summary>
    /// <param name="resourceList">资源列表</param>
    /// <param name="parentId">父ID</param>
    /// <returns></returns>
    public List<PlcResource> GetResourceChilden(List<PlcResource> resourceList, long parentId, bool depth = true)
    {
        //找下级资源ID列表
        var resources = resourceList.Where(it => it.ParentId == parentId).OrderBy(it => it.SortCode).ToList();
        if (resources.Count > 0)//如果数量大于0
        {
            var data = new List<PlcResource>();
            foreach (var item in resources)//遍历资源
            {
                data.Add(item);//添加到列表
                if (depth)
                {
                    var res = GetResourceChilden(resourceList, item.Id);
                    data.AddRange(res);//添加子节点;
                }
            }
            return data;//返回结果
        }
        return new List<PlcResource>();
    }

    /// <summary>
    /// 获取上级
    /// </summary>
    /// <param name="resourceList"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    public List<PlcResource> GetResourceParent(List<PlcResource> resourceList, long parentId)
    {
        //找上级资源ID列表
        var resources = resourceList.Where(it => it.Id == parentId).FirstOrDefault();
        if (resources != null)//如果数量大于0
        {
            var data = new List<PlcResource>();
            var parents = GetResourceParent(resourceList, resources.ParentId.Value);
            data.AddRange(parents);//添加子节点;
            data.Add(resources);//添加到列表
            return data;//返回结果
        }
        return new List<PlcResource>();
    }
    /// <summary>
    /// 查找同级以及父级的同级
    /// </summary>
    /// <param name="resourceList"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    public List<PlcResource> GetResourceBrothers(List<PlcResource> resourceList, long parentId)
    {
        var data = new List<PlcResource>();

        //找上级
        var parents = GetResourceParent(resourceList, parentId);

        data.AddRange(parents);
        foreach (var item in parents)
        {
            //通过父级查找对应子集
            var brothers = resourceList.Where(it => it.ParentId == item.Id).ToList();
            data.AddRange(brothers);
        }

        //找下级
        var chidrens = GetResourceChilden(resourceList, parentId);
        data.AddRange(chidrens);
        return data;
    }

    private async Task CheckInput(PlcResourceCopyInput input)
    {
        if (input.TargetId != 0)
        {
            //拷贝的目标对象必须是结构或者空数组
            var resource = await GetResurceById(input.TargetId);
            if (resource == null) throw Oops.Bah($"目标对象不存在{input.TargetId}");
            if (resource.Category == CateGoryConst.Resource_ArrData)
            {
                //是否有子集
                var childrens = await GetChildListById(input.TargetId, false);
                if (childrens.Count > 0) throw Oops.Bah($"目标数组结构已有子集");
            }
            if (resource.Category == CateGoryConst.Resource_BaseData) throw Oops.Bah($"基础类型数据不能添加子集");
        }
    }

    /// <summary>
    /// 根据PLCId列表获取所有父级PLC
    /// </summary>
    /// <param name="allPlcList"></param>
    /// <param name="PlcIds"></param>
    /// <returns></returns>
    public List<PlcResource> GetParentListByIds(List<PlcResource> allPlcList, List<long> PlcIds)
    {
        var resource = new HashSet<PlcResource>();//结果列表
        //遍历PLCID
        PlcIds.ForEach(it =>
        {
            //获取该PLCID的所有父级
            var parents = GetPlcParents(allPlcList, it);
            resource.AddRange(parents);//添加到结果
        });
        return resource.ToList();
    }
    public List<PlcResource> GetPlcParents(List<PlcResource> allPlcList, long PlcId, bool includeSelf = true)
    {
        //找到PLC
        var resource = allPlcList.Where(it => it.Id == PlcId).FirstOrDefault();
        if (resource != null)//如果PLC不为空
        {
            var data = new List<PlcResource>();
            var parents = GetPlcParents(allPlcList, resource.ParentId ?? 0, includeSelf);//递归获取父节点
            data.AddRange(parents);//添加父节点;
            if (includeSelf)
                data.Add(resource);//添加到列表
            return data;//返回结果
        }
        return new List<PlcResource>();
    }

    /// <summary>
    /// 获取PLC所有下级
    /// </summary>
    /// <param name="PlcList"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    public List<PlcResource> GetPlcResourceChilden(List<PlcResource> resourceList, long parentId)
    {
        //找下级PLCID列表
        var list = resourceList.Where(it => it.ParentId == parentId).ToList();
        if (list.Count > 0)//如果数量大于0
        {
            var data = new List<PlcResource>();
            foreach (var item in list)//遍历PLC
            {
                var childen = GetPlcResourceChilden(resourceList, item.Id);//获取子节点
                data.AddRange(childen);//添加子节点);
                data.Add(item);//添加到列表
            }
            return data;//返回结果
        }
        return new List<PlcResource>();
    }

    public List<PlcResource> ConstrucTrees(List<PlcResource> resourceList, long parentId = 0)
    {
        //找下级字典ID列表
        var list = resourceList.Where(it => it.ParentId == parentId).OrderBy(it => it.SortCode).ToList();
        if (list.Count > 0)//如果数量大于0
        {
            var data = new List<PlcResource>();
            foreach (var item in list)//遍历字典
            {
                item.Children = ConstrucTrees(resourceList, item.Id);//添加子节点
                data.Add(item);//添加到列表
            }
            return data;//返回结果
        }
        return new List<PlcResource>();
    }

    #endregion 方法
}