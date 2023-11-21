
namespace EasyPlc.Application;

public interface IExtWorkingStepService : ITransient
{
    /// <summary>
    /// 查询所有
    /// </summary>
    /// <returns></returns>
    Task<List<ExtWorkingStep>> GetListAsync();
    /// <summary>
    /// 根据加工记录Id查询
    /// </summary>
    /// <param name="workingStepId"></param>
    /// <returns></returns>
    Task<ExtWorkingStep> GetByWorkingStepId(long workingStepId);
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task Add(ExtWorkingStepAddInput input);
    /// <summary>
    /// 新增并反馈实体
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<ExtWorkingStep> AddReturnEntityAsync(ExtWorkingStepAddInput input);
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task Edit(ExtWorkingStepEditInput input);
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input);
    /// <summary>
    /// 删除，根据WorkingStepID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteByWorkingStepIdAsync(long id);
}
