namespace EasyPlc.Application;

/// <summary>
/// 基础数据服务
/// </summary>
public interface IArrDataService : ITransient
{
    /// <summary>
    /// 添加基础数据
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <returns></returns>
    Task<long> Add(ArrDataAddInput input);

    /// <summary>
    /// 批量添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<List<long>> AddBatch(ArrDataAddInput input);

    /// <summary>
    /// 删除基础数据
    /// </summary>
    /// <param name="input">删除参数</param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input);
    /// <summary>
    /// 删除数据不包含自己
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task DeleteNoSelf(List<BaseIdInput> input);

    /// <summary>
    /// 编辑基础数据
    /// </summary>
    /// <param name="input">编辑参数</param>
    /// <returns></returns>
    Task<long> Edit(ArrDataEditInput input);

    /// <summary>
    /// 基础数据分页查询
    /// </summary>
    /// <param name="input">查询条件</param>
    /// <returns>基础数据分页列表</returns>
    Task<SqlSugarPagedList<PlcResource>> Page(ArrDataPageInput input);
}