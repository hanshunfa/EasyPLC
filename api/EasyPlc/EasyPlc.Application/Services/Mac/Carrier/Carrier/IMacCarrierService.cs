namespace EasyPlc.Application;

/// <summary>
/// 载具服务
/// </summary>
public interface IMacCarrierService : ITransient
{
    /// <summary>
    /// 添加载具
    /// </summary>
    /// <param name="input">添加载具</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Add(CarrierAddInput input, string name = EasyPlcConst.MacCarrier);

    /// <summary>
    /// 删除载具
    /// </summary>
    /// <param name="input">id列表</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.MacCarrier);

    /// <summary>
    /// 编辑载具
    /// </summary>
    /// <param name="input">编辑载具</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Edit(CarrierEditInput input, string name = EasyPlcConst.MacCarrier);

    /// <summary>
    /// 获取载具列表
    /// </summary>
    /// <returns>载具列表</returns>
    Task<List<MacCarrier>> GetListAsync();

    /// <summary>
    /// 获取载具信息
    /// </summary>
    /// <param name="id">载具ID</param>
    /// <returns>载具信息</returns>
    Task<MacCarrier> GetMacCarrierById(long id);
    /// <summary>
    /// 获取载具列表根据型号ID
    /// </summary>
    /// <param name="modelId"></param>
    /// <returns></returns>
    Task<List<MacCarrier>> GetListByModelId(long modelId);
    /// <summary>
    /// 载具分页查询
    /// </summary>
    /// <param name="input">查询载具</param>
    /// <returns>分页列表</returns>
    Task<SqlSugarPagedList<MacCarrier>> Page(CarrierPageInput input);

    /// <summary>
    /// 载具选择器
    /// </summary>
    /// <param name="input">查询载具</param>
    /// <returns></returns>
    Task<List<MacCarrier>> CarrierSelector(CarrierSelectorInput input);

    /// <summary>
    /// 刷新缓存
    /// </summary>
    /// <returns></returns>
    Task RefreshCache();
}