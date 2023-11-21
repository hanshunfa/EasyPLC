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
* 创建时间：2023/11/10 10:17:22
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

[ApiDescriptionSettings("Application", Tag = "流程")]
[Route("mac/[controller]")]
public class FlowController : AllowAnonymousController
{
    private readonly IMacFlowService _flowService;

    public FlowController(
        IMacFlowService flowService
        )
    {
        _flowService = flowService;
    }

    #region 查询
    /// <summary>
    /// 流程详情
    /// </summary>
    /// <param name="input">id参数</param>
    /// <returns>组织信息</returns>
    [HttpGet("detail")]
    public async Task<MacFlow> Detail([FromQuery]BaseIdInput input)
    {
        return await _flowService.Detail(input);
    }
    /// <summary>
    /// 获取所有流程
    /// </summary>
    /// <returns>流程列表</returns>
    [HttpGet("list")]
    public Task<List<MacFlow>> List()
    {
        return _flowService.GetListAsync();
    }
    /// <summary>
    /// 获取所有流程带排序
    /// </summary>
    /// <returns>流程列表</returns>
    [HttpGet("listBySortCode")]
    public async Task <List<MacFlow>> GetListBySortCodeAsync()
    {
        return await _flowService.GetListBySortCodeAsync();
    }
    /// <summary>
    /// 获取流程信息
    /// </summary>
    /// <param name="id">流程id</param>
    /// <returns>流程信息</returns>
    [HttpGet("flowById")]
    public async Task<MacFlow> GetMacFlowById([FromQuery] long id)
    {
        return await _flowService.GetMacFlowById(id);
    }
    /// <summary>
    /// 流程分页查询
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页信息</returns>
    [HttpGet("page")]
    public async Task<dynamic> Page([FromQuery] MacFlowPageInput input)
    {
        return await _flowService.Page(input);
    }
    #endregion

    #region 新增
    /// <summary>
    /// 添加流程
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task Add([FromBody] MacFlowAddInput input)
    {
        await _flowService.Add(input);
    }
    /// <summary>
    /// 添加流程
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <returns></returns>
    [HttpPost("addReturnSnowflakeId")]
    public async Task<long> AddReturnSnowflakeId([FromBody]MacFlowAddInput input)
    {
        return await _flowService.AddReturnSnowflakeId(input);
    }

    /// <summary>
    /// 复制流程
    /// </summary>
    /// <param name="input">机构复制参数</param>
    /// <returns></returns>
    [HttpPost("copy")]
    public async Task Copy([FromBody]MacFlowCopyInput input)
    {
        await _flowService.Copy(input);
    }
    #endregion

    #region 编辑
    /// <summary>
    /// 编辑流程
    /// </summary>
    /// <param name="input">编辑参数</param>
    /// <returns></returns>
    [HttpPost("edit")]
    public async Task Edit([FromBody] MacFlowEditInput input)
    {
        await _flowService.Edit(input);
    }

    /// <summary>
    /// 给流程授权设备
    /// </summary>
    /// <param name="input">授权参数</param>
    /// <returns></returns>
    [HttpPost("grantEuipment")]
    public async Task GrantEuipment([FromBody] FlowGrantEquipmentInput input)
    {
        await _flowService.GrantEuipment(input);
    }
    #endregion

    #region 删除
    /// <summary>
    /// 删除流程
    /// </summary>
    /// <param name="input">删除参数</param>
    /// <returns></returns>
    [HttpPost("delete")]
    public async Task Delete([FromBody]List<BaseIdInput> input)
    {
        await _flowService.Delete(input);
    }
    #endregion
}
