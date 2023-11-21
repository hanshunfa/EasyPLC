﻿namespace EasyPlc.System;

/// <summary>
/// 事件总线常量
/// </summary>
public class EventSubscriberConst
{
    #region AuthEventSubscriber

    /// <summary>
    /// B端登录
    /// </summary>
    public const string LoginB = "B端登录";

    /// <summary>
    /// B端登录
    /// </summary>
    public const string LoginOutB = "B端登出";
    /// <summary>
    /// B端登录
    /// </summary>
    public const string LoginC = "C端登录";

    /// <summary>
    /// B端登录
    /// </summary>
    public const string LoginOutC = "C端登出";

    #endregion AuthEventSubscriber

    #region UserEventSubscriber

    /// <summary>
    /// 清除用户缓存
    /// </summary>
    public const string ClearUserCache = "清除用户缓存";

    #endregion UserEventSubscriber

    #region NoticeEventSubscriber

    /// <summary>
    /// 通知用户下线
    /// </summary>
    public const string UserLoginOut = "通知用户下线";

    /// <summary>
    /// 新消息
    /// </summary>
    public const string NewMessage = "新消息";

    #endregion NoticeEventSubscriber
}