namespace EasyPlc.Application;

/// <summary>
/// 设备台账服务
/// </summary>
public interface IMacEquipmentService : ITransient
{
    #region 查询
    /// <summary>
    /// 检查设备是否存在
    /// </summary>
    /// <param name="sysOrgs">设备列表</param>
    /// <param name="orgName">设备名称</param>
    /// <param name="parentId">父Id</param>
    /// <param name="orgId">设备Id</param>
    /// <returns>是否存在,存在返回设备ID</returns>
    bool IsExistEquipmentByName(List<MacEquipment> macEquipment, string equipmentName, long parentId, out long equipmentId);
    /// <summary>
    /// 设备详情
    /// </summary>
    /// <param name="input">id参数</param>
    /// <returns>组织信息</returns>
    Task<MacEquipment> Detail(BaseIdInput input);
    /// <summary>
    /// 根据设备ID获取 下级
    /// </summary>
    /// <param name="equipmentId">组织ID</param>
    /// <param name="isContainOneself">是否包含自己</param>
    /// <returns></returns>
    Task<List<MacEquipment>> GetChildListById(long equipmentId, bool isContainOneself = true);
    /// <summary>
    /// 获取所有设备
    /// </summary>
    /// <returns>设备列表</returns>
    Task<List<MacEquipment>> GetListAsync();
    /// <summary>
    /// 根据流程Id获取设备集合
    /// </summary>
    /// <param name="flowId"></param>
    /// <returns></returns>
    Task<List<MacEquipment>> GetEquipmentListByFlowId(long flowId);
    /// <summary>
    /// 获取所有设备带排序
    /// </summary>
    /// <returns>设备列表</returns>
    Task<List<MacEquipment>> GetListBySortCodeAsync();
    /// <summary>
    /// 获取设备及下级ID列表
    /// </summary>
    /// <param name="equipmentId"></param>
    /// <param name="isContainOneself"></param>
    /// <returns></returns>
    Task<List<long>> GetEquipmentChildIds(long equipmentId, bool isContainOneself = true);
    /// <summary>
    /// 根据设备Id递归获取上级
    /// </summary>
    /// <param name="allEquipmentList">设备列表</param>
    /// <param name="equipmentId">设备Id</param>
    /// <param name="includeSelf">是否包含自己</param>
    /// <returns></returns>
    List<MacEquipment> GetEquipmentParents(List<MacEquipment> allEquipmentList, long equipmentId, bool includeSelf = true);
    /// <summary>
    /// 获取设备信息
    /// </summary>
    /// <param name="id">设备id</param>
    /// <returns>设备信息</returns>
    Task<MacEquipment> GetEquipmentById(long id);
    /// <summary>
    /// 设备分页查询
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页信息</returns>
    Task<SqlSugarPagedList<MacEquipment>> Page(MacEquipmentPageInput input);
    #endregion

    #region 新增
    /// <summary>
    /// 添加设备
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Add(MacEquipmentAddInput input, string name = EasyPlcConst.MacEquipment);

    /// <summary>
    /// 复制设备
    /// </summary>
    /// <param name="input">机构复制参数</param>
    /// <returns></returns>
    Task Copy(MacEquipmentCopyInput input);
    #endregion

    #region 编辑
    /// <summary>
    /// 编辑设备
    /// </summary>
    /// <param name="input">编辑参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Edit(MacEquipmentEditInput input, string name = EasyPlcConst.MacEquipment);
    #endregion

    #region 删除
    /// <summary>
    /// 删除设备
    /// </summary>
    /// <param name="input">删除参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.MacEquipment);
    #endregion

    #region 其他
    /// <summary>
    /// 构建设备树形结构
    /// </summary>
    /// <param name="orgList">组织列表</param>
    /// <param name="parentId">父ID</param>
    /// <returns>树型结构</returns>
    List<MacEquipment> ConstrucEquipmentTrees(List<MacEquipment> equipmentList, long parentId = 0);

    /// <summary>
    /// 刷新缓存
    /// </summary>
    /// <returns></returns>
    Task RefreshCache();

    /// <summary>
    /// 获取组织树型结构
    /// </summary>
    /// <param name="orgIds">机构ID列表</param>
    /// <param name="treeInput">组织选择器(懒加载用)</param>
    /// <returns>组织树列表</returns>
    Task<List<MacEquipment>> Tree(List<long> orgIds = null, MacEquipmentTreeInput treeInput = null);
    #endregion
}
