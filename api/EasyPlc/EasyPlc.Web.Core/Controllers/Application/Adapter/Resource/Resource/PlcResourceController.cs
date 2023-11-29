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
* 创建时间：2023/11/13 16:33:11
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

[ApiDescriptionSettings("Application", Tag = "基础数据资源类型")]
[Route("adapter/[controller]")]
public class PlcResourceController : AllowAnonymousController
{
    private readonly IPlcResourceService _plcResourceService;

    public PlcResourceController(
        IPlcResourceService plcResourceService
        )
    {
        _plcResourceService = plcResourceService;
    }
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("page")]
    public async Task<dynamic> Page([FromQuery] PlcResourcePageInput input)
    {
        return await _plcResourceService.Page(input);
    }

    /// <summary>
    /// 根据资源ID获取资源
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("resurceById")]
    public async Task<PlcResource> GetResurceById([FromQuery] long id)
    {
        return await _plcResourceService.GetResurceById(id);
    }

    /// <summary>
    /// 获取资源列表
    /// </summary>
    /// <param name="categorys">资源分类列表</param>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<List<PlcResource>> GetListAsync([FromQuery] List<string> categorys)
    {
        return await _plcResourceService.GetListAsync(categorys);
    }
    /// <summary>
    /// 获取资源列表基于父ID和排序
    /// </summary>
    /// <returns></returns>
    [HttpGet("listBySortCode")]
    public async Task<List<PlcResource>> GetListBySortCodeAsync()
    {
        return await _plcResourceService.GetListBySortCodeAsync();
    }
    /// <summary>
    /// 根据分类获取资源列表
    /// </summary>
    /// <param name="category">分类名称</param>
    /// <returns>资源列表</returns>
    [HttpGet("listByCategory")]
    public async Task<List<PlcResource>> GetListByCategory([FromQuery] string category)
    {
        return await _plcResourceService.GetListByCategory(category);
    }
    /// <summary>
    /// 计算当前Id下所有数据所占地址长度
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("lenght")]
    public async Task<int> GetLenghAsync([FromQuery] long id)
    {
        return await _plcResourceService.GetLenghAsync(id);
    }
    /// <summary>
    /// 资源复制
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("copy")]
    public async Task Copy([FromBody] PlcResourceCopyInput input)
    {
        await _plcResourceService.Copy(input);
    }
    /// <summary>
    /// 重命名资源复制
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("changedNameCopy")]
    public async Task ChangedNameCopy([FromBody] PlcResourceCopyInput input)
    {
        await _plcResourceService.ChangedNameCopy(input);
    }
    /// <summary>
    /// 资源复制
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("getCopy")]
    public async Task<List<PlcResource>> GetCopy([FromBody] PlcResourceCopyInput input)
    {
        return await _plcResourceService.GetCopy(input);
    }
    /// <summary>  
    /// 资源剪切
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("cut")]
    public async Task Cut([FromBody] PlcResourceCopyInput input)
    {
        await _plcResourceService.Cut(input);
    }
}
