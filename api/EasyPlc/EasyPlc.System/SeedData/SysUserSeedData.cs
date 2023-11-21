﻿namespace EasyPlc.System;

/// <summary>
/// 用户表种子数据
/// </summary>
public class SysUserSeedData : ISqlSugarEntitySeedData<SysUser>
{
    public IEnumerable<SysUser> SeedData()
    {
        return SeedDataUtil.GetSeedData<SysUser>("seed_sys_user.json");
    }
}