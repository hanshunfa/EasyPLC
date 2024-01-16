using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace EasyRabbitMQ
{
    /// <summary>
    /// RabbitMQ客户端服务
    /// </summary>
    public interface IRabbitMQManager
    {
        /// <summary>
        /// 创建RabbitMQ会话
        /// </summary>
        /// <param name="config">配置信息</param>
        string CreateModel();

        /// <summary>
        /// 发送实时数据
        /// </summary>
        /// <param name="body"></param>
        void PublishRealTimeData(string body);
        /// <summary>
        /// 发送实时报警
        /// </summary>
        /// <param name="body"></param>
        void PublishRealTimeAlarm(string body);
        /// <summary>
        /// 发送事件数据
        /// </summary>
        /// <param name="body"></param>
        void PublishEvent(string body);
    }
}
