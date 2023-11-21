﻿namespace EasyPlc.System;

/// <summary>
/// 配置分页参数
/// </summary>
public class ConfigPageInput : BasePageInput
{
}

/// <summary>
/// 添加配置参数
/// </summary>
public class ConfigAddInput : DevConfig
{
    /// <summary>
    /// 配置键
    /// </summary>
    [Required(ErrorMessage = "configKey不能为空")]
    public override string ConfigKey { get; set; }

    /// <summary>
    /// 配置值
    /// </summary>

    [Required(ErrorMessage = "ConfigValue不能为空")]
    public override string ConfigValue { get; set; }
}

/// <summary>
/// 编辑配置参数
/// </summary>
public class ConfigEditInput : ConfigAddInput
{
    /// <summary>
    /// ID
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}

/// <summary>
/// 删除配置参数
/// </summary>

public class ConfigDeleteInput : BaseIdInput
{
}

/// <summary>
/// 批量修改输入参数
/// </summary>
public class ConfigEditBatchInput
{
    /// <summary>
    /// 分类
    /// </summary>
    public string CateGory { get; set; }

    public List<DevConfig> DevConfigs { get; set; }
}