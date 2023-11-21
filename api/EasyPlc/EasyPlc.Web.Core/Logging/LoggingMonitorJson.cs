﻿namespace EasyPlc.Web.Core;

/// <summary>
/// 请求信息格式化
/// </summary>
public class LoggingMonitorJson
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 操作名称
    /// </summary>
    public string DisplayTitle { get; set; }

    /// <summary>
    /// 控制器名
    /// </summary>
    public string ControllerName { get; set; }

    /// <summary>
    /// 方法名称
    /// </summary>
    public string ActionName { get; set; }

    /// <summary>
    /// 类名称
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// 服务端
    /// </summary>
    public string LocalIPv4 { get; set; }

    /// <summary>
    /// 客户端IPV4地址
    /// </summary>
    public string RemoteIPv4 { get; set; }

    /// <summary>
    /// 请求方法
    /// </summary>
    public string HttpMethod { get; set; }

    /// <summary>
    /// 请求地址
    /// </summary>
    public string RequestUrl { get; set; }

    /// <summary>
    /// 浏览器标识
    /// </summary>
    public string UserAgent { get; set; }

    /// <summary>
    /// 系统名称
    /// </summary>
    public string OsDescription { get; set; }

    /// <summary>
    /// 系统架构
    /// </summary>
    public string OsArchitecture { get; set; }

    /// <summary>
    /// 环境
    /// </summary>
    public string Environment { get; set; }

    /// <summary>
    /// 认证信息
    /// </summary>
    public List<AuthorizationClaims> AuthorizationClaims { get; set; }

    /// <summary>
    /// 参数列表
    /// </summary>
    public List<Parameters> Parameters { get; set; }

    /// <summary>
    /// 返回信息
    /// </summary>
    public ReturnInformation ReturnInformation { get; set; }

    /// <summary>
    /// 异常信息
    /// </summary>
    public Exception Exception { get; set; }

    /// <summary>
    /// 验证错误信息
    /// </summary>
    public Validation Validation { get; set; }

    /// <summary>
    /// 日志时间
    /// </summary>
    public DateTime LogDateTime { get; set; }
}

/// <summary>
/// 认证信息
/// </summary>
public class AuthorizationClaims
{
    /// <summary>
    /// 类型
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    public string Value { get; set; }
}

/// <summary>
/// 请求参数
/// </summary>
public class Parameters
{
    /// <summary>
    /// 参数名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    public object Value { get; set; }
}

/// <summary>
/// 返回信息
/// </summary>
public class ReturnInformation
{
    /// <summary>
    /// 返回值
    /// </summary>
    public RetrunValue Value { get; set; }

    public class RetrunValue
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 额外信息
        /// </summary>
        public object extras { get; set; }

        /// <summary>
        /// 内如
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }
    }
}

/// <summary>
/// 异常信息
/// </summary>
public class Exception
{
    /// <summary>
    /// 异常类型
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// 异常内容
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 堆栈信息
    /// </summary>
    public string StackTrace { get; set; }
}

/// <summary>
/// 验证失败信息
/// </summary>
public class Validation
{
    ///// <summary>
    ///// 错误码
    ///// </summary>
    //public string ErrorCode { get; set; }

    ///// <summary>
    ///// 错误码
    ///// </summary>
    //public string OriginErrorCode { get; set; }

    /// <summary>
    /// 错误详情
    /// </summary>
    public string Message { get; set; }
}