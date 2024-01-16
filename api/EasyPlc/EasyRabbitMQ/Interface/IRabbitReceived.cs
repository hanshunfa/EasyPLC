using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRabbitMQ
{
    /// <summary>
    /// 接收业务发送内容及备份+死信交换机消息
    /// </summary>
    public interface IRabbitReceived
    {
        /// <summary>
        /// 接收业务端实时数据
        /// </summary>
        /// <param name="body"></param>
        abstract void OnReceivedReadtimeData(string body);
        /// <summary>
        /// 接收业务端实时报警
        /// </summary>
        /// <param name="body"></param>
        abstract void OnReceivedReadtimeAlarm(string body);
        /// <summary>
        /// 接收业务端实时事件
        /// </summary>
        /// <param name="body"></param>
        abstract void OnReceivedReadtimeEvent(string body);

        /// <summary>
        /// 发送实时数据死信队列返回
        /// </summary>
        /// <param name="body"></param>
        abstract void OnSendReadtimeData_DLX(string body);
        /// <summary>
        /// 发送实时报警死信队列返回
        /// </summary>
        /// <param name="body"></param>
        abstract void OnSendReadtimeAlarm_DLX(string body);
        /// <summary>
        /// 发送事件数据死信队列返回
        /// </summary>
        /// <param name="body"></param>
        abstract void OnSendReadtimeEvent_DLX(string body);
        /// <summary>
        /// 发送实时数据备份队列返回
        /// </summary>
        /// <param name="body"></param>
        abstract void OnSendReadtimeData_AE(string body);
        /// <summary>
        /// 发送实时报警备份队列返回
        /// </summary>
        /// <param name="body"></param>
        abstract void OnSendReadtimeAlarm_AE(string body);
        /// <summary>
        /// 发送事件数据备份队列返回
        /// </summary>
        /// <param name="body"></param>
        abstract void OnSendReadtimeEvent_AE(string body);
    }
}
