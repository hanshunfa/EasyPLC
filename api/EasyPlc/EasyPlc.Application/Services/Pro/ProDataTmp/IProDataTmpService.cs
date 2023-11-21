using Masuit.Tools.Models;

namespace EasyPlc.Application;

public interface IProDataTmpService : ITransient
{
    /// <summary>
    /// 获取所有过程数据
    /// </summary>
    /// <returns>加工过程数据列表</returns>
    Task<List<ProDataTmp>> GetListAsync();
    /// <summary>
    /// 获取数据，根据ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ProDataTmp> GetDataTmpById(long id);
    /// <summary>
    /// 分页查询，根据工单号
    /// </summary>
    /// <param name="input"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<PagedList<ProDataTmp>> PageByOrderId(ProOrderPageInput input);
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task Add(ProDataTmpAddInput input);
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<long> AddReturnId(ProDataTmpAddInput input);
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task Edit(ProDataTmpEditInput input);
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input);
    /// <summary>
    /// 刷新缓存
    /// </summary>
    /// <returns></returns>
    Task RefreshCache();
}
