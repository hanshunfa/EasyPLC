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
* 创建时间：2023/11/9 16:40:20
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

[ApiDescriptionSettings("Application", Tag = "PLC配置")]
[Route("adapter/[controller]")]
public class PlcConfigController : AllowAnonymousController
{
    private readonly IPlcConfigService _plcConfigService;

    public PlcConfigController(
        IPlcConfigService plcConfigService
        )
    {
        _plcConfigService = plcConfigService;
    }

    #region 查询

    /// <summary>
    /// PLC详情
    /// </summary>
    /// <param name="input">id参数</param>
    /// <returns>PLC信息</returns>
    [HttpGet("detail")]
    public async Task<PlcConfig> Detail([FromQuery] BaseIdInput input)
    {
        return await _plcConfigService.Detail(input);
    }

    /// <summary>
    /// 获取所有PLC
    /// </summary>
    /// <returns>PLC列表</returns>
    [HttpGet("list")]
    public async Task<List<PlcConfig>> List()
    {
        return await _plcConfigService.GetListAsync();
    }

    /// <summary>
    /// 获取所有PLC带排序
    /// </summary>
    /// <returns>PLC列表</returns>
    [HttpGet("listBySortCode")]
    public async Task<List<PlcConfig>> GetListBySortCodeAsync()
    {
        return await _plcConfigService.GetListBySortCodeAsync();
    }


    /// <summary>
    /// PLC分页查询
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页信息</returns>
    [HttpGet("page")]
    public async Task<dynamic> Page([FromQuery] PlcConfigPageInput input)
    {
        return await _plcConfigService.Page(input);
    }


    #endregion 查询

    #region 新增

    /// <summary>
    /// 添加PLC
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task Add([FromBody] PlcConfigAddInput input)
    {
        await _plcConfigService.Add(input);
    }
    /// <summary>
    /// 添加PLC
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <returns></returns>
    [HttpPost("addReturnSnowflakeId")]
    public async Task<long> AddReturnSnowflakeId([FromBody] PlcConfigAddInput input)
    {
        return await _plcConfigService.AddReturnSnowflakeId(input);
    }
    /// <summary>
    /// 复制PLC
    /// </summary>
    /// <param name="input">机构复制参数</param>
    /// <returns></returns>
    [HttpPost("copy")]
    public async Task Copy([FromBody] PlcConfigCopyInput input)
    {
        await _plcConfigService.Copy(input);
    }

    #endregion 新增

    #region 编辑

    /// <summary>
    /// 编辑PLC
    /// </summary>
    /// <param name="input">编辑参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    [HttpPost("edit")]
    public async Task Edit([FromBody] PlcConfigEditInput input)
    {
        await _plcConfigService.Edit(input);
    }

    #endregion 编辑

    #region 删除

    /// <summary>
    /// 删除PLC
    /// </summary>
    /// <param name="input">删除参数</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    [HttpPost("delete")]
    public async Task Delete(List<BaseIdInput> input)
    {
        await _plcConfigService.Delete(input);
    }

    #endregion 删除
}
