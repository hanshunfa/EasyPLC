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
* 创建时间：2023/11/10 11:01:24
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

[ApiDescriptionSettings("Application", Tag = "型号参数")]
[Route("mac/[controller]")]

public class ModelParameterController : AllowAnonymousController
{
    private readonly IMacModelParamService _paramService;

    public ModelParameterController(
        IMacModelParamService paramService
        )
    {
        _paramService = paramService;
    }

    /// <summary>
    /// 添加参数
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task Add([FromBody]ParameterAddInput input)
    {
        await _paramService.Add(input);
    }

    /// <summary>
    /// 删除参数
    /// </summary>
    /// <param name="input">id列表</param>
    /// <returns></returns>
    [HttpPost("delete")]
    public async Task Delete([FromBody]List<BaseIdInput> input)
    {
        await _paramService.Delete(input);
    }

    /// <summary>
    /// 编辑参数
    /// </summary>
    /// <param name="input">编辑参数</param>
    /// <returns></returns>
    [HttpPost("edit")]
    public async Task Edit([FromBody]ParameterEditInput input)
    {
        await _paramService.Edit(input);
    }
    /// <summary>
    /// 拷贝参数
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("copy")]
    public async Task Copy([FromBody]ParameterCopyInput input)
    {
        await _paramService.Copy(input);
    }
    /// <summary>
    /// 获取参数列表
    /// </summary>
    /// <returns>参数列表</returns>
    [HttpGet("list")]
    public async Task<List<MacModelParam>> List()
    {
        return await _paramService.GetListAsync();
    }

    /// <summary>
    /// 获取参数信息
    /// </summary>
    /// <param name="id">参数ID</param>
    /// <returns>参数信息</returns>
    [HttpGet("parameterById")]
    public async Task<MacModelParam> GetMacParameterById([FromQuery]long id)
    {
        return await _paramService.GetMacParameterById(id);
    }

    /// <summary>
    /// 参数分页查询
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页列表</returns>
    [HttpGet("page")]
    public async Task<dynamic> Page([FromQuery]ParameterPageInput input)
    {
        return await _paramService.Page(input);
    }
    /// <summary>
    /// 根据型号ID获取参数列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("listByModelId")]
    public async Task<List<MacModelParam>> GetListByModelId([FromQuery]long id)
    {
        return await _paramService.GetListByModelId(id);
    }
}
