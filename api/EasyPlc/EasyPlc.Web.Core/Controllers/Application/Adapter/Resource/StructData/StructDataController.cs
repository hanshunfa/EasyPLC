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
* 创建时间：2023/11/10 9:24:19
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
[ApiDescriptionSettings("Application", Tag = "结构数据资源类型")]
[Route("adapter/[controller]")]
public class StructDataController : AllowAnonymousController
{
    private readonly IStructDataService _structDataService;

    public StructDataController(
        IStructDataService structDataService
        )
    {
        _structDataService = structDataService;
    }
    /// <summary>
    /// 基础数据分页查询
    /// </summary>
    /// <param name="input">查询条件</param>
    /// <returns>基础数据分页列表</returns>
    [HttpGet("page")]
    public async Task<dynamic> Page([FromQuery] StructDataPageInput input)
    {
        return await _structDataService.Page(input);
    }
    /// <summary>
    /// 查询所有机构
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<List<PlcResource>> GetListAsync()
    {
        return await _structDataService.GetListAsync();
    }

    /// <summary>
    /// 添加基础数据
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task Add([FromBody]StructDataAddInput input)
    {
        await _structDataService.Add(input);
    }

    /// <summary>
    /// 删除基础数据
    /// </summary>
    /// <param name="input">删除参数</param>
    /// <returns></returns>
    [HttpPost("delete")]
    public async Task Delete([FromBody]List<BaseIdInput> input)
    {
        await _structDataService.Delete(input);
    }

    /// <summary>
    /// 编辑基础数据
    /// </summary>
    /// <param name="input">编辑参数</param>
    /// <returns></returns>
    [HttpPost("edit")]
    public async Task Edit([FromBody] StructDataEditInput input)
    {
        await _structDataService.Edit(input);
    }
}
