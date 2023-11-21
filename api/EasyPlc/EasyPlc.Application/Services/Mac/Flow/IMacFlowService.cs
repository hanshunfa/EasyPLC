namespace EasyPlc.Application;

/// <summary>
/// 流程服务
/// </summary>
public interface IMacFlowService : ITransient
{
    #region 查询
    /// <summary>
    /// 检查流程是否存在
    /// </summary>
    /// <param name="sysOrgs">流程列表</param>
    /// <param name="orgName">流程名称</param>
    /// <param name="parentId">父Id</param>
    /// <param name="orgId">流程Id</param>
    /// <returns>是否存在,存在返回流程ID</returns>
    bool IsExistFlowByName(List<MacFlow> macFlow, string equipmentName, long parentId, out long equipmentId);
    /// <summary>
    /// 流程详情
    /// </summary>
    /// <param name="input">id参数</param>
    /// <returns>组织信息</returns>
    Task<MacFlow> Detail(BaseIdInput input);
    /// <summary>
    /// 根据流程ID获取 下级
    /// </summary>
    /// <param name="equipmentId">组织ID</param>
    /// <param name="isContainOneself">是否包含自己</param>
    /// <returns></returns>
    Task<List<MacFlow>> GetChildListById(long equipmentId, bool isContainOneself = true);
    /// <summary>
    /// 获取所有流程
    /// </summary>
    /// <returns>流程列表</returns>
    Task<List<MacFlow>> GetListAsync();
    /// <summary>
    /// 获取所有流程带排序
    /// </summary>
    /// <returns>流程列表</returns>
    Task<List<MacFlow>> GetListBySortCodeAsync();
    /// <summary>
    /// 获取流程及下级ID列表
    /// </summary>
    /// <param name="equipmentId"></param>
    /// <param name="isContainOneself"></param>
    /// <returns></returns>
    Task<List<long>> GetFlowChildIds(long equipmentId, bool isContainOneself = true);
    /// <summary>
    /// 根据流程Id递归获取上级
    /// </summary>
    /// <param name="allFlowList">流程列表</param>
    /// <param name="equipmentId">流程Id</param>
    /// <param name="includeSelf">是否包含自己</param>
    /// <returns></returns>
    List<MacFlow> GetFlowParents(List<MacFlow> allFlowList, long equipmentId, bool includeSelf = true);
    /// <summary>
    /// 获取流程信息
    /// </summary>
    /// <param name="id">流程id</param>
    /// <returns>流程信息</returns>
    Task<MacFlow> GetMacFlowById(long id);
    /// <summary>
    /// 流程分页查询
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页信息</returns>
    Task<SqlSugarPagedList<MacFlow>> Page(MacFlowPageInput input);
    #endregion

    #region 新增
    /// <summary>
    /// 添加流程
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Add(MacFlowAddInput input, string name = EasyPlcConst.MacFlow);
    /// <summary>
    /// 添加流程
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task<long> AddReturnSnowflakeId(MacFlowAddInput input, string name = EasyPlcConst.MacFlow);

    /// <summary>
    /// 复制流程
    /// </summary>
    /// <param name="input">机构复制参数</param>
    /// <returns></returns>
    Task Copy(MacFlowCopyInput input);
    #endregion

    #region 编辑
    /// <summary>
    /// 编辑流程
    /// </summary>
    /// <param name="input">编辑参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Edit(MacFlowEditInput input, string name = EasyPlcConst.MacFlow);

    /// <summary>
    /// 给流程授权设备
    /// </summary>
    /// <param name="input">授权参数</param>
    /// <returns></returns>
    Task GrantEuipment(FlowGrantEquipmentInput input);
    #endregion

    #region 删除
    /// <summary>
    /// 删除流程
    /// </summary>
    /// <param name="input">删除参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.MacFlow);
    #endregion

    #region 其他
    /// <summary>
    /// 构建流程树形结构
    /// </summary>
    /// <param name="orgList">组织列表</param>
    /// <param name="parentId">父ID</param>
    /// <returns>树型结构</returns>
    List<MacFlow> ConstrucFlowTrees(List<MacFlow> equipmentList, long parentId = 0);

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
    Task<List<MacFlow>> Tree(List<long> orgIds = null, MacFlowTreeInput treeInput = null);
    #endregion
}
