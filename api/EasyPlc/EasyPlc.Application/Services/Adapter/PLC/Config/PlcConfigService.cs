namespace EasyPlc.Application;

/// <inheritdoc cref="IPlcConfigService"/>
public class PlcConfigService : DbRepository<PlcConfig>, IPlcConfigService
{
    private readonly ISimpleCacheService _simpleCacheService;
    private readonly IImportExportService _importExportService;

    public PlcConfigService(ISimpleCacheService simpleCacheService, IImportExportService importExportService)
    {
        _simpleCacheService = simpleCacheService;
        _importExportService = importExportService;
    }

    #region 查询

    /// <inheritdoc />
    public override async Task<List<PlcConfig>> GetListAsync()
    {
        //先从Redis拿
        var sysPlcs = _simpleCacheService.Get<List<PlcConfig>>(CacheConst.Cache_PlcConfig);
        if (sysPlcs == null)
        {
            //redis没有就去数据库拿
            sysPlcs = await base.GetListAsync();
            if (sysPlcs.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(CacheConst.Cache_PlcConfig, sysPlcs);
            }
        }
        return sysPlcs;
    }
    public async Task<List<PlcConfig>> GetListBySortCodeAsync()
    {
        //获取所有PLC
        var plcs = await GetListAsync();
        //排序
        plcs = plcs
            .OrderBy(it => it.Category)
            .OrderBy(it => it.SortCode)
            .ToList();
        return plcs;
    }

    /// <inheritdoc />
    public async Task<PlcConfig> GetPlcConfigById(long id)
    {
        var sysPlc = await GetListAsync();
        var result = sysPlc.Where(it => it.Id == id).FirstOrDefault();
        return result;
    }

    /// <inheritdoc />
    public async Task<List<PlcConfig>> GetChildListById(long PlcId, bool isContainOneself = true)
    {
        //获取所有PLC
        var sysPlcs = await GetListAsync();
        //查找下级
        var childLsit = GetPlcConfigChilden(sysPlcs, PlcId);
        if (isContainOneself)//如果包含自己
        {
            //获取自己的PLC信息
            var self = sysPlcs.Where(it => it.Id == PlcId).FirstOrDefault();
            if (self != null) childLsit.Insert(0, self);//如果PLC不为空就插到第一个
        }
        return childLsit;
    }

    /// <inheritdoc />
    public async Task<List<long>> GetPlcChildIds(long PlcId, bool isContainOneself = true)
    {
        var PlcIds = new List<long>();//PLC列表
        if (PlcId > 0)//如果Plcid有值
        {
            //获取所有子集
            var sysPlcs = await GetChildListById(PlcId, isContainOneself);
            PlcIds = sysPlcs.Select(x => x.Id).ToList();//提取ID列表
        }
        return PlcIds;
    }

    /// <inheritdoc/>
    public async Task<SqlSugarPagedList<PlcConfig>> Page(PlcConfigPageInput input)
    {
        var query = Context.Queryable<PlcConfig>()
            .WhereIF(input.ParentId > 0, it => it.ParentId == input.ParentId)//父级
            .WhereIF(!string.IsNullOrEmpty(input.SearchKey), it => it.Name.Contains(input.SearchKey))//根据关键字查询
            .OrderByIF(!string.IsNullOrEmpty(input.SortField), $"{input.SortField} {input.SortOrder}")
            .OrderBy(it => it.SortCode);//排序
        var pageInfo = await query.ToPagedListAsync(input.Current, input.Size);//分页
        return pageInfo;
    }

    /// <inheritdoc />
    public async Task<List<PlcConfig>> Tree(List<long> PlcIds = null, PlcConfigTreeInput treeInput = null)
    {
        long parentId = EasyPlcConst.Zero;//父级ID
        //获取所有PLC
        var sysPlcs = await GetListAsync();
        if (PlcIds != null)
            sysPlcs = GetParentListByIds(sysPlcs, PlcIds);//如果PLCID不为空则获取PLCID列表的所有父节点
        //如果选择器ID不为空则表示是懒加载,只加载子节点
        if (treeInput != null && treeInput.ParentId != null)
        {
            parentId = treeInput.ParentId.Value;
            sysPlcs = GetPlcConfigChildenLazy(sysPlcs, treeInput.ParentId.Value);//获取懒加载下级
        }
        sysPlcs = sysPlcs.OrderBy(it => it.SortCode).ToList();//排序
        //构建PLC树
        var result = ConstrucTrees(sysPlcs, parentId);
        return result;
    }

    /// <inheritdoc />
    public async Task<PlcConfig> Detail(BaseIdInput input)
    {
        var sysPlcs = await GetListAsync();
        var PlcDetail = sysPlcs.Where(it => it.Id == input.Id).FirstOrDefault();
        return PlcDetail;
    }

    /// <inheritdoc />
    public List<PlcConfig> GetPlcParents(List<PlcConfig> allPlcList, long PlcId, bool includeSelf = true)
    {
        //找到PLC
        var sysPlcs = allPlcList.Where(it => it.Id == PlcId).FirstOrDefault();
        if (sysPlcs != null)//如果PLC不为空
        {
            var data = new List<PlcConfig>();
            var parents = GetPlcParents(allPlcList, sysPlcs.ParentId, includeSelf);//递归获取父节点
            data.AddRange(parents);//添加父节点;
            if (includeSelf)
                data.Add(sysPlcs);//添加到列表
            return data;//返回结果
        }
        return new List<PlcConfig>();
    }

    /// <inheritdoc />
    public bool IsExistPlcByName(List<PlcConfig> sysPlcs, string PlcName, long parentId, out long PlcId)
    {
        PlcId = 0;
        var sysPlc = sysPlcs.Where(it => it.ParentId == parentId && it.Name == PlcName).FirstOrDefault();
        if (sysPlc != null)
        {
            PlcId = sysPlc.Id;
            return true;
        }
        else
            return false;
    }

    #endregion 查询

    #region 新增

    /// <inheritdoc />
    public async Task Add(PlcConfigAddInput input, string name = EasyPlcConst.PlcConfig)
    {
        await CheckInput(input, name);//检查参数
        var sysPlc = input.Adapt<PlcConfig>();//实体转换
        sysPlc.Code = RandomHelper.CreateRandomString(10);//赋值Code
        if (await InsertAsync(sysPlc))//插入数据
            await RefreshCache();//刷新缓存
    }

    public async Task<long> AddReturnSnowflakeId(PlcConfigAddInput input, string name = EasyPlcConst.PlcConfig)
    {
        await CheckInput(input, name);//检查参数
        var sysPlc = input.Adapt<PlcConfig>();//实体转换
        sysPlc.Code = RandomHelper.CreateRandomString(10);//赋值Code
        var id = (await InsertReturnEntityAsync(sysPlc)).Id;//插入数据
        await RefreshCache();//刷新缓存
        return id;
    }

    /// <inheritdoc />
    public async Task Copy(PlcConfigCopyInput input)
    {
        var PlcList = await GetListAsync();//获取所有
        var ids = new HashSet<long>();//定义不重复Id集合
        var addPlcList = new List<PlcConfig>();//添加机构列表
        var alreadyIds = new HashSet<long>();//定义已经复制过得PLCId
        ids.AddRange(input.Ids);//加到集合
        if (ids.Contains(input.TargetId))
            throw Oops.Bah($"不能包含自己");
        //获取目标PLC
        var target = PlcList.Where(it => it.Id == input.TargetId).FirstOrDefault();
        if (target != null || input.TargetId == EasyPlcConst.Zero)
        {
            //需要复制的PLC名称列表
            var PlcNames = PlcList.Where(it => ids.Contains(it.Id)).Select(it => it.Name).ToList();
            //目标PLC的一级子PLC名称列表
            var targetChildNames = PlcList.Where(it => it.ParentId == input.TargetId).Select(it => it.Name).ToList();
            PlcNames.ForEach(it =>
            {
                if (targetChildNames.Contains(it)) throw Oops.Bah($"已存在{it}");
            });

            foreach (var id in input.Ids)
            {
                var Plc = PlcList.Where(o => o.Id == id).FirstOrDefault();//获取下级
                if (Plc != null && !alreadyIds.Contains(id))
                {
                    alreadyIds.Add(id);//添加到已复制列表
                    RedirectPlc(Plc);//生成新的实体
                    Plc.ParentId = input.TargetId;//父id为目标Id
                    addPlcList.Add(Plc);
                    //是否包含下级
                    if (input.ContainsChild)
                    {
                        var childIds = await GetPlcChildIds(id, false);//获取下级id列表
                        alreadyIds.AddRange(childIds);//添加到已复制id
                        var childList = PlcList.Where(c => childIds.Contains(c.Id)).ToList();//获取下级
                        var addPlcs = CopyPlcConfigChilden(childList, id, Plc.Id);//赋值下级PLC
                        addPlcList.AddRange(addPlcs);
                    }
                }
            }
            //遍历机构重新赋值全称
            addPlcList.ForEach(it =>
            {
                it.Names = it.ParentId == EasyPlcConst.Zero ? it.Name : GetNames(PlcList, it.ParentId, it.Name);
            });

            if (await InsertRangeAsync(addPlcList))//插入数据
                await RefreshCache();//刷新缓存
        }
    }

    #endregion 新增

    #region 编辑

    /// <inheritdoc />
    public async Task Edit(PlcConfigEditInput input, string name = EasyPlcConst.PlcConfig)
    {
        await CheckInput(input, name);//检查参数
        var sysPlc = input.Adapt<PlcConfig>();//实体转换
        if (await UpdateAsync(sysPlc))//更新数据
            await RefreshCache();//刷新缓存
    }

    #endregion 编辑

    #region 删除

    /// <inheritdoc />
    public async Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.PlcConfig)
    {
        //获取所有ID
        var ids = input.Select(it => it.Id).ToList();
        if (ids.Count > 0)
        {
            var sysPlcs = await GetListAsync();//获取所有PLC
            var sysDeletePlcList = new List<long>();//需要删除的PLCID集合
            ids.ForEach(it =>
            {
                var childen = GetPlcConfigChilden(sysPlcs, it);//查找下级PLC
                sysDeletePlcList.AddRange(childen.Select(it => it.Id).ToList());
                sysDeletePlcList.Add(it);
            });
            //删除PLC
            if (await DeleteByIdsAsync(sysDeletePlcList.Cast<object>().ToArray()))
                await RefreshCache();//刷新缓存
        }
    }

    #endregion 删除

    #region 其他

    /// <inheritdoc />
    public async Task RefreshCache()
    {
        _simpleCacheService.Remove(CacheConst.Cache_PlcConfig);//从redis删除
        await GetListAsync();//刷新缓存
    }

    /// <inheritdoc />
    public List<PlcConfig> ConstrucTrees(List<PlcConfig> PlcList, long parentId = 0)
    {
        //找下级字典ID列表
        var Plcs = PlcList.Where(it => it.ParentId == parentId).OrderBy(it => it.SortCode).ToList();
        if (Plcs.Count > 0)//如果数量大于0
        {
            var data = new List<PlcConfig>();
            foreach (var item in Plcs)//遍历字典
            {
                item.Children = ConstrucTrees(PlcList, item.Id);//添加子节点
                data.Add(item);//添加到列表
            }
            return data;//返回结果
        }
        return new List<PlcConfig>();
    }

    #endregion 其他

    #region 方法

    /// <summary>
    /// 检查输入参数
    /// </summary>
    /// <param name="sysPlc"></param>
    /// <param name="name"></param>
    private async Task CheckInput(PlcConfig sysPlc, string name)
    {
        //判断分类是否正确
        if (sysPlc.Category != CateGoryConst.Plc_PLC
            && sysPlc.Category != CateGoryConst.Plc_CUSTOM_R
            && sysPlc.Category != CateGoryConst.Plc_CUSTOM_W
            && sysPlc.Category != CateGoryConst.Plc_GGQ_R
            && sysPlc.Category != CateGoryConst.Plc_GGQ_W
            && sysPlc.Category != CateGoryConst.Plc_SJQ_R
            && sysPlc.Category != CateGoryConst.Plc_SJQ_W
            )
            throw Oops.Bah($"{name}所属分类错误:{sysPlc.Category}");

        var sysPlcs = await GetListAsync();//获取全部
        if (sysPlcs.Any(it => it.ParentId == sysPlc.ParentId && it.Name == sysPlc.Name && it.Id != sysPlc.Id))//判断同级是否有名称重复的
            throw Oops.Bah($"存在重复的同级{name}:{sysPlc.Name}");
        sysPlc.Names = sysPlc.Name;//全称默认自己
        if (sysPlc.ParentId != 0)
        {
            //获取父级,判断父级ID正不正确
            var parent = sysPlcs.Where(it => it.Id == sysPlc.ParentId).FirstOrDefault();
            if (parent != null)
            {
                if (parent.Id == sysPlc.Id)
                    throw Oops.Bah($"上级{name}不能选择自己");
            }
            else
            {
                throw Oops.Bah($"上级{name}不存在:{sysPlc.ParentId}");
            }
            sysPlc.Names = GetNames(sysPlcs, sysPlc.ParentId, sysPlc.Name);
        }
    }

    /// <summary>
    /// 根据PLCId列表获取所有父级PLC
    /// </summary>
    /// <param name="allPlcList"></param>
    /// <param name="PlcIds"></param>
    /// <returns></returns>
    public List<PlcConfig> GetParentListByIds(List<PlcConfig> allPlcList, List<long> PlcIds)
    {
        var sysPlcs = new HashSet<PlcConfig>();//结果列表
        //遍历PLCID
        PlcIds.ForEach(it =>
        {
            //获取该PLCID的所有父级
            var parents = GetPlcParents(allPlcList, it);
            sysPlcs.AddRange(parents);//添加到结果
        });
        return sysPlcs.ToList();
    }

    /// <summary>
    /// 获取PLC所有下级
    /// </summary>
    /// <param name="PlcList"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    public List<PlcConfig> GetPlcConfigChilden(List<PlcConfig> PlcList, long parentId)
    {
        //找下级PLCID列表
        var Plcs = PlcList.Where(it => it.ParentId == parentId).ToList();
        if (Plcs.Count > 0)//如果数量大于0
        {
            var data = new List<PlcConfig>();
            foreach (var item in Plcs)//遍历PLC
            {
                var childen = GetPlcConfigChilden(PlcList, item.Id);//获取子节点
                data.AddRange(childen);//添加子节点);
                data.Add(item);//添加到列表
            }
            return data;//返回结果
        }
        return new List<PlcConfig>();
    }

    /// <summary>
    /// 获取PLC下级(懒加载)
    /// </summary>
    /// <param name="PlcList"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    public List<PlcConfig> GetPlcConfigChildenLazy(List<PlcConfig> PlcList, long parentId)
    {
        //找下级PLCID列表
        var Plcs = PlcList.Where(it => it.ParentId == parentId).ToList();
        if (Plcs.Count > 0)//如果数量大于0
        {
            var data = new List<PlcConfig>();
            foreach (var item in Plcs)//遍历PLC
            {
                var childen = PlcList.Where(it => it.ParentId == item.Id).ToList();//获取子节点
                //遍历子节点
                childen.ForEach(it =>
                {
                    if (!PlcList.Any(Plc => Plc.ParentId == it.Id)) it.IsLeaf = true;//如果没有下级,则设置为叶子节点
                });
                data.AddRange(childen);//添加子节点);
                data.Add(item);//添加到列表
            }
            return data;//返回结果
        }
        return new List<PlcConfig>();
    }

    /// <summary>
    /// 赋值PLC的所有下级
    /// </summary>
    /// <param PlcName="PlcList">PLC列表</param>
    /// <param PlcName="parentId">父Id</param>
    /// <param PlcName="newParentId">新父Id</param>
    /// <returns></returns>
    public List<PlcConfig> CopyPlcConfigChilden(List<PlcConfig> PlcList, long parentId, long newParentId)
    {
        //找下级PLC列表
        var Plcs = PlcList.Where(it => it.ParentId == parentId).ToList();
        if (Plcs.Count > 0)//如果数量大于0
        {
            var data = new List<PlcConfig>();
            var newId = CommonUtils.GetSingleId();
            foreach (var item in Plcs)//遍历PLC
            {
                var childen = CopyPlcConfigChilden(PlcList, item.Id, newId);//获取子节点
                data.AddRange(childen);//添加子节点);
                RedirectPlc(item);//实体重新赋值
                item.ParentId = newParentId;//赋值父Id
                data.Add(item);//添加到列表
            }
            return data;//返回结果
        }
        return new List<PlcConfig>();
    }

    /// <summary>
    /// 重新生成PLC实体
    /// </summary>
    /// <param PlcName="Plc"></param>
    private void RedirectPlc(PlcConfig Plc)
    {
        //重新生成ID并赋值
        var newId = CommonUtils.GetSingleId();
        Plc.Id = newId;
        Plc.Code = RandomHelper.CreateRandomString(10);
        Plc.CreateTime = DateTime.Now;
        Plc.CreateUser = UserManager.UserAccount;
        Plc.CreateUserId = UserManager.UserId;
    }

    /// <summary>
    /// 获取全称
    /// </summary>
    /// <param name="sysPlcs">PLC列表</param>
    /// <param name="parentId">父Id</param>
    /// <param name="PlcName">PLC名称</param>
    public string GetNames(List<PlcConfig> sysPlcs, long parentId, string PlcName)
    {
        var names = "";
        //获取父级菜单
        var parents = GetPlcParents(sysPlcs, parentId, true);
        parents.ForEach(it => names += $"{it.Name}/");//循环加上名称
        names = names + PlcName;//赋值全称
        return names;
    }

    #endregion 方法
}