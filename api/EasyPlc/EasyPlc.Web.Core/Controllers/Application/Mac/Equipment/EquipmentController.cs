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
* 创建时间：2023/11/10 10:04:27
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


[ApiDescriptionSettings("Application", Tag = "设备台账")]
[Route("mac/[controller]")]
public class EquipmentController : AllowAnonymousController
{
    private readonly IMacEquipmentService _equipmentService;

    public EquipmentController(
        IMacEquipmentService equipmentService
        )
    {
        _equipmentService = equipmentService;
    }
    #region 查询
    /// <summary>
    /// 设备详情
    /// </summary>
    /// <param name="input">id参数</param>
    /// <returns>设备信息</returns>
    [HttpGet("detail")]
    public async Task<MacEquipment> Detail([FromQuery]BaseIdInput input)
    {
        return await _equipmentService.Detail(input);
    }
    /// <summary>
    /// 获取所有设备
    /// </summary>
    /// <returns>设备列表</returns>
    [HttpGet("list")]
    public async Task<List<MacEquipment>> List()
    {
        return await _equipmentService.GetListAsync();
    }
    /// <summary>
    /// 根据流程Id获取设备集合
    /// </summary>
    /// <param name="flowId"></param>
    /// <returns></returns>
    [HttpGet("listByFlowId")]
    public async Task<List<MacEquipment>> GetEquipmentListByFlowId([FromQuery] long flowId)
    {
        return await _equipmentService.GetEquipmentListByFlowId(flowId);
    }
    /// <summary>
    /// 获取所有设备带排序
    /// </summary>
    /// <returns>设备列表</returns>
    [HttpGet("listBySortCode")]
    public async Task<List<MacEquipment>> GetListBySortCodeAsync()
    {
        return await _equipmentService.GetListBySortCodeAsync();
    }
    /// <summary>
    /// 获取设备信息
    /// </summary>
    /// <param name="id">设备id</param>
    /// <returns>设备信息</returns>
    [HttpGet("equipmentById")]
    public async Task<MacEquipment> GetEquipmentById([FromQuery] long id)
    {
        return await _equipmentService.GetEquipmentById(id);
    }
    /// <summary>
    /// 设备分页查询
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页信息</returns>
    [HttpGet("page")]
    public async Task<dynamic> Page([FromQuery] MacEquipmentPageInput input)
    {
        return await _equipmentService.Page(input);
    }
    #endregion

    #region 新增
    /// <summary>
    /// 添加设备
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task Add([FromBody] MacEquipmentAddInput input)
    {
        await _equipmentService.Add(input);
    }

    /// <summary>
    /// 复制设备
    /// </summary>
    /// <param name="input">机构复制参数</param>
    /// <returns></returns>
    [HttpPost("copy")]
    public async Task Copy([FromBody]MacEquipmentCopyInput input)
    {
        await _equipmentService.Copy(input);
    }
    #endregion

    #region 编辑
    /// <summary>
    /// 编辑设备
    /// </summary>
    /// <param name="input">编辑参数</param>
    /// <returns></returns>
    [HttpPost("edit")]
    public async Task Edit([FromBody]MacEquipmentEditInput input)
    {
        await _equipmentService.Edit(input);
    }
    #endregion

    #region 删除
    /// <summary>
    /// 删除设备
    /// </summary>
    /// <param name="input">删除参数</param>
    /// <returns></returns>
    [HttpPost("delete")]
    public async Task Delete([FromBody] List<BaseIdInput> input)
    {
        await _equipmentService.Delete(input);
    }
    #endregion
}
