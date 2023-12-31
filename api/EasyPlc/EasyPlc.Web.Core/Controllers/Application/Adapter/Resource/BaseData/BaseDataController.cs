﻿/*=============================================================================================
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
* 创建时间：2023/11/10 9:18:50
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

[ApiDescriptionSettings("Application", Tag = "基础数据资源类型")]
[Route("adapter/[controller]")]
public class BaseDataController : AllowAnonymousController
{
    private readonly IBaseDataService _baseDataService;

    public BaseDataController(
        IBaseDataService baseDataService
        )
    {
        _baseDataService = baseDataService;
    }

    /// <summary>
    /// 基础数据分页查询
    /// </summary>
    /// <param name="input">查询条件</param>
    /// <returns>基础数据分页列表</returns>
    [HttpGet("page")]
    public async Task<dynamic> Page([FromQuery]BaseDataPageInput input)
    {
        return await _baseDataService.Page(input);
    }

    /// <summary>
    /// 添加基础数据
    /// </summary>
    /// <param name="input">添加参数</param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task Add([FromBody]BaseDataAddInput input)
    {
        await _baseDataService.Add(input);
    }

    /// <summary>
    /// 批量添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("addBatch")]
    public async Task<List<long>> AddBatch([FromBody] BaseDataAddInput input)
    {
        return await _baseDataService.AddBatch(input);
    }

    /// <summary>
    /// 编辑基础数据
    /// </summary>
    /// <param name="input">编辑参数</param>
    /// <returns></returns>
    [HttpPost("edit")]
    public async Task Edit([FromBody] BaseDataEditInput input)
    {
        await _baseDataService.Edit(input);
    }

    /// <summary>
    /// 删除基础数据
    /// </summary>
    /// <param name="input">删除参数</param>
    /// <returns></returns>
    [HttpPost("delete")]
    public async Task Delete([FromBody] List<BaseIdInput> input)
    {
        await _baseDataService.Delete(input);
    }
}
