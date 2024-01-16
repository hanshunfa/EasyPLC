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

namespace EasyPlc.Plugin.RabbitMQ;

public interface IRabbitMQManagemerntService : ISingleton
{
    /// <summary>
    /// 发送实时数据-读公共区数据
    /// </summary>
    /// <param name="body"></param>
    /// <returns></returns>
    Task<bool> PublishRealtimeData(RabbitMqInfoInput input);
    /// <summary>
    /// 发送实时报警-读公共区报警
    /// </summary>
    /// <param name="body"></param>
    /// <returns></returns>
    Task<bool> PublishRealtimeAlarm(RabbitMqInfoInput input);
    /// <summary>
    /// 发送事件数据-读事件区数据
    /// </summary>
    /// <param name="body"></param>
    /// <returns></returns>
    Task<bool> PublishRealtimeEvent(RabbitMqInfoInput input);
}