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
* 创建时间：2023/11/10 9:58:35
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

[ApiDescriptionSettings("Application", Tag = "载具穴位")]
[Route("mac/[controller]")]
public class PointController : AllowAnonymousController
{
    private readonly IMacPointService _pointService;

    public PointController(
        IMacPointService pointService
        )
    {
        _pointService = pointService;
    }

    /// <summary>
    /// 添加位置
    /// </summary>
    /// <param name="input">添加位置</param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task Add([FromBody] PointAddInput input)
    {
        await _pointService.Add(input);
    }


    /// <summary>
    /// 删除位置
    /// </summary>
    /// <param name="input">id列表</param>
    /// <returns></returns>
    [HttpPost("delete")]
    public async Task Delete([FromBody] List<BaseIdInput> input)
    {
        await _pointService.Delete(input);
    }

    /// <summary>
    /// 编辑位置
    /// </summary>
    /// <param name="input">编辑位置</param>
    /// <returns></returns>
    [HttpPost("edit")]
    public async Task Edit([FromBody] PointEditInput input)
    {
        await _pointService.Edit(input);
    }

    /// <summary>
    /// 获取位置列表
    /// </summary>
    /// <returns>位置列表</returns>
    [HttpGet("list")]
    public async Task<List<MacPoint>> List()
    {
        return await _pointService.GetListAsync();
    }

    /// <summary>
    /// 位置分页查询
    /// </summary>
    /// <param name="input">查询位置</param>
    /// <returns>分页列表</returns>
    [HttpGet("page")]
    public async Task<dynamic> Page([FromQuery]PointPageInput input)
    {
        return await _pointService.Page(input);
    }
}
