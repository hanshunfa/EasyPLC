namespace EasyPlc.Application;

/// <summary>
/// 参数服务
/// </summary>
public interface IMacModelParamService : ITransient
{
    /// <summary>
    /// 添加参数
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Add(ParameterAddInput input, string name = EasyPlcConst.MacModelParam);

    /// <summary>
    /// 删除参数
    /// </summary>
    /// <param name="input">id列表</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.MacModelParam);

    /// <summary>
    /// 编辑参数
    /// </summary>
    /// <param name="input">编辑参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    Task Edit(ParameterEditInput input, string name = EasyPlcConst.MacModelParam);
    /// <summary>
    /// 拷贝参数
    /// </summary>
    /// <param name="selfId"></param>
    /// <param name="targetId"></param>
    /// <returns></returns>
    Task Copy(ParameterCopyInput input);
    /// <summary>
    /// 获取参数列表
    /// </summary>
    /// <returns>参数列表</returns>
    Task<List<MacModelParam>> GetListAsync();

    /// <summary>
    /// 获取参数信息
    /// </summary>
    /// <param name="id">参数ID</param>
    /// <returns>参数信息</returns>
    Task<MacModelParam> GetMacParameterById(long id);

    /// <summary>
    /// 参数分页查询
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页列表</returns>
    Task<SqlSugarPagedList<MacModelParam>> Page(ParameterPageInput input);
    /// <summary>
    /// 根据型号ID获取参数列表
    /// </summary>
    /// <returns></returns>
    Task<List<MacModelParam>> GetListByModelId(long id);

    /// <summary>
    /// 刷新缓存
    /// </summary>
    /// <returns></returns>
    Task RefreshCache();
}