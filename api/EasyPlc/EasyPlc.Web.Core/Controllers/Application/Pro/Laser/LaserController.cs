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
* 创建时间：2023/11/10 14:04:06
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

[ApiDescriptionSettings("Application", Tag = "镭射")]
[Route("pro/[controller]")]
public class LaserController : AllowAnonymousController
{
    private readonly IProLaserService _laserService;

    public LaserController(
        IProLaserService laserService
        )
    {
        _laserService = laserService;
    }

    /// <summary>
    /// 查询所有工单
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<List<ProLaser>> List()
    {
        return await _laserService.GetListAsync();
    }
    /// <summary>
    /// 根据ID查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("laserById")]
    public async Task<ProLaser> GetProLaserById([FromQuery]  int id)
    {
        return await _laserService.GetProLaserById(id);
    }
    /// <summary>
    /// 根据工单ID查询
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpGet("laserByOrderId")]
    public async Task<ProLaser> GetProLaserByOrderId([FromQuery]  long orderId)
    {
        return await _laserService.GetProLaserByOrderId(orderId);
    }
    /// <summary>
    /// 获取当前预览结果
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpGet("currentPreview")]
    public async Task<ProLaser> GetCurrentPreview([FromQuery] long orderId)
    {
        return await _laserService.GetCurrentPreview(orderId);
    }
}
