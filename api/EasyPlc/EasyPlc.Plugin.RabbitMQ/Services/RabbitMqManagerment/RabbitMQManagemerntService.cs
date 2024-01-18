/*=============================================================================================
* 
*      *******    *******         **    **
*      **         **              **    **
*      **         **              **    **
*      *******    *******   **    ********
*           **    **              **    **
*           **    **              **    **
*      *******    **              **    **
*
* 创建者：韩顺发
* CLR版本：4.0.30319.42000
* 电子邮箱：shunfa.han@kstopa.com.cn
* 创建时间：2023/12/8 10:42:34
* 版本：v1.0.0
* 描述：
*
* ==============================================================================================
* 修改人：
* 修改时间：
* 修改说明：
* 版本：
* 
===============================================================================================*/

using EasyRabbitMQ;
using NewLife.Serialization;

namespace EasyPlc.Plugin.RabbitMQ;

/// <summary>
/// RabbitMQ管理器
/// </summary>
public class RabbitMQManagemerntService : IRabbitMQManagemerntService
{
    private readonly IRabbitMQManager _rabbitMQManager;
    private readonly IRabbitMqMonitoringService _rabbitMqMonitoringService;

    public RabbitMQManagemerntService(
        IRabbitMQManager rabbitMQManager,
        IRabbitMqMonitoringService rabbitMqMonitoringService
        )
    {
        _rabbitMQManager = rabbitMQManager;
        _rabbitMqMonitoringService = rabbitMqMonitoringService;
    }
    public async Task<bool> PublishRealtimeData(RabbitMqInfoInput input)
    {
        bool rlt = true;
        try
        {
            _rabbitMQManager.PublishRealTimeData(input.ToJson(true));
            //await _rabbitMqMonitoringService.Add(input.Adapt<RabbitMonitoringAddInput>());//新增记录到内存中
        }
        catch { }
        return rlt;
    }
    public async Task<bool> PublishRealtimeAlarm(RabbitMqInfoInput input)
    {
        bool rlt = true;
        try
        {
            _rabbitMQManager.PublishRealTimeAlarm(input.ToJson(true));
            //await _rabbitMqMonitoringService.Add(input.Adapt<RabbitMonitoringAddInput>());//新增记录到内存中
        }
        catch
        {
            // ignored
        }

        return rlt;
    }
    public async Task<bool> PublishRealtimeEvent(RabbitMqInfoInput input)
    {
        bool rlt = true;
        try
        {
            _rabbitMQManager.PublishEvent(input.ToJson());
            await _rabbitMqMonitoringService.Add(input.Adapt<RabbitMonitoringAddInput>());//新增记录到内存中
        }
        catch { }
        return rlt;
    }
}
