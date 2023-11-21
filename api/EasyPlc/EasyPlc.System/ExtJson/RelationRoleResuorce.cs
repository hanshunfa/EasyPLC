﻿namespace EasyPlc.System;

/// <summary>
/// SYS_ROLE_HAS_RESOURCE
/// 角色有哪些资源扩展
/// </summary>
public class RelationRoleResuorce
{
    /// <summary>
    /// 菜单ID
    /// </summary>
    public long MenuId { get; set; }

    /// <summary>
    /// 按钮信息
    /// </summary>
    public List<long> ButtonInfo { get; set; } = new List<long>();
}