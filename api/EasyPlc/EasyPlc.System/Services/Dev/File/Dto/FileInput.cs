﻿namespace EasyPlc.System;

/// <summary>
/// 文件分页输出
/// </summary>
public class FilePageInput : BasePageInput
{
    /// <summary>
    /// 文件引擎
    /// </summary>
    public string Engine { get; set; }
}