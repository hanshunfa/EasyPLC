
using Masuit.Tools.Models;

namespace EasyPlc.Application;
public interface IProWorkingStepService : ITransient
{
    /// <summary>
    /// 获取所有加工过程
    /// </summary>
    /// <returns>加工过程列表</returns>
    Task<List<ProWorkingStep>> GetListAsync();
    /// <summary>
    /// 获取加工过程，根据ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ProWorkingStep> GetWorkingStepById(long id);
    /// <summary>
    /// 获取加工过程，根据工单ID
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<List<ProWorkingStep>> GetListByOrderId(long orderId);
    /// <summary>
    /// 获取加工过程，根据工单ID，分页
    /// </summary>
    /// <param name="input"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<PagedList<ProWorkingStep>> PageByOrderId(ProOrderPageInput input, long orderId = 0);
    /// <summary>
    /// 获取加工过程，根据ID，分页
    /// </summary>
    /// <param name="input"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<PagedList<ProWorkingStep>> PageById(ProPageInput input);
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task Add(ProWorkingStepAddInput input);
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns>Id</returns>
    Task<long> AddReturnId(ProWorkingStepAddInput input);
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task Edit(ProWorkingStepEditInput input);
    /// <summary>
    /// 删除，根据ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input);
    /// <summary>
    /// 删除，根据ID，并删除临时数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task DeleteAndDataTmp(List<BaseIdInput> input);
    /// <summary>
    /// 删除，根据订单ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteByOrderId(long orderId);
}

