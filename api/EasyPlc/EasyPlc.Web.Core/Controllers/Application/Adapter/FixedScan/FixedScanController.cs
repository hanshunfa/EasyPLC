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
* 创建时间：2023/11/9 16:12:09
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

[ApiDescriptionSettings("Application", Tag = "固定扫码器")]
[Route("adapter/[controller]")]
public class FixedScanController : AllowAnonymousController
{
    private readonly IFixedScanService _fixedScanService;

    public FixedScanController(
        IFixedScanService fixedScanService
        )
    {
        _fixedScanService = fixedScanService;
    }

    #region  查询
    /// <summary>
    /// 查询所有RFID
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<List<FixedScan>> List()
    {
        return await _fixedScanService.GetListAsync();
    }
    #endregion

    #region 新增
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task Add([FromBody] FixedScanAddInput input)
    {
        await _fixedScanService.Add(input);
    }
    #endregion

    #region 编辑
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpPost("edit")]
    public async Task Edit([FromBody] FixedScanAddInput input)
    {
        await _fixedScanService.Edit(input);
    }
    #endregion

    #region 删除
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("delete")]
    public async Task Delete([FromBody] List<BaseIdInput> input)
    {
        await _fixedScanService.Delete(input);
    }
    #endregion
}

