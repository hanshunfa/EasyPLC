namespace EasyPlc.Application;

public class MacModelService : DbRepository<MacModel>, IMacModelService
{
    private readonly ISimpleCacheService _simpleCacheService;

    public MacModelService(ISimpleCacheService simpleCacheService)
    {
        _simpleCacheService = simpleCacheService;
    }

    #region 查询
    public override async Task<List<MacModel>> GetListAsync()
    {
        //先从Redis拿
        var macModels = _simpleCacheService.Get<List<MacModel>>(CacheConst.Cache_MacModel);
        if (macModels == null)
        {
            //redis没有就去数据库拿
            macModels = await base.GetListAsync();
            if (macModels.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(CacheConst.Cache_MacModel, macModels);
            }
        }
        return macModels;
    }
    public async Task<List<MacModel>> GetListBySortCodeAsync()
    {
        //获取所有型号
        var models = await GetListAsync();
        //排序
        models = models.OrderBy(it=>it.Category).OrderBy(it=>it.SortCode).ToList();
        return models;
    }
    public async Task<MacModel> Detail(BaseIdInput input)
    {
        var macModels = await GetListAsync();
        var modelDetail = macModels.Where(it=>it.Id == input.Id).FirstOrDefault();
        return modelDetail;
    }
    public async Task<List<MacModel>> GetChildListById(long modelId, bool isContainOneself = true)
    {
        //获取所有型号
        var models = await GetListAsync();
        //直接找下级
        var childList = GetMacModelChilden(models, modelId);
        //如果包含自己
        if(isContainOneself)
        {
            //获取自己的型号信息
            var self = models.Where(it=>it.Id == modelId).FirstOrDefault();
            if (self != null) childList.Insert(0, self);
        }
        return childList;
    }

    public async Task<List<long>> GetModelChildIds(long modelId, bool isContainOneself = true)
    {
        var modelIds = new List<long>();
        if(modelId > 0)
        {
            //获取所有子集
            var models = await GetChildListById(modelId, isContainOneself);
            modelIds = models.Select(it=>it.Id).ToList();//获取Id列表
        }
        return modelIds;
    }

    public async Task<MacModel> GetMacModelById(long id)
    {
        //获取所有型号
        var models = await GetListAsync();
        var model = models.Where(it=>it.Id == id).FirstOrDefault();
        return model;
    }
    public async Task<MacModel> GetMacModelByName(string name)
    {
        //获取所有型号
        var models = await GetListAsync();
        var model = models.Where(it => it.Name == name).FirstOrDefault();
        return model;
    }

    public List<MacModel> GetModelParents(List<MacModel> allModelList, long modelId, bool includeSelf = true)
    {
        //找到组织
        var models = allModelList.Where(it => it.Id == modelId).FirstOrDefault();
        if (models != null)//如果组织不为空
        {
            var data = new List<MacModel>();
            var parents = GetModelParents(allModelList, models.ParentId, includeSelf);//递归获取父节点
            data.AddRange(parents);//添加父节点;
            if (includeSelf)
                data.Add(models);//添加到列表
            return data;//返回结果
        }
        return new List<MacModel>();
    }

    public bool IsExistModelByName(List<MacModel> macModel, string modelName, long parentId, out long modelId)
    {
        modelId = 0;
        var model = macModel.Where(it => it.ParentId == parentId && it.Name == modelName).FirstOrDefault();
        if (model != null)
        {
            modelId = model.Id;
            return true;
        }
        else
            return false;
    }

    public async Task<SqlSugarPagedList<MacModel>> Page(MacModelPageInput input)
    {
        var query = Context.Queryable<MacModel>()
           .WhereIF(input.ParentId > 0, it => it.ParentId == input.ParentId)//父级
           .WhereIF(input.ModelIds != null, it => input.ModelIds.Contains(it.Id))//机构ID查询
           .WhereIF(!string.IsNullOrEmpty(input.SearchKey), it => it.Name.Contains(input.SearchKey))//根据关键字查询
           .OrderByIF(!string.IsNullOrEmpty(input.SortField), $"{input.SortField} {input.SortOrder}")
           .OrderBy(it => it.SortCode);//排序
        var pageInfo = await query.ToPagedListAsync(input.Current, input.Size);//分页
        return pageInfo;
    }
    #endregion

    #region 新增
    public async Task Add(MacModelAddInput input, string name = "型号")
    {
        await CheckInput(input, name);//检查参数
        var model = input.Adapt<MacModel>();//实体转换
        if (await InsertAsync(model))//插入数据
            await RefreshCache();//刷新缓存
    }
    public async Task Copy(MacModelCopyInput input)
    {
        var modelList = await GetListAsync();//获取所有
        var ids = new HashSet<long>();//定义不重复Id集合
        var addModelList = new List<MacModel>();//添加型号列表
        var alreadyIds = new HashSet<long>();//定义已经复制过得shebId
        ids.AddRange(input.Ids);//加到集合
        if (ids.Contains(input.TargetId))
            throw Oops.Bah($"不能包含自己");
        //获取目标型号
        var target = modelList.Where(it => it.Id == input.TargetId).FirstOrDefault();
        if (target != null || input.TargetId == EasyPlcConst.Zero)
        {
            //需要复制的型号名称列表
            var modelNames = modelList.Where(it => ids.Contains(it.Id)).Select(it => it.Name).ToList();
            //目标型号的一级子型号名称列表
            var targetChildNames = modelList.Where(it => it.ParentId == input.TargetId).Select(it => it.Name).ToList();
            modelNames.ForEach(it =>
            {
                if (targetChildNames.Contains(it)) throw Oops.Bah($"已存在{it}");
            });

            foreach (var id in input.Ids)
            {
                var resource = modelList.Where(o => o.Id == id).FirstOrDefault();//获取下级
                if (resource != null && !alreadyIds.Contains(id))
                {
                    alreadyIds.Add(id);//添加到已复制列表
                    RedirectModel(resource);//生成新的实体
                    resource.ParentId = input.TargetId;//父id为目标Id
                    addModelList.Add(resource);
                    //是否包含下级
                    if (input.ContainsChild)
                    {
                        var childIds = await GetModelChildIds(id, false);//获取下级id列表
                        alreadyIds.AddRange(childIds);//添加到已复制id
                        var childList = modelList.Where(c => childIds.Contains(c.Id)).ToList();//获取下级
                        var addOrgs = CopyMacModelChilden(childList, id, resource.Id);//赋值下级组织
                        addModelList.AddRange(addOrgs);
                    }
                }
            }
            //遍历机构重新赋值全称
            addModelList.ForEach(it =>
            {
                it.Names = it.ParentId == EasyPlcConst.Zero ? it.Name : GetNames(modelList, it.ParentId, it.Name);
            });

            if (await InsertRangeAsync(addModelList))//插入数据
                await RefreshCache();//刷新缓存
        }
    }

    #endregion

    #region 编辑
    public async Task Edit(MacModelEditInput input, string name = EasyPlcConst.MacModel)
    {
        await CheckInput(input, name);//检查参数
        var model = input.Adapt<MacModel>();//实体转换
        if (await UpdateAsync(model))//更新数据
            await RefreshCache();//刷新缓存
    }
    #endregion

    #region 删除
    public async Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.MacModel)
    {
        //获取所有ID
        var ids = input.Select(it => it.Id).ToList();
        if (ids.Count > 0)
        {
            var models = await GetListAsync();//获取所有型号
            var sysDeleteModelList = new List<long>();//需要删除的型号ID集合
            ids.ForEach(it =>
            {
                var childen = GetMacModelChilden(models, it);//查找下级型号
                sysDeleteModelList.AddRange(childen.Select(it => it.Id).ToList());
                sysDeleteModelList.Add(it);
            });
            /*
             * 检查关联
             */

            //删除组织
            if (await DeleteByIdsAsync(sysDeleteModelList.Cast<object>().ToArray()))
                await RefreshCache();//刷新缓存
        }
    }
    #endregion

    #region 其他
    public List<MacModel> ConstrucModelTrees(List<MacModel> modelList, long parentId = 0)
    {
        throw new NotImplementedException();
    }
    public async Task RefreshCache()
    {
        _simpleCacheService.Remove(CacheConst.Cache_MacModel);//从redis删除
        await GetListAsync();//刷新缓存
    }

    public Task<List<MacModel>> Tree(List<long> orgIds = null, MacModelTreeInput treeInput = null)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region 方法
    /// <summary>
    /// 检查输入参数
    /// </summary>
    /// <param name="macModel"></param>
    /// <param name="name"></param>
    private async Task CheckInput(MacModel macModel, string name)
    {
        //判断分类是否正确
        if (macModel.Category != CateGoryConst.Mac_MODEL_CLASS
            && macModel.Category != CateGoryConst.Mac_MODEL_MODEL
            )
            throw Oops.Bah($"{name}所属分类错误:{macModel.Category}");

        var macModels = await GetListAsync();//获取全部
        if (macModels.Any(it => it.ParentId == macModel.ParentId && it.Name == macModel.Name && it.Id != macModel.Id))//判断同级是否有名称重复的
            throw Oops.Bah($"存在重复的同级{name}:{macModel.Name}");
        macModel.Names = macModel.Name;//全称默认自己
        if (macModel.ParentId != 0)
        {
            //获取父级,判断父级ID正不正确
            var parent = macModels.Where(it => it.Id == macModel.ParentId).FirstOrDefault();
            if (parent != null)
            {
                if (parent.Id == macModel.Id)
                    throw Oops.Bah($"上级{name}不能选择自己");
            }
            else
            {
                throw Oops.Bah($"上级{name}不存在:{macModel.ParentId}");
            }
            macModel.Names = GetNames(macModels, macModel.ParentId, macModel.Name);
        }
    }
    /// <summary>
    /// 赋值型号的所有下级
    /// </summary>
    /// <param modelName="orgList">组织列表</param>
    /// <param modelName="parentId">父Id</param>
    /// <param modelName="newParentId">新父Id</param>
    /// <returns></returns>
    public List<MacModel> CopyMacModelChilden(List<MacModel> modelList, long parentId, long newParentId)
    {
        //找下级组织列表
        var orgs = modelList.Where(it => it.ParentId == parentId).ToList();
        if (orgs.Count > 0)//如果数量大于0
        {
            var data = new List<MacModel>();
            var newId = CommonUtils.GetSingleId();
            foreach (var item in orgs)//遍历组织
            {
                var childen = CopyMacModelChilden(modelList, item.Id, newId);//获取子节点
                data.AddRange(childen);//添加子节点);
                RedirectModel(item);//实体重新赋值
                item.ParentId = newParentId;//赋值父Id
                data.Add(item);//添加到列表
            }
            return data;//返回结果
        }
        return new List<MacModel>();
    }
    /// <summary>
    /// 重新生成型号实体
    /// </summary>
    /// <param modelName="model"></param>
    private void RedirectModel(MacModel model)
    {
        //重新生成ID并赋值
        var newId = CommonUtils.GetSingleId();
        model.Id = newId;
        model.CreateTime = DateTime.Now;
        model.CreateUser = UserManager.UserAccount;
        model.CreateUserId = UserManager.UserId;
    }

    /// <summary>
    /// 获取型号所有下级
    /// </summary>
    /// <param name="modelList"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    public List<MacModel> GetMacModelChilden(List<MacModel> modelList, long parentId)
    {
        //找下级组织ID列表
        var models = modelList.Where(it => it.ParentId == parentId).ToList();
        if (models.Count > 0)//如果数量大于0
        {
            var data = new List<MacModel>();
            foreach (var item in models)//遍历组织
            {
                var childen = GetMacModelChilden(modelList, item.Id);//获取子节点
                data.AddRange(childen);//添加子节点);
                data.Add(item);//添加到列表
            }
            return data;//返回结果
        }
        return new List<MacModel>();
    }
    /// <summary>
    /// 获取全称
    /// </summary>
    /// <param name="sysOrgs">组织列表</param>
    /// <param name="parentId">父Id</param>
    /// <param name="orgName">组织名称</param>
    public string GetNames(List<MacModel> macModels, long parentId, string modelName)
    {
        var names = "";
        //获取父级菜单
        var parents = GetModelParents(macModels, parentId, true);
        parents.ForEach(it => names += $"{it.Name}/");//循环加上名称
        names = names + modelName;//赋值全称
        return names;
    }
    #endregion
}
