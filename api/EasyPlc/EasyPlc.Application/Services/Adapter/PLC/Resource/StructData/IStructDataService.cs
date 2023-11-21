namespace EasyPlc.Application;

/// <summary>
/// 基础数据服务
/// </summary>
public interface IStructDataService : ITransient
{
    /// <summary>
    /// 添加基础数据
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <returns></returns>
    Task Add(StructDataAddInput input);

    /// <summary>
    /// 删除基础数据
    /// </summary>
    /// <param name="input">删除参数</param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input);

    /// <summary>
    /// 编辑基础数据
    /// </summary>
    /// <param name="input">编辑参数</param>
    /// <returns></returns>
    Task Edit(StructDataEditInput input);

    /// <summary>
    /// 基础数据分页查询
    /// </summary>
    /// <param name="input">查询条件</param>
    /// <returns>基础数据分页列表</returns>
    Task<SqlSugarPagedList<PlcResource>> Page(StructDataPageInput input);
    /// <summary>
    /// 查询所有机构
    /// </summary>
    /// <returns></returns>
    Task<List<PlcResource>> GetListAsync();
}