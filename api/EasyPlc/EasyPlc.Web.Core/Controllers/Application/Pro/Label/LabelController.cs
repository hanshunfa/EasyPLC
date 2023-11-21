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
* 创建时间：2023/11/10 13:58:28
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

[ApiDescriptionSettings("Application", Tag = "标签")]
[Route("pro/[controller]")]
public class LabelController : AllowAnonymousController
{
    private readonly IProLabelService _labelService;

    public LabelController(
        IProLabelService labelService
        )
    {
        _labelService = labelService;
    }

    /// <summary>
    /// 查询所有标签
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<List<ProLabel>> List()
    {
        return await _labelService.GetListAsync();
    }
    /// <summary>
    /// 根据ID查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("labelById")]
    public async Task<ProLabel> GetProLabelById([FromQuery]int id)
    {
        return await _labelService.GetProLabelById(id);
    }
    /// <summary>
    /// 根据工单ID查询
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpGet("labelByOrderId")]
    public async Task<ProLabel> GetProLabelByOrderId([FromQuery]long orderId)
    {
        return await _labelService.GetProLabelByOrderId(orderId);
    }
    /// <summary>
    /// 获取当前预览结果
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpGet("currentPreview")]
    public async Task<ProLabel> GetCurrentPreview([FromQuery] long orderId)
    {
        return await _labelService.GetCurrentPreview(orderId);
    }
}
