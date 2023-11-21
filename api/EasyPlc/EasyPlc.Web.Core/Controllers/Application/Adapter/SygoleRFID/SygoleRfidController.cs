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
* 创建时间：2023/11/9 17:10:05
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

[ApiDescriptionSettings("Application", Tag = "思谷RFID")]
[Route("adapter/[controller]")]
public class SygoleRfidController : AllowAnonymousController
{
    private readonly ISygoleRfidService _sygoleRfidService;

    public SygoleRfidController(
        ISygoleRfidService sygoleRfidService
        )
    {
        _sygoleRfidService = sygoleRfidService;
    }
    #region  查询
    /// <summary>
    /// 查询所有RFID
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<List<SygoleRfid>> List()
    {
        return await _sygoleRfidService.GetListAsync();
    }
    #endregion

    #region 新增
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task Add([FromBody] SygoleRfidAddInput input)
    {
        await _sygoleRfidService.Add(input);
    }
    #endregion

    #region 编辑
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("edit")]
    public async Task Edit([FromBody] SygoleRfidAddInput input)
    {
        await _sygoleRfidService.Edit(input);
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
        await _sygoleRfidService.Delete(input);
    }
    #endregion
}
