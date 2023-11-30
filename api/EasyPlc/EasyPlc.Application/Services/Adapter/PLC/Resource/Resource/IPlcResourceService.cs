namespace EasyPlc.Application;
/// <summary>
/// 资源服务
/// </summary>
public interface IPlcResourceService : ITransient
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<SqlSugarPagedList<PlcResource>> Page(PlcResourcePageInput input);
    /// <summary>
    /// 获取资源树型结构
    /// </summary>
    /// <param name="resourceIds">资源ID列表</param>
    /// <param name="treeInput">PLC选择器(懒加载用)</param>
    /// <returns>PLC树列表</returns>
    Task<List<PlcResource>> Tree(List<long> resourceIds = null, PlcResourceTreeInput treeInput = null, bool isContainOneself = true);
    /// <summary>
    /// 根据资源ID获取所有下级资源
    /// </summary>
    /// <param name="resId">资源ID</param>
    /// <param name="isContainOneself">是否包含自己</param>
    /// <returns>资源列表</returns>
    Task<List<PlcResource>> GetChildListById(long resId, bool isContainOneself = true, bool depth = true);

    /// <summary>
    /// 根据资源ID获取资源
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<PlcResource> GetResurceById(long id);
    /// <summary>
    /// 根据资源ID获取所有下级资源
    /// </summary>
    /// <param name="plcResources">资源列表</param>
    /// <param name="resId">资源ID</param>
    /// <param name="isContainOneself">是否包含自己</param>
    /// <returns>资源列表</returns>
    List<PlcResource> GetChildListById(List<PlcResource> plcResources, long resId, bool isContainOneself = true);

    /// <summary>
    /// 根据资源ID获取所有同级以及上级的同级资源
    /// </summary>
    /// <param name="resId">资源ID</param>
    /// <param name="isContainOneself">是否包含自己</param>
    /// <returns>资源列表</returns>
    Task<List<PlcResource>> GetBrotherListById(long resId, bool isContainOneself = true);

    /// <summary>
    /// 根据资源ID获取所有同级以及上级的同级资源
    /// </summary>
    /// <param name="plcResources">资源列表</param>
    /// <param name="resId">资源ID</param>
    /// <param name="isContainOneself">是否包含自己</param>
    /// <returns>资源列表</returns>
    List<PlcResource> GetBrotherListById(List<PlcResource> plcResources, long resId, bool isContainOneself = true);

    /// <summary>
    /// 获取ID获取Code列表
    /// </summary>
    /// <param name="ids">id列表</param>
    /// <param name="category">分类</param>
    /// <returns>Code列表</returns>
    Task<List<string>> GetCodeByIds(List<long> ids, string category);

    /// <summary>
    /// 获取资源列表
    /// </summary>
    /// <param name="categorys">资源分类列表</param>
    /// <returns></returns>
    Task<List<PlcResource>> GetListAsync(List<string> categorys = null);
    /// <summary>
    /// 获取资源列表基于父ID和排序
    /// </summary>
    /// <returns></returns>
    Task<List<PlcResource>> GetListBySortCodeAsync();
    /// <summary>
    /// 根据分类获取资源列表
    /// </summary>
    /// <param name="category">分类名称</param>
    /// <returns>资源列表</returns>
    Task<List<PlcResource>> GetListByCategory(string category);
    /// <summary>
    /// 计算当前Id下所有数据所占地址长度
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<int> GetLenghAsync(long id);
    /// <summary>
    /// 资源复制
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task Copy(PlcResourceCopyInput input);
    /// <summary>
    /// 重命名资源复制
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task ChangedNameCopy(PlcResourceCopyInput input);
    /// <summary>
    /// 资源复制
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<List<PlcResource>> GetCopy(PlcResourceCopyInput input);
    /// <summary>
    /// 资源剪切
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task Cut(PlcResourceCopyInput input);

    /// <summary>
    /// 刷新缓存
    /// </summary>
    /// <param name="category">分类名称</param>
    /// <returns></returns>
    Task RefreshCache(string category = null);
    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task Sort(PlcResourceSortInput input);
}