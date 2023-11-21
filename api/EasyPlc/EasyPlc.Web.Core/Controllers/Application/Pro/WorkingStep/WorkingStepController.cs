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
* 创建时间：2023/11/10 14:56:47
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

[ApiDescriptionSettings("Application", Tag = "工作步")]
[Route("pro/[controller]")]
public class WorkingStepController : AllowAnonymousController
{
    private readonly IProWorkingStepService _workingStepService;

    public WorkingStepController(
        IProWorkingStepService workingStepService
        )
    {
        _workingStepService = workingStepService;
    }

    /// <summary>
    /// 获取所有加工过程
    /// </summary>
    /// <returns>加工过程列表</returns>
    [HttpGet("list")]
    public async Task<List<ProWorkingStep>> List()
    {
        return await _workingStepService.GetListAsync();
    }
    /// <summary>
    /// 获取加工过程，根据ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("workingStepById")]
    public async Task<ProWorkingStep> GetWorkingStepById([FromQuery]long id)
    {
        return await _workingStepService.GetWorkingStepById(id);
    }
    /// <summary>
    /// 获取加工过程，根据工单ID
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpGet("listByOrderId")]
    public async Task<List<ProWorkingStep>> GetListByOrderId([FromQuery]long orderId)
    {
        return await _workingStepService.GetListByOrderId(orderId);
    }
    /// <summary>
    /// 获取加工过程，根据工单ID，分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("pageByOrderId")]
    public async Task<dynamic> PageByOrderId([FromQuery] ProOrderPageInput input)
    {
        return await _workingStepService.PageByOrderId(input);
    }
    /// <summary>
    /// 获取加工过程，根据ID，分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("pageById")]
    public async Task<dynamic> PageById([FromQuery] ProPageInput input)
    {
        return await _workingStepService.PageById(input);
    }
    /// <summary>
    /// 删除，根据ID，并删除临时数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("deleteAndDataTmp")]
    public async Task DeleteAndDataTmp([FromBody] List<BaseIdInput> input)
    {
        await _workingStepService.DeleteAndDataTmp(input);
    }
}
