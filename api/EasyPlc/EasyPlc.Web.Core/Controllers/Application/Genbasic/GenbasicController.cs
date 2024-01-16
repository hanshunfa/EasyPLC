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
* 创建时间：2024/1/2 15:51:03
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



using EasyPlc.Plugin.Gen;

namespace EasyPlc.Web.Core;

[ApiDescriptionSettings("Application", Tag = "地址")]
[Route("biz/[controller]")]
public class GenbasicController : AllowAnonymousController
{
    private readonly IGenbasicService _genbasicService;

    public GenbasicController(
        IGenbasicService genbasicService
        )
    {
        _genbasicService = genbasicService;
    }
    [HttpGet("execGen")]
    public async Task<string> ExecGen()
    {
        return await _genbasicService.ExecGenClassPro();
    }
}
