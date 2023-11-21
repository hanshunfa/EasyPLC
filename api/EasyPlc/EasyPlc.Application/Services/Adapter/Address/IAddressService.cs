namespace EasyPlc.Application;

/// <summary>
/// PLC地址服务
/// </summary>
public interface IAddressService : ITransient
{
    #region 查询
    /// <summary>
    /// 查询所有地址
    /// </summary>
    /// <returns></returns>
    Task<List<PlcAddress>> GetListAsync();
    /// <summary>
    /// 通过Id查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<PlcAddress> GetAddressById(long id);
    /// <summary>
    /// 根据PLC ID获取地址
    /// </summary>
    /// <param name="plcId"></param>
    /// <returns></returns>
    Task<List<PlcAddress>> GetListByPlcId(long plcId);
    /// <summary>
    /// 根据PLC ID获取数据资源
    /// </summary>
    /// <param name="plcId"></param>
    /// <returns></returns>
    Task<List<PlcResource>> GetResourceListByPlcId(long plcId);
    #endregion

    #region 新增
    /// <summary>
    /// 添加地址
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task Add(AddressAddInput input);

    #endregion

    #region 编辑
    /// <summary>
    /// 编辑地址
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task Edit(AddressEditInput input);
    /// <summary>
    /// 排序
    /// </summary>
    /// <returns></returns>
    Task Sort(AddressSortInput input = null);
    #endregion

    #region 删除
    /// <summary>
    /// 删除地址
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input);
    #endregion

    #region 刷新
    Task RefreshCache();
    #endregion

}
