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
* 创建时间：2023/11/10 13:47:45
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

[ApiDescriptionSettings("Application", Tag = "临时数据")]
[Route("pro/[controller]")]
public class DataTmpController : AllowAnonymousController
{
    private readonly IProDataTmpService _dataTmpService;

    public DataTmpController(
        IProDataTmpService dataTmpService
        )
    {
        _dataTmpService = dataTmpService;
    }

    /// <summary>
    /// 获取所有过程数据
    /// </summary>
    /// <returns>加工过程数据列表</returns>
    [HttpGet("list")]
    public async Task<List<ProDataTmp>> List()
    {
        return await _dataTmpService.GetListAsync();
    }
    /// <summary>
    /// 获取数据，根据ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("dataTmpById")]
    public async Task<ProDataTmp> GetDataTmpById([FromQuery]long id)
    {
        return await _dataTmpService.GetDataTmpById(id);
    }
    /// <summary>
    /// 分页查询，根据工单号
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("pageByOrderId")]
    public async Task<dynamic> PageByOrderId([FromQuery]ProOrderPageInput input)
    {
        return await _dataTmpService.PageByOrderId(input);
    }
}
