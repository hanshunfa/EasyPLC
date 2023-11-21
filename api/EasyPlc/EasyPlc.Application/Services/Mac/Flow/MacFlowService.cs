namespace EasyPlc.Application;

public class MacFlowService : DbRepository<MacFlow>, IMacFlowService
{
    private readonly ILogger<MacFlowService> _logger;
    private readonly ISimpleCacheService _simpleCacheService;
    private readonly IMacRelationService _macRelationService;

    public MacFlowService(
        ILogger<MacFlowService> logger,
        ISimpleCacheService simpleCacheService,
        IMacRelationService macRelationService
        )
    {
        _logger = logger;
        _simpleCacheService = simpleCacheService;
        _macRelationService = macRelationService;
    }

    #region 查询
    public override async Task<List<MacFlow>> GetListAsync()
    {
        //先从Redis拿
        var macFlows = _simpleCacheService.Get<List<MacFlow>>(CacheConst.Cache_MacFlow);
        if (macFlows == null)
        {
            //redis没有就去数据库拿
            macFlows = await base.GetListAsync();
            if (macFlows.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(CacheConst.Cache_MacFlow, macFlows);
            }
        }
        return macFlows;
    }
    public async Task<List<MacFlow>> GetListBySortCodeAsync()
    {
        //获取所有流程
        var equipments = await GetListAsync();
        //排序
        equipments = equipments.OrderBy(it=>it.Category).OrderBy(it=>it.SortCode).ToList();
        return equipments;
    }
    public async Task<MacFlow> Detail(BaseIdInput input)
    {
        var macFlows = await GetListAsync();
        var equipmentDetail = macFlows.Where(it=>it.Id == input.Id).FirstOrDefault();
        return equipmentDetail;
    }
    public async Task<List<MacFlow>> GetChildListById(long equipmentId, bool isContainOneself = true)
    {
        //获取所有流程
        var equipments = await GetListAsync();
        //直接找下级
        var childList = GetMacFlowChilden(equipments, equipmentId);
        //如果包含自己
        if(isContainOneself)
        {
            //获取自己的流程信息
            var self = equipments.Where(it=>it.Id == equipmentId).FirstOrDefault();
            if (self != null) childList.Insert(0, self);
        }
        return childList;
    }

    public async Task<List<long>> GetFlowChildIds(long equipmentId, bool isContainOneself = true)
    {
        var equipmentIds = new List<long>();
        if(equipmentId > 0)
        {
            //获取所有子集
            var equipments = await GetChildListById(equipmentId, isContainOneself);
            equipmentIds = equipments.Select(it=>it.Id).ToList();//获取Id列表
        }
        return equipmentIds;
    }

    public async Task<MacFlow> GetMacFlowById(long id)
    {
        //获取所有流程
        var equipments = await GetListAsync();
        var equipment = equipments.Where(it=>it.Id == id).FirstOrDefault();
        return equipment;
    }
    public List<MacFlow> GetFlowParents(List<MacFlow> allFlowList, long equipmentId, bool includeSelf = true)
    {
        //找到流程
        var equipments = allFlowList.Where(it => it.Id == equipmentId).FirstOrDefault();
        if (equipments != null)//如果流程不为空
        {
            var data = new List<MacFlow>();
            var parents = GetFlowParents(allFlowList, equipments.ParentId, includeSelf);//递归获取父节点
            data.AddRange(parents);//添加父节点;
            if (includeSelf)
                data.Add(equipments);//添加到列表
            return data;//返回结果
        }
        return new List<MacFlow>();
    }

    public bool IsExistFlowByName(List<MacFlow> macFlow, string equipmentName, long parentId, out long equipmentId)
    {
        equipmentId = 0;
        var equipment = macFlow.Where(it => it.ParentId == parentId && it.Name == equipmentName).FirstOrDefault();
        if (equipment != null)
        {
            equipmentId = equipment.Id;
            return true;
        }
        else
            return false;
    }

    public async Task<SqlSugarPagedList<MacFlow>> Page(MacFlowPageInput input)
    {
        var query = Context.Queryable<MacFlow>()
           .WhereIF(input.ParentId > 0, it => it.ParentId == input.ParentId)//父级
           .WhereIF(input.FlowIds != null, it => input.FlowIds.Contains(it.Id))//机构ID查询
           .WhereIF(!string.IsNullOrEmpty(input.SearchKey), it => it.Name.Contains(input.SearchKey))//根据关键字查询
           .OrderByIF(!string.IsNullOrEmpty(input.SortField), $"{input.SortField} {input.SortOrder}")
           .OrderBy(it => it.SortCode);//排序
        var pageInfo = await query.ToPagedListAsync(input.Current, input.Size);//分页
        return pageInfo;
    }
    #endregion

    #region 新增
    public async Task Add(MacFlowAddInput input, string name = "流程")
    {
        await CheckInput(input, name);//检查参数
        var equipment = input.Adapt<MacFlow>();//实体转换
        equipment.Code = RandomHelper.CreateRandomString(10);//赋值Code
        if (await InsertAsync(equipment))//插入数据
            await RefreshCache();//刷新缓存
    }
    public async Task<long> AddReturnSnowflakeId(MacFlowAddInput input, string name = EasyPlcConst.MacFlow)
    {
        await CheckInput(input, name);//检查参数
        var equipment = input.Adapt<MacFlow>();//实体转换
        equipment.Code = RandomHelper.CreateRandomString(10);//赋值Code
        var id = (await InsertReturnEntityAsync(equipment)).Id;
        await RefreshCache();//刷新缓存
        return id;
    }
    public async Task Copy(MacFlowCopyInput input)
    {
        var equipmentList = await GetListAsync();//获取所有
        var ids = new HashSet<long>();//定义不重复Id集合
        var addFlowList = new List<MacFlow>();//添加流程列表
        var alreadyIds = new HashSet<long>();//定义已经复制过得shebId
        ids.AddRange(input.Ids);//加到集合
        if (ids.Contains(input.TargetId))
            throw Oops.Bah($"不能包含自己");
        //获取目标流程
        var target = equipmentList.Where(it => it.Id == input.TargetId).FirstOrDefault();
        if (target != null || input.TargetId == EasyPlcConst.Zero)
        {
            //需要复制的流程名称列表
            var equipmentNames = equipmentList.Where(it => ids.Contains(it.Id)).Select(it => it.Name).ToList();
            //目标流程的一级子流程名称列表
            var targetChildNames = equipmentList.Where(it => it.ParentId == input.TargetId).Select(it => it.Name).ToList();
            equipmentNames.ForEach(it =>
            {
                if (targetChildNames.Contains(it)) throw Oops.Bah($"已存在{it}");
            });

            foreach (var id in input.Ids)
            {
                var org = equipmentList.Where(o => o.Id == id).FirstOrDefault();//获取下级
                if (org != null && !alreadyIds.Contains(id))
                {
                    alreadyIds.Add(id);//添加到已复制列表
                    RedirectFlow(org);//生成新的实体
                    org.ParentId = input.TargetId;//父id为目标Id
                    addFlowList.Add(org);
                    //是否包含下级
                    if (input.ContainsChild)
                    {
                        var childIds = await GetFlowChildIds(id, false);//获取下级id列表
                        alreadyIds.AddRange(childIds);//添加到已复制id
                        var childList = equipmentList.Where(c => childIds.Contains(c.Id)).ToList();//获取下级
                        var addOrgs = CopyMacFlowChilden(childList, id, org.Id);//赋值下级流程
                        addFlowList.AddRange(addOrgs);
                    }
                }
            }
            //遍历机构重新赋值全称
            addFlowList.ForEach(it =>
            {
                it.Names = it.ParentId == EasyPlcConst.Zero ? it.Name : GetNames(equipmentList, it.ParentId, it.Name);
            });

            if (await InsertRangeAsync(addFlowList))//插入数据
                await RefreshCache();//刷新缓存
        }
    }

    #endregion

    #region 编辑
    public async Task Edit(MacFlowEditInput input, string name = EasyPlcConst.MacFlow)
    {
        await CheckInput(input, name);//检查参数
        var equipment = input.Adapt<MacFlow>();//实体转换
        if (await UpdateAsync(equipment))//更新数据
            await RefreshCache();//刷新缓存
    }
    public async Task GrantEuipment(FlowGrantEquipmentInput input)
    {
        var flow = await GetMacFlowById(input.Id);
        if(flow != null)
        {
            //授权
            await _macRelationService.SaveRelationBatch(CateGoryConst.Relation_MAC_FLOW_HAS_EQUIPMENT, input.Id, input.EquipmentIdList.Select(it => it.ToString()).ToList(), null, true);
        }
    }
    #endregion

    #region 删除
    public async Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.MacFlow)
    {
        //获取所有ID
        var ids = input.Select(it => it.Id).ToList();
        if (ids.Count > 0)
        {
            var equipments = await GetListAsync();//获取所有流程
            var deleteFlowList = new List<long>();//需要删除的流程ID集合
            ids.ForEach(it =>
            {
                var childen = GetMacFlowChilden(equipments, it);//查找下级流程
                deleteFlowList.AddRange(childen.Select(it => it.Id).ToList());
                deleteFlowList.Add(it);
            });
            //需要删除的关系
            //定义删除的关系
            var delRelations = new List<string> { CateGoryConst.Relation_MAC_FLOW_HAS_EQUIPMENT };
            //事务
            var result = await itenant.UseTranAsync(async () => { 
                //删除流程
                await DeleteByIdAsync(deleteFlowList);
                //删除关系
                var relationRep = ChangeRepository<DbRepository<MacRelation>>();//切换仓储
                //删除流程与工位之间关系
                await relationRep.DeleteAsync(it=> deleteFlowList.Contains(it.ObjectId) && delRelations.Contains(it.Category));
            });
            if(result.IsSuccess)
            {
                //刷新流程
                await RefreshCache();
                //刷新关系
                await _macRelationService.RefreshCache(CateGoryConst.Relation_MAC_FLOW_HAS_EQUIPMENT);
            }
            else
            {
                //写日志
                _logger.LogError(result.ErrorMessage, result.ErrorException);
                throw Oops.Oh(ErrorCodeEnum.A0002);
            }
        }
    }
    #endregion

    #region 其他
    public List<MacFlow> ConstrucFlowTrees(List<MacFlow> equipmentList, long parentId = 0)
    {
        throw new NotImplementedException();
    }
    public async Task RefreshCache()
    {
        _simpleCacheService.Remove(CacheConst.Cache_MacFlow);//从redis删除
        await GetListAsync();//刷新缓存
    }

    public Task<List<MacFlow>> Tree(List<long> orgIds = null, MacFlowTreeInput treeInput = null)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region 方法
    /// <summary>
    /// 检查输入参数
    /// </summary>
    /// <param name="macFlow"></param>
    /// <param name="name"></param>
    private async Task CheckInput(MacFlow macFlow, string name)
    {
        //判断分类是否正确
        if (macFlow.Category != CateGoryConst.Mac_FLOW_MORMAL
            && macFlow.Category != CateGoryConst.Mac_FLOW_REPAIR
            )
            throw Oops.Bah($"{name}所属分类错误:{macFlow.Category}");

        var macFlows = await GetListAsync();//获取全部
        if (macFlows.Any(it => it.ParentId == macFlow.ParentId && it.Name == macFlow.Name && it.Id != macFlow.Id))//判断同级是否有名称重复的
            throw Oops.Bah($"存在重复的同级{name}:{macFlow.Name}");
        macFlow.Names = macFlow.Name;//全称默认自己
        if (macFlow.ParentId != 0)
        {
            //获取父级,判断父级ID正不正确
            var parent = macFlows.Where(it => it.Id == macFlow.ParentId).FirstOrDefault();
            if (parent != null)
            {
                if (parent.Id == macFlow.Id)
                    throw Oops.Bah($"上级{name}不能选择自己");
            }
            else
            {
                throw Oops.Bah($"上级{name}不存在:{macFlow.ParentId}");
            }
            macFlow.Names = GetNames(macFlows, macFlow.ParentId, macFlow.Name);
        }
    }
    /// <summary>
    /// 赋值流程的所有下级
    /// </summary>
    /// <param equipmentName="orgList">流程列表</param>
    /// <param equipmentName="parentId">父Id</param>
    /// <param equipmentName="newParentId">新父Id</param>
    /// <returns></returns>
    public List<MacFlow> CopyMacFlowChilden(List<MacFlow> equipmentList, long parentId, long newParentId)
    {
        //找下级流程列表
        var orgs = equipmentList.Where(it => it.ParentId == parentId).ToList();
        if (orgs.Count > 0)//如果数量大于0
        {
            var data = new List<MacFlow>();
            var newId = CommonUtils.GetSingleId();
            foreach (var item in orgs)//遍历流程
            {
                var childen = CopyMacFlowChilden(equipmentList, item.Id, newId);//获取子节点
                data.AddRange(childen);//添加子节点);
                RedirectFlow(item);//实体重新赋值
                item.ParentId = newParentId;//赋值父Id
                data.Add(item);//添加到列表
            }
            return data;//返回结果
        }
        return new List<MacFlow>();
    }
    /// <summary>
    /// 重新生成流程实体
    /// </summary>
    /// <param equipmentName="equipment"></param>
    private void RedirectFlow(MacFlow equipment)
    {
        //重新生成ID并赋值
        var newId = CommonUtils.GetSingleId();
        equipment.Id = newId;
        equipment.CreateTime = DateTime.Now;
        equipment.CreateUser = UserManager.UserAccount;
        equipment.CreateUserId = UserManager.UserId;
    }

    /// <summary>
    /// 获取流程所有下级
    /// </summary>
    /// <param name="equipmentList"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    public List<MacFlow> GetMacFlowChilden(List<MacFlow> equipmentList, long parentId)
    {
        //找下级流程ID列表
        var equipments = equipmentList.Where(it => it.ParentId == parentId).ToList();
        if (equipments.Count > 0)//如果数量大于0
        {
            var data = new List<MacFlow>();
            foreach (var item in equipments)//遍历流程
            {
                var childen = GetMacFlowChilden(equipmentList, item.Id);//获取子节点
                data.AddRange(childen);//添加子节点);
                data.Add(item);//添加到列表
            }
            return data;//返回结果
        }
        return new List<MacFlow>();
    }
    /// <summary>
    /// 获取全称
    /// </summary>
    /// <param name="sysOrgs">流程列表</param>
    /// <param name="parentId">父Id</param>
    /// <param name="orgName">流程名称</param>
    public string GetNames(List<MacFlow> macFlows, long parentId, string equipmentName)
    {
        var names = "";
        //获取父级菜单
        var parents = GetFlowParents(macFlows, parentId, true);
        parents.ForEach(it => names += $"{it.Name}/");//循环加上名称
        names = names + equipmentName;//赋值全称
        return names;
    }
    #endregion
}
