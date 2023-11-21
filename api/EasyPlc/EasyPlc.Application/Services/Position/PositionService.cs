﻿namespace EasyPlc.Application;

/// <summary>
/// <inheritdoc cref="IPositionService"/>
/// </summary>
public class PositionService : DbRepository<SysPosition>, IPositionService
{
    private readonly ISysUserService _sysUserService;
    private readonly ISysPositionService _sysPositionService;

    public PositionService(ISysUserService sysUserService, ISysPositionService sysPositionService)
    {
        _sysUserService = sysUserService;
        _sysPositionService = sysPositionService;
    }

    /// <inheritdoc />
    public async Task<SqlSugarPagedList<SysPosition>> Page(PositionPageInput input)
    {
        //获取数据范围
        var dataScope = await _sysUserService.GetLoginUserApiDataScope();
        input.OrgIds = dataScope;
        //分页查询
        var pageInfo = await _sysPositionService.Page(input);
        return pageInfo;
    }

    /// <inheritdoc />
    public async Task Add(PositionAddInput input)
    {
        await CheckInput(input, EasyPlcConst.Add);//检查参数
        await _sysPositionService.Add(input, EasyPlcConst.BizPos);//添加岗位
    }

    /// <inheritdoc />
    public async Task Delete(List<BaseIdInput> input)
    {
        //获取所有ID
        var ids = input.Select(it => it.Id).ToList();
        //获取数据范围
        var dataScope = await _sysUserService.GetLoginUserApiDataScope();
        if (dataScope.Count > 0)//如果有机构
        {
            //获取职位下所有机构ID
            var orgIds = (await _sysPositionService.GetListAsync()).Where(it => ids.Contains(it.Id)).Select(it => it.OrgId).ToList();
            if (!dataScope.ContainsAll(orgIds))
                throw Oops.Bah($"您没有权限删除这些岗位");
        }
        else
        {
            //获取要删除的岗位列表
            var positions = (await _sysPositionService.GetListAsync()).Where(it => ids.Contains(it.Id)).ToList();
            //如果岗位列表里有任何不是自己创建的岗位
            if (positions.Any(it => it.CreateUserId != UserManager.UserId))
                throw Oops.Bah($"只能删除自己创建的岗位");
        }
        await _sysPositionService.Delete(input, EasyPlcConst.BizOrg);//删除岗位
    }

    /// <inheritdoc />
    public async Task Edit(PositionEditInput input)
    {
        await CheckInput(input, EasyPlcConst.Edit);//检查参数
        await _sysPositionService.Edit(input, EasyPlcConst.BizPos);//编辑
    }

    /// <inheritdoc/>
    public async Task<List<SysPosition>> PositionSelector(PositionSelectorInput input)
    {
        //获取数据范围
        var dataScope = await _sysUserService.GetLoginUserApiDataScope();
        input.OrgIds = dataScope;//赋值机构列表
        var result = await _sysPositionService.PositionSelector(input);//查询
        return result;
    }

    #region 方法

    /// <summary>
    /// 检查输入参数
    /// </summary>
    /// <param name="sysPosition">参数</param>
    /// <param name="operate">操作名称</param>
    private async Task CheckInput(SysPosition sysPosition, string operate)
    {
        var errorMessage = $"您没有权限在该机构下{operate}岗位";
        //获取数据范围
        var dataScope = await _sysUserService.GetLoginUserApiDataScope();
        if (dataScope.Count > 0)//如果有机构
        {
            if (!dataScope.Contains(sysPosition.OrgId))//判断机构ID是否在数据范围
                throw Oops.Bah(errorMessage);
        }
        else
        {
            //如果id大于0表示编辑
            if (sysPosition.Id > 0)
            {
                var position = await _sysPositionService.GetSysPositionById(sysPosition.Id);//获取机构
                if (position.CreateUserId != UserManager.UserId) throw Oops.Bah(errorMessage);//岗位的创建人不是自己则报错
            }
            else
            {
                throw Oops.Bah(errorMessage);
            }
        }
    }

    #endregion 方法
}