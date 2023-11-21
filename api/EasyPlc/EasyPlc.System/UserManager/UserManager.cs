using EasyPlc.Core.UserInfo;

namespace EasyPlc.System;

/// <summary>
/// 当前登录用户信息
/// </summary>
public class UserManager
{
    /// <summary>
    /// 当前用户Id
    /// </summary>
    public static long UserId => LoginUser.UserId.ToLong();

    /// <summary>
    /// 当前用户账号
    /// </summary>
    public static string UserAccount => LoginUser.Account;

    /// <summary>
    /// 当前用户昵称
    /// </summary>
    public static string Name => LoginUser.Name;

    /// <summary>
    /// 是否超级管理员
    /// </summary>
    public static bool SuperAdmin => LoginUser.IsSuperAdmin;

    /// <summary>
    /// 机构ID
    /// </summary>
    public static long OrgId => LoginUser.OrgId.ToLong();
}