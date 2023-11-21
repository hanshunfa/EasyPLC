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
* 创建时间：2023/11/13 13:48:56
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

[ApiDescriptionSettings("Application", Tag = "西门子PLC工厂")]
[Route("adapter/[controller]")]
public class SiemensPlcFactoryController : AllowAnonymousController
{
    private readonly ISiemensPlcFactoryService _siemensPlcFactoryService;

    public SiemensPlcFactoryController(
        ISiemensPlcFactoryService siemensPlcFactoryService
        )
    {
        _siemensPlcFactoryService = siemensPlcFactoryService;
    }

    /// <summary>
    /// 初始化工厂，创建PLC通讯实列
    /// </summary>
    [HttpPost("init")]
    public async Task<string> InitFactory()
    {
        return await Task.FromResult(_siemensPlcFactoryService.InitFactory());
    }
    /// <summary>
    /// 全部开始
    /// </summary>
    /// <param name="connectionSiemensPLC"></param>
    /// <returns></returns>
    [HttpPost("start")]
    public async Task StartPLC()
    {
        await Task.Run(()=>_siemensPlcFactoryService.StartPLC());
    }
    /// <summary>
    /// 开始
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    [HttpPost("startByIp")]
    public async Task<string> StartPLC([FromBody] string ip)
    {
        return await Task.FromResult(_siemensPlcFactoryService.StartPLC(ip));
    }
    /// <summary>
    /// 结束
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    [HttpPost("stopByIp")]
    public async Task<string> StopPLC([FromBody] string ip)
    {
        return await Task.FromResult(_siemensPlcFactoryService.StopPLC(ip));
    }
    /// <summary>
    /// 全部结束
    /// </summary>
    /// <returns></returns>
    [HttpPost("stop")]
    public async Task StopPLC()
    {
        await Task.Run(() => _siemensPlcFactoryService.StopPLC());
    }
    /// <summary>
    /// 获取连接PLC
    /// </summary>
    /// <returns></returns>
    [HttpGet("connList")]
    public async Task<List<SiemensPlcInfo>> GetConnectionSiemensPLCList()
    {
        return await Task.FromResult(_siemensPlcFactoryService.GetConnectionSiemensPLCList().Select(it=>it.PlcInfo).ToList());
    }
}
