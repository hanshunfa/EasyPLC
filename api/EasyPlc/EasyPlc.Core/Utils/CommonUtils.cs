﻿namespace EasyPlc.Core.Utils;

/// <summary>
/// 公共功能
/// </summary>
public static class CommonUtils
{
    /// <summary>
    /// 获取唯一Id
    /// </summary>
    /// <returns></returns>
    public static long GetSingleId()
    {
        return YitIdHelper.NextId();
    }
}