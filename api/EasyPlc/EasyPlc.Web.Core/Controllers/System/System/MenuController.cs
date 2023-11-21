﻿namespace EasyPlc.Web.Core;

/// <summary>
/// 菜单管理控制器
/// </summary>
[ApiDescriptionSettings(Tag = "菜单管理")]
public class MenuController : BaseController
{
    private readonly IMenuService _menuService;
    private readonly IResourceService _resourceService;

    public MenuController(IMenuService menuService, IResourceService resourceService)
    {
        _menuService = menuService;
        _resourceService = resourceService;
    }

    /// <summary>
    /// 模块选择
    /// </summary>
    /// <returns></returns>
    [HttpGet("moduleSelector")]
    public async Task<dynamic> ModuleSelector()
    {
        return await _resourceService.GetListByCategory(CateGoryConst.Resource_MODULE);
    }

    /// <summary>
    /// 获取菜单
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<dynamic> List([FromQuery] MenuTreeInput input)
    {
        return await _menuService.GetListAsync(input); ;
    }

    /// <summary>
    /// 获取菜单树
    /// </summary>
    /// <returns></returns>
    [HttpGet("tree")]
    public async Task<dynamic> Tree([FromQuery] MenuTreeInput input)
    {
        return await _menuService.Tree(input); ;
    }

    /// <summary>
    /// 获取菜单树选择器
    /// </summary>
    /// <returns></returns>
    [HttpGet("menuTreeSelector")]
    public async Task<dynamic> MenuTreeSelector([FromQuery] MenuTreeInput input)
    {
        return await _menuService.Tree(input); ;
    }

    /// <summary>
    /// 添加菜单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("add")]
    [DisplayName("添加菜单")]
    public async Task Add([FromBody] MenuAddInput input)
    {
        await _menuService.Add(input);
    }

    /// <summary>
    /// 编辑菜单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("edit")]
    [DisplayName("编辑菜单")]
    public async Task Edit([FromBody] MenuEditInput input)
    {
        await _menuService.Edit(input);
    }

    /// <summary>
    /// 获取菜单详情
    /// </summary>
    /// <returns></returns>
    [HttpGet("detail")]
    public async Task<dynamic> Detail([FromQuery] BaseIdInput input)
    {
        return await _menuService.Detail(input); ;
    }

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("delete")]
    [DisplayName("删除菜单")]
    public async Task Delete([FromBody] List<BaseIdInput> input)
    {
        await _menuService.Delete(input);
    }

    /// <summary>
    /// 更改模块
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("changeModule")]
    [DisplayName("更改模块")]
    public async Task ChangeModule([FromBody] MenuChangeModuleInput input)
    {
        await _menuService.ChangeModule(input);
    }
}