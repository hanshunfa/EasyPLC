namespace EasyPlc.Application;

/// <summary>
/// 型号台账服务
/// </summary>
public interface IMacModelService : ITransient
{
    #region 查询
    /// <summary>
    /// 检查型号是否存在
    /// </summary>
    /// <param name="sysOrgs">型号列表</param>
    /// <param name="orgName">型号名称</param>
    /// <param name="parentId">父Id</param>
    /// <param name="orgId">型号Id</param>
    /// <returns>是否存在,存在返回型号ID</returns>
    bool IsExistModelByName(List<MacModel> macModel, string equipmentName, long parentId, out long equipmentId);
    /// <summary>
    /// 型号详情
    /// </summary>
    /// <param name="input">id参数</param>
    /// <returns>组织信息</returns>
    Task<MacModel> Detail(BaseIdInput input);
    /// <summary>
    /// 根据型号ID获取 下级
    /// </summary>
    /// <param name="equipmentId">组织ID</param>
    /// <param name="isContainOneself">是否包含自己</param>
    /// <returns></returns>
    Task<List<MacModel>> GetChildListById(long equipmentId, bool isContainOneself = true);
    /// <summary>
    /// 获取所有型号
    /// </summary>
    /// <returns>型号列表</returns>
    Task<List<MacModel>> GetListAsync();
    /// <summary>
    /// 获取所有型号带排序
    /// </summary>
    /// <returns>型号列表</returns>
    Task<List<MacModel>> GetListBySortCodeAsync();
    /// <summary>
    /// 获取型号及下级ID列表
    /// </summary>
    /// <param name="equipmentId"></param>
    /// <param name="isContainOneself"></param>
    /// <returns></returns>
    Task<List<long>> GetModelChildIds(long equipmentId, bool isContainOneself = true);
    /// <summary>
    /// 根据型号Id递归获取上级
    /// </summary>
    /// <param name="allModelList">型号列表</param>
    /// <param name="equipmentId">型号Id</param>
    /// <param name="includeSelf">是否包含自己</param>
    /// <returns></returns>
    List<MacModel> GetModelParents(List<MacModel> allModelList, long equipmentId, bool includeSelf = true);
    /// <summary>
    /// 获取型号信息
    /// </summary>
    /// <param name="id">型号id</param>
    /// <returns>型号信息</returns>
    Task<MacModel> GetMacModelById(long id);
    /// <summary>
    /// 获取型号信息
    /// </summary>
    /// <param name="name">型号name</param>
    /// <returns>型号信息</returns>
    Task<MacModel> GetMacModelByName(string name);
    /// <summary>
    /// 型号分页查询
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页信息</returns>
    Task<SqlSugarPagedList<MacModel>> Page(MacModelPageInput input);
    #endregion

    #region 新增
    /// <summary>
    /// 添加型号
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Add(MacModelAddInput input, string name = EasyPlcConst.MacModel);

    /// <summary>
    /// 复制型号
    /// </summary>
    /// <param name="input">机构复制参数</param>
    /// <returns></returns>
    Task Copy(MacModelCopyInput input);
    #endregion

    #region 编辑
    /// <summary>
    /// 编辑型号
    /// </summary>
    /// <param name="input">编辑参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Edit(MacModelEditInput input, string name = EasyPlcConst.MacModel);
    #endregion

    #region 删除
    /// <summary>
    /// 删除型号
    /// </summary>
    /// <param name="input">删除参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.MacModel);
    #endregion

    #region 其他
    /// <summary>
    /// 构建型号树形结构
    /// </summary>
    /// <param name="orgList">组织列表</param>
    /// <param name="parentId">父ID</param>
    /// <returns>树型结构</returns>
    List<MacModel> ConstrucModelTrees(List<MacModel> equipmentList, long parentId = 0);

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
    Task<List<MacModel>> Tree(List<long> orgIds = null, MacModelTreeInput treeInput = null);
    #endregion
}
