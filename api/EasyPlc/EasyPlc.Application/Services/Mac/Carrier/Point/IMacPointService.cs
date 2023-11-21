namespace EasyPlc.Application;

/// <summary>
/// 位置服务
/// </summary>
public interface IMacPointService : ITransient
{
    /// <summary>
    /// 添加位置
    /// </summary>
    /// <param name="input">添加位置</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Add(PointAddInput input, string name = EasyPlcConst.MacCarrierPosition);


    /// <summary>
    /// 删除位置
    /// </summary>
    /// <param name="input">id列表</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.MacCarrierPosition);

    /// <summary>
    /// 编辑位置
    /// </summary>
    /// <param name="input">编辑位置</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Edit(PointEditInput input, string name = EasyPlcConst.MacCarrierPosition);

    /// <summary>
    /// 获取位置列表
    /// </summary>
    /// <returns>位置列表</returns>
    Task<List<MacPoint>> GetListAsync();

    /// <summary>
    /// 获取位置信息
    /// </summary>
    /// <param name="id">位置ID</param>
    /// <returns>位置信息</returns>
    Task<MacPoint> GetMacPointById(long id);
    /// <summary>
    /// 获取位置信息，根据载具ID和穴位号
    /// </summary>
    /// <param name="carrierId"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    Task<MacPoint> GetMacPointByCarrierIdAndPoint(long carrierId, int point = 1);
    /// <summary>
    /// 位置分页查询
    /// </summary>
    /// <param name="input">查询位置</param>
    /// <returns>分页列表</returns>
    Task<SqlSugarPagedList<MacPoint>> Page(PointPageInput input);

    /// <summary>
    /// 位置选择器
    /// </summary>
    /// <param name="input">查询位置</param>
    /// <returns></returns>
    Task<List<MacPoint>> PointSelector(PointSelectorInput input);

    /// <summary>
    /// 刷新缓存
    /// </summary>
    /// <returns></returns>
    Task RefreshCache();
}