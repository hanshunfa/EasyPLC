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
* 创建时间：2023/11/13 11:38:40
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


using EasyPlc.Plugin.SygoleRFID;

namespace EasyPlc.Web.Core;


[ApiDescriptionSettings("Application", Tag = "思谷RFID工厂")]
[Route("adapter/[controller]")]
public class SygoleRfidFactoryController : AllowAnonymousController
{
    private readonly ISygoleFactoryService _sygoleFactoryService;

    public SygoleRfidFactoryController(
        ISygoleFactoryService sygoleFactoryService
        )
    {
        _sygoleFactoryService = sygoleFactoryService;
    }
    /// <summary>
    /// 初始化工厂
    /// </summary>
    /// <param name="qz">强制初始化工厂</param>
    /// <returns></returns>
    [HttpPost("init")]
    public async Task InitFactory([FromBody] bool qz)
    {
        await _sygoleFactoryService.InitFactory(qz);
    }

    /// <summary>
    /// 获取当前RFID
    /// </summary>
    /// <returns></returns>
    [HttpGet("connections")]
    public async Task<List<SygoleRfid>> GetConnections()
    {
        return await Task.FromResult(_sygoleFactoryService.GetConnections().Select(it => it.RfidSygole).ToList());
    }
    /// <summary>
    /// 读取RFID
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("read")]
    public async Task<SygoleRfidFactoryReadOutput> ReadRFID([FromQuery] SygoleRfidFactoryReadInput input)
    {
        SygoleRfidFactoryReadOutput output = new SygoleRfidFactoryReadOutput();
        string readContent = string.Empty;
        output.IsSuccesd = _sygoleFactoryService.ReadRFID(input.Name, input.ReadLen, ref readContent);
        output.ReadContent = readContent;
        return await Task.FromResult(output);
    }
    /// <summary>
    /// 写RFID
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("wirte")]
    public async Task<BaseResultOutput> WriteRFID([FromBody] SygoleRfidFactoryWriteInput input)
    {
        BaseResultOutput output = new BaseResultOutput();
        string msg = string.Empty;
        output.IsSuccesd = _sygoleFactoryService.WriteRFID(input.Name, input.WirteContent, ref msg);
        output.Message = msg;
        return await Task.FromResult(output);
    }
    /// <summary>
    /// 获取读写日志
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("log")]
    public async Task<string> GetRFIDLog([FromQuery]string name)
    {
        return await Task.FromResult(_sygoleFactoryService.GetRFIDLog(name));
    }
}
