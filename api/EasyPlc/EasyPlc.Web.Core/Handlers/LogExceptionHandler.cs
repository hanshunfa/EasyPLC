﻿using DnsClient.Internal;
using Furion.DependencyInjection;
using Furion.FriendlyException;

namespace EasyPlc.Web.Core;

/// <summary>
/// 全局异常处理提供器
/// </summary>
public class LogExceptionHandler : IGlobalExceptionHandler, ISingleton
{
    private readonly ILogger<LogExceptionHandler> _logger;

    public LogExceptionHandler(ILogger<LogExceptionHandler> logger)
    {
        this._logger = logger;
    }

    public async Task OnExceptionAsync(ExceptionContext context)
    {
        var exception = context.Exception;//获取异常
        //如果异常类型不是友好异常
        if (exception.GetType() != typeof(AppFriendlyException))
        {
            _logger.LogError(exception, exception.Message);
            //重新定义异常
            context.Exception = new AppFriendlyException("系统异常，请联系管理员", ErrorCodeEnum.A0000);
        }
        await Task.CompletedTask;
    }
}