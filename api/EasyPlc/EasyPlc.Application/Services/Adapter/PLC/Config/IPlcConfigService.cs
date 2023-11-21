namespace EasyPlc.Application;

/// <summary>
/// PLC架构服务
/// </summary>
public interface IPlcConfigService : ITransient
{
    #region 查询

    /// <summary>
    /// 检查PLC是否存在
    /// </summary>
    /// <param name="sysPlcs">PLC列表</param>
    /// <param name="PlcName">PLC名称</param>
    /// <param name="parentId">父Id</param>
    /// <param name="PlcId">PLCId</param>
    /// <returns>是否存在,存在返回PLCID</returns>
    bool IsExistPlcByName(List<PlcConfig> sysPlcs, string PlcName, long parentId, out long PlcId);

    /// <summary>
    /// 获取PLC树型结构
    /// </summary>
    /// <param name="PlcIds">机构ID列表</param>
    /// <param name="treeInput">PLC选择器(懒加载用)</param>
    /// <returns>PLC树列表</returns>
    Task<List<PlcConfig>> Tree(List<long> PlcIds = null, PlcConfigTreeInput treeInput = null);
    /// <summary>
    /// PLC详情
    /// </summary>
    /// <param name="input">id参数</param>
    /// <returns>PLC信息</returns>
    Task<PlcConfig> Detail(BaseIdInput input);

    /// <summary>
    /// 根据PLCID获取 下级
    /// </summary>
    /// <param name="PlcId">PLCID</param>
    /// <param name="isContainOneself">是否包含自己</param>
    /// <returns></returns>
    Task<List<PlcConfig>> GetChildListById(long PlcId, bool isContainOneself = true);

    /// <summary>
    /// 获取所有PLC
    /// </summary>
    /// <returns>PLC列表</returns>
    Task<List<PlcConfig>> GetListAsync();

    /// <summary>
    /// 获取所有PLC带排序
    /// </summary>
    /// <returns>PLC列表</returns>
    Task<List<PlcConfig>> GetListBySortCodeAsync();
    /// <summary>
    /// 获取机构及下级ID列表
    /// </summary>
    /// <param name="PlcId"></param>
    /// <param name="isContainOneself"></param>
    /// <returns></returns>
    Task<List<long>> GetPlcChildIds(long PlcId, bool isContainOneself = true);

    /// <summary>
    /// 根据PLCId递归获取上级
    /// </summary>
    /// <param name="allPlcList">PLC列表</param>
    /// <param name="PlcId">PLCId</param>
    /// <param name="includeSelf">是否包含自己</param>
    /// <returns></returns>
    List<PlcConfig> GetPlcParents(List<PlcConfig> allPlcList, long PlcId, bool includeSelf = true);

    /// <summary>
    /// 获取PLC信息
    /// </summary>
    /// <param name="id">PLCid</param>
    /// <returns>PLC信息</returns>
    Task<PlcConfig> GetPlcConfigById(long id);

    /// <summary>
    /// PLC分页查询
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页信息</returns>
    Task<SqlSugarPagedList<PlcConfig>> Page(PlcConfigPageInput input);


    #endregion 查询

    #region 新增

    /// <summary>
    /// 添加PLC
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Add(PlcConfigAddInput input, string name = EasyPlcConst.PlcConfig);
    /// <summary>
    /// 添加PLC
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task<long> AddReturnSnowflakeId(PlcConfigAddInput input, string name = EasyPlcConst.PlcConfig);
    /// <summary>
    /// 复制PLC
    /// </summary>
    /// <param name="input">机构复制参数</param>
    /// <returns></returns>
    Task Copy(PlcConfigCopyInput input);

    #endregion 新增

    #region 编辑

    /// <summary>
    /// 编辑PLC
    /// </summary>
    /// <param name="input">编辑参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Edit(PlcConfigEditInput input, string name = EasyPlcConst.PlcConfig);

    #endregion 编辑

    #region 删除

    /// <summary>
    /// 删除PLC
    /// </summary>
    /// <param name="input">删除参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.PlcConfig);

    #endregion 删除

    #region 其他

    /// <summary>
    /// 构建PLC树形结构
    /// </summary>
    /// <param name="PlcList">PLC列表</param>
    /// <param name="parentId">父ID</param>
    /// <returns>树型结构</returns>
    List<PlcConfig> ConstrucTrees(List<PlcConfig> PlcList, long parentId = 0);

    /// <summary>
    /// 刷新缓存
    /// </summary>
    /// <returns></returns>
    Task RefreshCache();

    

    #endregion 其他
}