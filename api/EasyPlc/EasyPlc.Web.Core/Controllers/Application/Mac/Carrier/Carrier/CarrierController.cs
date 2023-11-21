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
* 创建时间：2023/11/10 9:49:52
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

[ApiDescriptionSettings("Application", Tag = "载具")]
[Route("mac/[controller]")]
public class CarrierController : AllowAnonymousController
{
    private readonly IMacCarrierService _carrierService;

    public CarrierController(
        IMacCarrierService carrierService
        )
    {
        _carrierService = carrierService;
    }
    /// <summary>
    /// 添加载具
    /// </summary>
    /// <param name="input">添加载具</param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task Add([FromBody]CarrierAddInput input)
    {
        await _carrierService.Add(input);
    }

    /// <summary>
    /// 删除载具
    /// </summary>
    /// <param name="input">id列表</param>
    /// <returns></returns>
    [HttpPost("delete")]
    public async Task Delete([FromBody]List<BaseIdInput> input)
    {
        await _carrierService.Delete(input);
    }

    /// <summary>
    /// 编辑载具
    /// </summary>
    /// <param name="input">编辑载具</param>
    /// <returns></returns>
    [HttpPost("edit")]
    public async Task Edit([FromBody]CarrierEditInput input)
    {
        await _carrierService.Edit(input);
    }

    /// <summary>
    /// 获取载具列表
    /// </summary>
    /// <returns>载具列表</returns>
    [HttpGet("list")]
    public async Task<List<MacCarrier>> List()
    {
        return await _carrierService.GetListAsync();
    }

    /// <summary>
    /// 获取载具列表根据型号ID
    /// </summary>
    /// <param name="modelId"></param>
    /// <returns></returns>
    [HttpGet("listByModelId")]
    public async Task<List<MacCarrier>> GetListByModelId([FromQuery]long modelId)
    {
        return await _carrierService.GetListByModelId(modelId);
    }
    /// <summary>
    /// 载具分页查询
    /// </summary>
    /// <param name="input">查询载具</param>
    /// <returns>分页列表</returns>
    [HttpGet("page")]
    public async Task<dynamic> Page([FromQuery]CarrierPageInput input)
    {
        return await _carrierService.Page(input);
    }
}
