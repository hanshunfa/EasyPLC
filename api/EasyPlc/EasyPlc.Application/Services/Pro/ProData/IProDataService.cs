namespace EasyPlc.Application;

public interface IProDataService : ITransient
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<SqlSugarPagedList<ProData>> PageByOrderId(ProDataPageInput input);
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task Add(ProDataAddInput input);
}
