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
* 创建时间：2023/11/10 13:42:40
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

[ApiDescriptionSettings("Application", Tag = "数据")]
[Route("pro/[controller]")]
public class DataController : AllowAnonymousController
{
    private readonly IProDataService _dataService;

    public DataController(
        IProDataService dataService
        )
    {
        _dataService = dataService;
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("pageByOrderId")]
    public async Task<dynamic> PageByOrderId([FromQuery]ProDataPageInput input)
    {
        return await _dataService.PageByOrderId(input);
    }
}
