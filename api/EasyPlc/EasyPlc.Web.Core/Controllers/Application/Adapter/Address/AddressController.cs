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
* 创建时间：2023/11/10 15:23:43
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

[ApiDescriptionSettings("Application", Tag = "地址")]
[Route("adapter/[controller]")]
public class AddressController : AllowAnonymousController
{
    private readonly IAddressService _addressService;

    public AddressController(
        IAddressService addressService
        )
    {
        _addressService = addressService;
    }

    #region 查询
    /// <summary>
    /// 查询所有地址
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<List<PlcAddress>> List()
    {
        return await _addressService.GetListAsync();
    }
    /// <summary>
    /// 通过Id查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("dddressById")]
    public async Task<PlcAddress> GetAddressById([FromQuery]long id)
    {
        return await _addressService.GetAddressById(id);
    }
    /// <summary>
    /// 根据PLC ID获取地址
    /// </summary>
    /// <param name="plcId"></param>
    /// <returns></returns>
    [HttpGet("listByPlcId")]
    public async Task<List<PlcAddress>> GetListByPlcId([FromQuery]long plcId)
    {
        return await _addressService.GetListByPlcId(plcId);
    }
    /// <summary>
    /// 根据PLC ID获取数据资源
    /// </summary>
    /// <param name="plcId"></param>
    /// <returns></returns>
    [HttpGet("resourceListByPlcId")]
    public async Task<List<PlcResource>> GetResourceListByPlcId([FromQuery] long plcId)
    {
        return await _addressService.GetResourceListByPlcId(plcId);
    }
    #endregion

    #region 新增
    /// <summary>
    /// 添加地址
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task Add([FromBody]AddressAddInput input)
    {
        await _addressService.Add(input);
    }

    #endregion

    #region 编辑
    /// <summary>
    /// 编辑地址
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("edit")]
    public async Task Edit([FromBody] AddressEditInput input)
    {
        await _addressService.Edit(input);
    }
    /// <summary>
    /// 排序
    /// </summary>
    /// <returns></returns>
    [HttpPost("sort")]
    public async Task Sort([FromBody] AddressSortInput input)
    {
        await _addressService.Sort(input);
    }
    #endregion

    #region 删除
    /// <summary>
    /// 删除地址
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("delete")]
    public async Task Delete([FromBody] List<BaseIdInput> input)
    {
        await _addressService.Delete(input);
    }
    #endregion
}
