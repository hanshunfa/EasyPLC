using Furion.ConfigurableOptions;

namespace EasyPlc.Plugin.Core;

/// <summary>
/// 插件配置选项
/// </summary>
public class PluginSettingsOptions : IConfigurableOptions
{
    /// <summary>
    /// 是否开启SignalR
    /// </summary>
    public bool UseSignalR { get; set; } = true;

    /// <summary>
    /// 是否开启Mqtt
    /// </summary>
    public bool UseMqtt { get; set; } = false;

    /// <summary>
    /// 默认通知类型
    /// SignalR/Mqtt
    /// </summary>
    public NoticeComponent NoticeComponent { get; set; } = NoticeComponent.Signalr;

    public SiemensPlc SiemensPlc { get; set; }
}
public class SiemensPlc
{
    public bool IsUse { get; set; } = false;
    public bool IsInitFactory { get; set;} = false;
}