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
* 创建时间：2023/11/13 19:39:24
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


using EasyPlc.Plugin.ScrewGun;

namespace EasyPlc.Web.Core;

[ApiDescriptionSettings("Application", Tag = "康沃螺丝枪工厂")]
[Route("adapter/[controller]")]
public class KwScrewGunFactoryController : AllowAnonymousController
{
    private readonly IKwScrewGunFactoryService _kwScrewGunFactoryService;

    public KwScrewGunFactoryController(
        IKwScrewGunFactoryService kwScrewGunFactoryService
        )
    {
        _kwScrewGunFactoryService = kwScrewGunFactoryService;
    }

    /// <summary>
    /// 开始服务
    /// </summary>
    /// <returns></returns>
    [HttpPost("start")]
    public async Task<bool> StartTcpServer()
    {
        return await _kwScrewGunFactoryService.StartTcpServer();
    }
    /// <summary>
    /// 停止服务
    /// </summary>
    /// <returns></returns>
    [HttpPost("stop")]
    public async Task StopTcpServer()
    {
        await Task.Run(() => _kwScrewGunFactoryService.StopTcpServer());
    }
    /// <summary>
    /// 获取螺丝枪
    /// </summary>
    /// <returns></returns>
    [HttpGet("connList")]
    public async Task<List<KwScrewGunOutput>> GetKwScrewGuns()
    {
        return await Task.FromResult(_kwScrewGunFactoryService.GetKwScrewGuns());
    }
    /// <summary>
    /// 获取指定IP螺丝枪最新反馈信息
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    [HttpGet("newRecvInfo")]
    public async Task<RecvOutput> GetNewRecvInfo([FromQuery] string ip)
    {
        return await Task.FromResult(_kwScrewGunFactoryService.GetNewRecvInfo(ip));
    }
}
