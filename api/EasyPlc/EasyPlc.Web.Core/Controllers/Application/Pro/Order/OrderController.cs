/*=============================================================================================
* 
*      *******    *******         **    **
*      **         **              **    **
*      **         **              **    **
*      *******    *******   **    ********
*           **    **              **    **
*           **    **              **    **
*      *******    **              **    **
*
* 创建者：韩顺发
* CLR版本：4.0.30319.42000
* 电子邮箱：shunfa.han@kstopa.com.cn
* 创建时间：2023/11/10 14:08:11
* 版本：v1.0.0
* 描述：
*
* ==============================================================================================
* 修改人：
* 修改时间：
* 修改说明：
* 版本：
* 
===============================================================================================*/

namespace EasyPlc.Web.Core;

[ApiDescriptionSettings("Application", Tag = "工单")]
[Route("pro/[controller]")]
public class OrderController : AllowAnonymousController
{
    private readonly IProOrderService _orderService;

    public OrderController(
        IProOrderService orderService
        )
    {
        _orderService = orderService;
    }

    #region  查询
    /// <summary>
    /// 查询所有工单
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<List<ProOrder>> List()
    {
        return await _orderService.GetListAsync();
    }
    /// <summary>
    /// 查询以生产但没有完成的工单
    /// </summary>
    /// <returns></returns>
    [HttpGet("listByStatusDes")]
    public async Task<List<ProOrder>> GetListByStatusDes()
    {
        return await _orderService.GetListByStatusDes();
    }
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("page")]
    public async Task<dynamic> Page([FromQuery] ProOrderPageInput input)
    {
        return await _orderService.Page(input);
    }
    /// <summary>
    /// 根据ID查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("orderById")]
    public async Task<ProOrder> GetProOrderById([FromQuery] long id)
    {
        return await _orderService.GetProOrderById(id);
    }
    /// <summary>
    /// 获取当前正在生产的工单
    /// </summary>
    /// <returns></returns>
    [HttpGet("workingOrder")]
    public async Task<ProOrder> GetWorkingOrder()
    {
        return await _orderService.GetWorkingOrder();
    }
    /// <summary>
    /// 获取当前正在生产的工单，排除自己
    /// </summary>
    /// <param name="selfId"></param>
    /// <returns></returns>
    [HttpGet("workingOrderNoSelf")]
    public async Task<ProOrder> GetWorkingOrderNoSelf([FromQuery] long selfId)
    {
        return await _orderService.GetWorkingOrderNoSelf(selfId);
    }
    #endregion

    #region 新增
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task Add([FromBody] ProOrderAddInput input)
    {
        await _orderService.Add(input);
    }
    #endregion

    #region 编辑
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("edit")]
    public async Task Edit([FromBody] ProOrderAddInput input)
    {
        await _orderService.Edit(input);
    }
    /// <summary>
    /// 修改状态
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("setStatus")]
    public async Task SetStatus([FromBody] ProOrderStatusInput input)
    {
        await _orderService.SetStatus(input);
    }
    /// <summary>
    /// 工单准备好状态，根据工单生成对应的镭射，打标信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("readyOrder")]
    public async Task ReadyOrder([FromBody] BaseIdInput input)
    {
        await _orderService.ReadyOrder(input);
    }
    #endregion

    #region 删除
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("delete")]
    public async Task Delete([FromBody] List<BaseIdInput> input)
    {
        await _orderService.Delete(input);
    }
    #endregion
}
