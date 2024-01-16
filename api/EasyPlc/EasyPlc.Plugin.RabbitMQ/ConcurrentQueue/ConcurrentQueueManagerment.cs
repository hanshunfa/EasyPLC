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
* 创建时间：2023/12/12 11:47:38
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

using EasyPlc.Application;
using System.Collections.Concurrent;

namespace EasyPlc.Plugin.RabbitMQ;
/// <summary>
/// 安全队列管理
/// </summary>
public static class ConcurrentQueueManagerment
{
    private static ConcurrentQueue<RabbitMqEventDataLog> PI_Queue;//公共
    private static ConcurrentQueue<RabbitMqEventDataLog> Event_Queue;//事件
    static ConcurrentQueueManagerment()
    {
        PI_Queue = new ConcurrentQueue<RabbitMqEventDataLog>();
        Event_Queue = new ConcurrentQueue<RabbitMqEventDataLog>();
    }
    public static void PI_Enqueue(RabbitMqEventDataLog log)
    {
        PI_Queue.Enqueue(log);
    }
    public static void Event_Enqueue(RabbitMqEventDataLog log)
    {
        Event_Queue.Enqueue(log);
    }
    public static RabbitMqEventDataLog PI_TryDequeue()
    {
        RabbitMqEventDataLog log = null;
        if (!PI_Queue.IsEmpty)
        {
            if (PI_Queue.TryDequeue(out log))
            {
                return log;
            }
        }
        return null;
    }
    public static RabbitMqEventDataLog Event_TryDequeue()
    {
        RabbitMqEventDataLog log = null;
        if (!Event_Queue.IsEmpty)
        {
            if (Event_Queue.TryDequeue(out log))
            {
                return log;
            }
        }
        return null;
    }
}
