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
* 创建时间：2023/11/10 10:49:12
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

[ApiDescriptionSettings("Application", Tag = "型号")]
[Route("mac/[controller]")]
public class ModelController : AllowAnonymousController
{
    private readonly IMacModelService _modelService;

    public ModelController(
        IMacModelService modelService
        )
    {
        _modelService = modelService;
    }

    #region 查询
    /// <summary>
    /// 型号详情
    /// </summary>
    /// <param name="input">id参数</param>
    /// <returns>组织信息</returns>
    [HttpGet("detail")]
    public async Task<MacModel> Detail([FromQuery]BaseIdInput input)
    {
        return await _modelService.Detail(input);
    }
    /// <summary>
    /// 获取所有型号
    /// </summary>
    /// <returns>型号列表</returns>
    [HttpGet("list")]
    public async Task<List<MacModel>> List()
    {
        return await _modelService.GetListAsync();
    }
    /// <summary>
    /// 获取所有型号带排序
    /// </summary>
    /// <returns>型号列表</returns>
    [HttpGet("listBySortCode")]
    public async Task<List<MacModel>> GetListBySortCodeAsync()
    {
        return await _modelService.GetListBySortCodeAsync();
    }
    /// <summary>
    /// 获取型号信息
    /// </summary>
    /// <param name="id">型号id</param>
    /// <returns>型号信息</returns>
    [HttpGet("modelById")]
    public async Task<MacModel> GetMacModelById([FromQuery]long id)
    {
        return await _modelService.GetMacModelById(id);
    }
    /// <summary>
    /// 获取型号信息
    /// </summary>
    /// <param name="name">型号name</param>
    /// <returns>型号信息</returns>
    [HttpGet("modelByName")]
    public async Task<MacModel> GetMacModelByName([FromQuery]string name)
    {
        return await _modelService.GetMacModelByName(name);
    }
    /// <summary>
    /// 型号分页查询
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页信息</returns>
    [HttpGet("page")]
    public async Task<dynamic> Page([FromQuery]MacModelPageInput input)
    {
        return await _modelService.Page(input);
    }
    #endregion

    #region 新增
    /// <summary>
    /// 添加型号
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task Add([FromBody]MacModelAddInput input)
    {
        await _modelService.Add(input);
    }

    /// <summary>
    /// 复制型号
    /// </summary>
    /// <param name="input">机构复制参数</param>
    /// <returns></returns>
    [HttpPost("copy")]
    public async Task Copy([FromBody]MacModelCopyInput input)
    {
        await _modelService.Copy(input);
    }
    #endregion

    #region 编辑
    /// <summary>
    /// 编辑型号
    /// </summary>
    /// <param name="input">编辑参数</param>
    /// <returns></returns>
    [HttpPost("edit")]
    public async Task Edit([FromBody] MacModelEditInput input)
    {
        await _modelService.Edit(input);
    }
    #endregion

    #region 删除
    /// <summary>
    /// 删除型号
    /// </summary>
    /// <param name="input">删除参数</param>
    /// <returns></returns>
    [HttpPost("delete")]
    public async Task Delete([FromBody]List<BaseIdInput> input)
    {
        await _modelService.Delete(input);
    }
    #endregion
}
