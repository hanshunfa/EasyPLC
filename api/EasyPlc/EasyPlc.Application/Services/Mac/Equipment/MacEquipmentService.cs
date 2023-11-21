namespace EasyPlc.Application;

public class MacEquipmentService : DbRepository<MacEquipment>, IMacEquipmentService
{
    private readonly ISimpleCacheService _simpleCacheService;
    private readonly IMacRelationService _macRelationService;

    public MacEquipmentService(
        ISimpleCacheService simpleCacheService,
        IMacRelationService macRelationService
        )
    {
        _simpleCacheService = simpleCacheService;
        _macRelationService = macRelationService;
    }

    #region 查询
    public override async Task<List<MacEquipment>> GetListAsync()
    {
        //先从Redis拿
        var macEquipments = _simpleCacheService.Get<List<MacEquipment>>(CacheConst.Cache_MacEquipment);
        if (macEquipments == null)
        {
            //redis没有就去数据库拿
            macEquipments = await base.GetListAsync();
            if (macEquipments.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(CacheConst.Cache_MacEquipment, macEquipments);
            }
        }
        return macEquipments;
    }
    public async Task<List<MacEquipment>> GetEquipmentListByFlowId(long flowId)
    {
        var listEquiment = await GetListAsync();
        List<MacEquipment> equipments = new List<MacEquipment>();
        var equipmentList = await _macRelationService.GetRelationListByObjectIdAndCategory(flowId, CateGoryConst.Relation_MAC_FLOW_HAS_EQUIPMENT);
        var equipmentIdList = equipmentList.Select(x => x.TargetId.ToLong()).ToList();
        if(equipmentIdList.Count > 0)
        {
            equipmentIdList.ForEach(id => {
                var e = listEquiment.Where(it => it.Id == id).FirstOrDefault();
                if(e != null) equipments.Add(e);
            });
        }
        return equipments;
    }
    public async Task<List<MacEquipment>> GetListBySortCodeAsync()
    {
        //获取所有设备
        var equipmentList = await GetListAsync();
        return ConstrucEquipmentListTrees(equipmentList);
    }
    public async Task<MacEquipment> Detail(BaseIdInput input)
    {
        var macEquipments = await GetListAsync();
        var equipmentDetail = macEquipments.Where(it=>it.Id == input.Id).FirstOrDefault();
        return equipmentDetail;
    }
    public async Task<List<MacEquipment>> GetChildListById(long equipmentId, bool isContainOneself = true)
    {
        //获取所有设备
        var equipments = await GetListAsync();
        //直接找下级
        var childList = GetMacEquipmentChilden(equipments, equipmentId);
        //如果包含自己
        if(isContainOneself)
        {
            //获取自己的设备信息
            var self = equipments.Where(it=>it.Id == equipmentId).FirstOrDefault();
            if (self != null) childList.Insert(0, self);
        }
        return childList;
    }

    public async Task<List<long>> GetEquipmentChildIds(long equipmentId, bool isContainOneself = true)
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

    public async Task<MacEquipment> GetEquipmentById(long id)
    {
        //获取所有设备
        var equipments = await GetListAsync();
        var equipment = equipments.Where(it=>it.Id == id).FirstOrDefault();
        return equipment;
    }

    public List<MacEquipment> GetEquipmentParents(List<MacEquipment> allEquipmentList, long equipmentId, bool includeSelf = true)
    {
        //找到组织
        var equipments = allEquipmentList.Where(it => it.Id == equipmentId).FirstOrDefault();
        if (equipments != null)//如果组织不为空
        {
            var data = new List<MacEquipment>();
            var parents = GetEquipmentParents(allEquipmentList, equipments.ParentId, includeSelf);//递归获取父节点
            data.AddRange(parents);//添加父节点;
            if (includeSelf)
                data.Add(equipments);//添加到列表
            return data;//返回结果
        }
        return new List<MacEquipment>();
    }

    public bool IsExistEquipmentByName(List<MacEquipment> macEquipment, string equipmentName, long parentId, out long equipmentId)
    {
        equipmentId = 0;
        var equipment = macEquipment.Where(it => it.ParentId == parentId && it.Name == equipmentName).FirstOrDefault();
        if (equipment != null)
        {
            equipmentId = equipment.Id;
            return true;
        }
        else
            return false;
    }

    public async Task<SqlSugarPagedList<MacEquipment>> Page(MacEquipmentPageInput input)
    {
        var query = Context.Queryable<MacEquipment>()
           .WhereIF(input.ParentId > 0, it => it.ParentId == input.ParentId)//父级
           .WhereIF(input.EquipmentIds != null, it => input.EquipmentIds.Contains(it.Id))//机构ID查询
           .WhereIF(!string.IsNullOrEmpty(input.SearchKey), it => it.Name.Contains(input.SearchKey))//根据关键字查询
           .OrderByIF(!string.IsNullOrEmpty(input.SortField), $"{input.SortField} {input.SortOrder}")
           .OrderBy(it => it.SortCode);//排序
        var pageInfo = await query.ToPagedListAsync(input.Current, input.Size);//分页
        return pageInfo;
    }
    #endregion

    #region 新增
    public async Task Add(MacEquipmentAddInput input, string name = "设备")
    {
        await CheckInput(input, name);//检查参数
        var equipment = input.Adapt<MacEquipment>();//实体转换
        if (await InsertAsync(equipment))//插入数据
            await RefreshCache();//刷新缓存
    }
    public async Task Copy(MacEquipmentCopyInput input)
    {
        var equipmentList = await GetListAsync();//获取所有
        var ids = new HashSet<long>();//定义不重复Id集合
        var addEquipmentList = new List<MacEquipment>();//添加设备列表
        var alreadyIds = new HashSet<long>();//定义已经复制过得shebId
        ids.AddRange(input.Ids);//加到集合
        if (ids.Contains(input.TargetId))
            throw Oops.Bah($"不能包含自己");
        //获取目标设备
        var target = equipmentList.Where(it => it.Id == input.TargetId).FirstOrDefault();
        if (target != null || input.TargetId == EasyPlcConst.Zero)
        {
            //需要复制的设备名称列表
            var equipmentNames = equipmentList.Where(it => ids.Contains(it.Id)).Select(it => it.Name).ToList();
            //目标设备的一级子设备名称列表
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
                    RedirectEquipment(org);//生成新的实体
                    org.ParentId = input.TargetId;//父id为目标Id
                    addEquipmentList.Add(org);
                    //是否包含下级
                    if (input.ContainsChild)
                    {
                        var childIds = await GetEquipmentChildIds(id, false);//获取下级id列表
                        alreadyIds.AddRange(childIds);//添加到已复制id
                        var childList = equipmentList.Where(c => childIds.Contains(c.Id)).ToList();//获取下级
                        var addOrgs = CopyMacEquipmentChilden(childList, id, org.Id);//赋值下级组织
                        addEquipmentList.AddRange(addOrgs);
                    }
                }
            }
            //遍历机构重新赋值全称
            addEquipmentList.ForEach(it =>
            {
                it.Names = it.ParentId == EasyPlcConst.Zero ? it.Name : GetNames(equipmentList, it.ParentId, it.Name);
            });

            if (await InsertRangeAsync(addEquipmentList))//插入数据
                await RefreshCache();//刷新缓存
        }
    }

    #endregion

    #region 编辑
    public async Task Edit(MacEquipmentEditInput input, string name = EasyPlcConst.MacEquipment)
    {
        await CheckInput(input, name);//检查参数
        var equipment = input.Adapt<MacEquipment>();//实体转换
        if (await UpdateAsync(equipment))//更新数据
            await RefreshCache();//刷新缓存
    }
    #endregion

    #region 删除
    public async Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.MacEquipment)
    {
        //获取所有ID
        var ids = input.Select(it => it.Id).ToList();
        if (ids.Count > 0)
        {
            var equipments = await GetListAsync();//获取所有设备
            var sysDeleteEquipmentList = new List<long>();//需要删除的设备ID集合
            ids.ForEach(it =>
            {
                var childen = GetMacEquipmentChilden(equipments, it);//查找下级设备
                sysDeleteEquipmentList.AddRange(childen.Select(it => it.Id).ToList());
                sysDeleteEquipmentList.Add(it);
            });
            /*
             * 检查关联
             */

            //删除组织
            if (await DeleteByIdsAsync(sysDeleteEquipmentList.Cast<object>().ToArray()))
                await RefreshCache();//刷新缓存
        }
    }
    #endregion

    #region 其他
    public List<MacEquipment> ConstrucEquipmentTrees(List<MacEquipment> equipmentList, long parentId = 0)
    {
        //找下级字典ID列表
        var equipments = equipmentList.Where(it => it.ParentId == parentId).OrderBy(it => it.SortCode).ToList();
        if (equipments.Count > 0)//如果数量大于0
        {
            var data = new List<MacEquipment>();
            foreach (var item in equipments)//遍历字典
            {
                item.Children = ConstrucEquipmentTrees(equipmentList, item.Id);//添加子节点
                data.Add(item);//添加到列表
            }
            return data;//返回结果
        }
        return new List<MacEquipment>();
    }
    public List<MacEquipment> ConstrucEquipmentListTrees(List<MacEquipment> equipmentList, long parentId = 0)
    {
        //找下级字典ID列表
        var equipments = equipmentList.Where(it => it.ParentId == parentId).OrderBy(it => it.SortCode).ToList();
        if (equipments.Count > 0)//如果数量大于0
        {
            var data = new List<MacEquipment>();
            foreach (var item in equipments)//遍历字典
            {
                data.Add(item);//添加到列表
                data.AddRange(ConstrucEquipmentListTrees(equipmentList, item.Id));//添加子节点
            }
            return data;//返回结果
        }
        return new List<MacEquipment>();
    }
    public async Task RefreshCache()
    {
        _simpleCacheService.Remove(CacheConst.Cache_MacEquipment);//从redis删除
        await GetListAsync();//刷新缓存
    }

    public Task<List<MacEquipment>> Tree(List<long> orgIds = null, MacEquipmentTreeInput treeInput = null)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region 方法
    /// <summary>
    /// 检查输入参数
    /// </summary>
    /// <param name="macEquipment"></param>
    /// <param name="name"></param>
    private async Task CheckInput(MacEquipment macEquipment, string name)
    {
        //判断分类是否正确
        if (macEquipment.Category != CateGoryConst.Mac_LINE 
            && macEquipment.Category != CateGoryConst.Mac_EQUIPMENT
            && macEquipment.Category != CateGoryConst.Mac_STATION
            )
            throw Oops.Bah($"{name}所属分类错误:{macEquipment.Category}");

        var macEquipments = await GetListAsync();//获取全部
        if (macEquipments.Any(it => it.ParentId == macEquipment.ParentId && it.Name == macEquipment.Name && it.Id != macEquipment.Id))//判断同级是否有名称重复的
            throw Oops.Bah($"存在重复的同级{name}:{macEquipment.Name}");
        macEquipment.Names = macEquipment.Name;//全称默认自己
        if (macEquipment.ParentId != 0)
        {
            //获取父级,判断父级ID正不正确
            var parent = macEquipments.Where(it => it.Id == macEquipment.ParentId).FirstOrDefault();
            if (parent != null)
            {
                if (parent.Id == macEquipment.Id)
                    throw Oops.Bah($"上级{name}不能选择自己");
            }
            else
            {
                throw Oops.Bah($"上级{name}不存在:{macEquipment.ParentId}");
            }
            macEquipment.Names = GetNames(macEquipments, macEquipment.ParentId, macEquipment.Name);
        }
    }
    /// <summary>
    /// 赋值设备的所有下级
    /// </summary>
    /// <param equipmentName="orgList">组织列表</param>
    /// <param equipmentName="parentId">父Id</param>
    /// <param equipmentName="newParentId">新父Id</param>
    /// <returns></returns>
    public List<MacEquipment> CopyMacEquipmentChilden(List<MacEquipment> equipmentList, long parentId, long newParentId)
    {
        //找下级组织列表
        var orgs = equipmentList.Where(it => it.ParentId == parentId).ToList();
        if (orgs.Count > 0)//如果数量大于0
        {
            var data = new List<MacEquipment>();
            var newId = CommonUtils.GetSingleId();
            foreach (var item in orgs)//遍历组织
            {
                var childen = CopyMacEquipmentChilden(equipmentList, item.Id, newId);//获取子节点
                data.AddRange(childen);//添加子节点);
                RedirectEquipment(item);//实体重新赋值
                item.ParentId = newParentId;//赋值父Id
                data.Add(item);//添加到列表
            }
            return data;//返回结果
        }
        return new List<MacEquipment>();
    }
    /// <summary>
    /// 重新生成设备实体
    /// </summary>
    /// <param equipmentName="equipment"></param>
    private void RedirectEquipment(MacEquipment equipment)
    {
        //重新生成ID并赋值
        var newId = CommonUtils.GetSingleId();
        equipment.Id = newId;
        equipment.CreateTime = DateTime.Now;
        equipment.CreateUser = UserManager.UserAccount;
        equipment.CreateUserId = UserManager.UserId;
    }

    /// <summary>
    /// 获取设备所有下级
    /// </summary>
    /// <param name="equipmentList"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    public List<MacEquipment> GetMacEquipmentChilden(List<MacEquipment> equipmentList, long parentId)
    {
        //找下级组织ID列表
        var equipments = equipmentList.Where(it => it.ParentId == parentId).ToList();
        if (equipments.Count > 0)//如果数量大于0
        {
            var data = new List<MacEquipment>();
            foreach (var item in equipments)//遍历组织
            {
                var childen = GetMacEquipmentChilden(equipmentList, item.Id);//获取子节点
                data.AddRange(childen);//添加子节点);
                data.Add(item);//添加到列表
            }
            return data;//返回结果
        }
        return new List<MacEquipment>();
    }
    /// <summary>
    /// 获取全称
    /// </summary>
    /// <param name="sysOrgs">组织列表</param>
    /// <param name="parentId">父Id</param>
    /// <param name="orgName">组织名称</param>
    public string GetNames(List<MacEquipment> macEquipments, long parentId, string equipmentName)
    {
        var names = "";
        //获取父级菜单
        var parents = GetEquipmentParents(macEquipments, parentId, true);
        parents.ForEach(it => names += $"{it.Name}/");//循环加上名称
        names = names + equipmentName;//赋值全称
        return names;
    }
    #endregion
}
