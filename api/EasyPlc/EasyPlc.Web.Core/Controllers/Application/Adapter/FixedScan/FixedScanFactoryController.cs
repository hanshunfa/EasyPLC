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
* 创建时间：2023/11/13 9:57:36
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


using EasyPlc.Plugin.Scan;

namespace EasyPlc.Web.Core;

[ApiDescriptionSettings("Application", Tag = "固定扫码器工厂")]
[Route("adapter/[controller]")]
public class FixedScanFactoryController : AllowAnonymousController
{
    private readonly IFixedScanFactoryService _fixedScanFactoryService;

    public FixedScanFactoryController(
        IFixedScanFactoryService fixedScanFactoryService
        )
    {
        _fixedScanFactoryService = fixedScanFactoryService;
    }

    /// <summary>
    /// 初始化工厂
    /// </summary>
    /// <param name="qz">强制初始化工厂</param>
    /// <returns></returns>
    [HttpPost("init")]
    public async Task InitFactory([FromQuery] bool qz)
    {
        await _fixedScanFactoryService.InitFactory(qz);
    }

    /// <summary>
    /// 获取当前扫码器
    /// </summary>
    /// <returns></returns>
    [HttpGet("connList")]
    public async Task<List<FixedScan>> GetConnections()
    {
        return await Task.FromResult(_fixedScanFactoryService.GetConnections().Select(it => it.FixedScan).ToList());
    }
    /// <summary>
    /// 读取扫码器
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="readLen"></param>
    /// <param name="resStr"></param>
    /// <returns></returns>
    [HttpGet("read")]
    public async Task<FixedScanFactoryOutput> ReadScan([FromQuery] string name)
    {
        FixedScanFactoryOutput output = new FixedScanFactoryOutput();
        string readContent = string.Empty;
        output.IsSuccesd = _fixedScanFactoryService.ReadScan(name, ref readContent);
        output.ReadContent = readContent;
        return await Task.FromResult(output);
    }
    /// <summary>
    /// 获取读写日志
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    [HttpGet("log")]
    public async Task<string> GetScanLog([FromQuery] string name)
    {
        return await Task.FromResult(_fixedScanFactoryService.GetScanLog(name));
    }
}
