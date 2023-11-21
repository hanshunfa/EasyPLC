﻿namespace EasyPlc.Plugin.Mqtt;

/// <summary>
/// mqtt常量
/// </summary>
public class MqttConst
{
    /// <summary>
    /// mqtt主题前缀
    /// </summary>
    public const string Mqtt_TopicPrefix = "EasyPlc/";

    /// <summary>
    /// 登出
    /// </summary>
    public const string Mqtt_Message_LoginOut = "LoginOut";

    /// <summary>
    /// 新消息
    /// </summary>
    public const string Mqtt_Message_New = "NewMessage";
}