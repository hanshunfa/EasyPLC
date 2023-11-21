﻿namespace EasyPlc.Core;

/// <summary>
/// 文件上传输入参数
/// </summary>
public class BaseFileInput
{
    /// <summary>
    /// 文件
    /// </summary>
    [Required(ErrorMessage = "文件不能为空")]
    public IFormFile File { get; set; }
}