
using Masuit.Tools.Models;

namespace EasyPlc.Application;

public interface IProOrderService : ITransient
{
    #region  查询
    /// <summary>
    /// 查询所有工单
    /// </summary>
    /// <returns></returns>
    Task<List<ProOrder>> GetListAsync();
    /// <summary>
    /// 查询以生产但没有完成的工单
    /// </summary>
    /// <returns></returns>
    Task<List<ProOrder>> GetListByStatusDes();
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedList<ProOrder>> Page(ProOrderPageInput input);
    /// <summary>
    /// 根据ID查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ProOrder> GetProOrderById(long id);
    /// <summary>
    /// 获取当前正在生产的工单
    /// </summary>
    /// <returns></returns>
    Task<ProOrder> GetWorkingOrder();
    /// <summary>
    /// 获取当前正在生产的工单，排除自己
    /// </summary>
    /// <param name="selfId"></param>
    /// <returns></returns>
    Task<ProOrder> GetWorkingOrderNoSelf(long selfId);
    #endregion

    #region 新增
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task Add(ProOrderAddInput input, string name = EasyPlcConst.ProOrder);
    #endregion

    #region 编辑
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task Edit(ProOrderAddInput input, string name = EasyPlcConst.ProOrder);
    /// <summary>
    /// 修改投入数量
    /// </summary>
    /// <param name="input"></param>
    /// <param name="putQty"></param>
    /// <returns></returns>
    Task EditPutQty(BaseIdInput input, int putQty, int onlineQty);
    /// 跟新数量
    /// </summary>
    /// <param name="input"></param>
    /// <param name="okQty"></param>
    /// <param name="onlineQty"></param>
    /// <param name="repairQty"></param>
    /// <param name="scrapQty"></param>
    /// <returns></returns>
    Task EditQty(BaseIdInput input, int okQty, int onlineQty, int repairQty, int scrapQty);
    /// <summary>
    /// 修改状态
    /// </summary>
    /// <param name="input"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    Task SetStatus(ProOrderStatusInput input);
    /// <summary>
    /// 工单准备好状态，根据工单生成对应的镭射，打标信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task ReadyOrder(BaseIdInput input);
    #endregion

    #region 删除
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.ProOrder);
    #endregion

    /// <summary>
    /// 刷新
    /// </summary>
    /// <returns></returns>
    Task RefreshCache();
}
