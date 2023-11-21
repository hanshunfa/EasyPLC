
namespace EasyPlc.Application;
/// <summary>
/// 产品加工流程
/// </summary>
public interface IProProcessService : ISingleton
{
    /// <summary>
    /// 准备工单，根据工单生成与之相关的功能服务，如标签，镭射等
    /// </summary>
    /// <param name="baseIdInput"></param>
    /// <returns></returns>
    Task ReadyOrder(BaseIdInput baseIdInput);
    Task GetWorkingOrderInfo();
    /// <summary>
    /// 设置线束SN
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<CableOutput> SetCableSN(CableInput input);
    /// <summary>
    /// 根据返修品，返修ID获取该返修品能够选择的返修工站
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<RepairGetOutput> GetRepairStep(RepairGetInput input);
    /// <summary>
    /// Eap回复需要返修的工艺
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<RepairSetOutput> SetRepairStep(RepairSetInput input);
}
