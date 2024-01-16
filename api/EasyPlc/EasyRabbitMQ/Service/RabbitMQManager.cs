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
* 创建时间：2023/12/7 9:11:02
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;


namespace EasyRabbitMQ
{

    public class RabbitMQManager : IRabbitMQManager
    {
        private readonly IConnectionFactory _factory;
        private readonly RabbitMQClientConfig _config;
        private IModel _channel;
        private readonly IRabbitReceived _rabbitReceived;
        public RabbitMQManager(
            IServiceProvider provider,
            IConfiguration configuration
            )
        {
            _rabbitReceived = provider.GetService<IRabbitReceived>();
            _config = new  RabbitMQClientConfig();
            configuration.GetSection("RabbitMQSetting").Bind(_config);
            //创建连接工厂对象
            _factory = new ConnectionFactory//创建连接工厂对象
            {
                HostName = _config.Host,//IP地址
                Port = _config.Port,//端口号
                UserName = _config.User,//用户账号
                Password = _config.Password//用户密码
            };
        }
        public string CreateModel()
        {
            try
            {
                IConnection con = _factory.CreateConnection();//创建连接对象
                _channel = con.CreateModel();//创建连接会话对象
                //声明交换机
                string normalEx = "normalEx", backupEx = "backupEx";
                var arguments = new Dictionary<string, object>
                {
                    { "alternate-exchange", backupEx }
                };
                //声明normalEx交换机，且设置其备选交换机为backupEx
                _channel.ExchangeDeclare(normalEx, ExchangeType.Direct, true, false, arguments);
                //声明backupEx交换机
                _channel.ExchangeDeclare(backupEx, ExchangeType.Fanout, true, false, null);
                //声明死信deadLetterEx交换机（就是一普通交换机）
                _channel.ExchangeDeclare("deadLetterEx", ExchangeType.Direct);


                //声明队列
                var queueArgs = new Dictionary<string, object>
                {
                    { "x-message-ttl", _config.Timeout },//单位是毫秒
                    { "x-dead-letter-exchange", "deadLetterEx" },//设置对应的死信交换机名为deadLetterEx
                    { "x-dead-letter-routing-key", "key_received_deadletter_send_realtimedata" }//可选。指定死信一个新的routingKey，如果没设置则还沿用以前的。
                };
                //共用交换机有3中队列 实时数据队列+实时报警队列+事件队列
                _channel.QueueDeclare(queue: "noremal_send_realtimedata_queue", durable: true, false, false, queueArgs);
                _channel.QueueBind(queue: "noremal_send_realtimedata_queue", exchange: normalEx, routingKey: "key_send_realtimedata");//发送实时数据队列
                queueArgs = new Dictionary<string, object>
                {
                    { "x-message-ttl", _config.Timeout },//单位是毫秒
                    { "x-dead-letter-exchange", "deadLetterEx" },//设置对应的死信交换机名为deadLetterEx
                    { "x-dead-letter-routing-key", "key_received_deadletter_send_realtimealarm" }//可选。指定死信一个新的routingKey，如果没设置则还沿用以前的。
                };
                _channel.QueueDeclare(queue: "noremal_send_realtimealarm_queue", durable: true, false, false, queueArgs);
                _channel.QueueBind(queue: "noremal_send_realtimealarm_queue", exchange: normalEx, routingKey: "key_send_realtimealarm");//发送实时报警队列
                queueArgs = new Dictionary<string, object>
                {
                    { "x-message-ttl", _config.Timeout },//单位是毫秒
                    { "x-dead-letter-exchange", "deadLetterEx" },//设置对应的死信交换机名为deadLetterEx
                    { "x-dead-letter-routing-key", "key_received_deadletter_send_event" }//可选。指定死信一个新的routingKey，如果没设置则还沿用以前的。
                };
                _channel.QueueDeclare(queue: "noremal_send_event_queue", durable: true, false, false, queueArgs);
                _channel.QueueBind(queue: "noremal_send_event_queue", exchange: normalEx, routingKey: "key_send_event");//发送事件队列

                _channel.QueueDeclare(queue: "noremal_received_realtimedata_queue", durable: true, false, false, null);
                _channel.QueueBind(queue: "noremal_received_realtimedata_queue", exchange: normalEx, routingKey: "key_received_realtimedata");//接收实时数据队列
                _channel.QueueDeclare(queue: "noremal_received_realtimealarm_queue", durable: true, false, false, null);
                _channel.QueueBind(queue: "noremal_received_realtimealarm_queue", exchange: normalEx, routingKey: "key_received_realtimealarm");//接收实时报警队列
                _channel.QueueDeclare(queue: "noremal_received_event_queue", durable: true, false, false, null);
                _channel.QueueBind(queue: "noremal_received_event_queue", exchange: normalEx, routingKey: "key_received_event");//接收事件队列

                //备份交换机队列
                _channel.QueueDeclare("backup_received_send_realtimedata_queue", true, false, false, null);
                _channel.QueueBind(queue: "backup_received_send_realtimedata_queue", exchange: backupEx, routingKey: "key_received_backup_send_realtimedata");//备选队列-接收实时数据队列
                _channel.QueueDeclare("backup_received_send_realtimealarm_queue", true, false, false, null);
                _channel.QueueBind(queue: "backup_received_send_realtimealarm_queue", exchange: backupEx, routingKey: "key_received_backup_send_realtimealarm");//备选队列-接收实时报警队列
                _channel.QueueDeclare("backup_received_send_event_queue", true, false, false, null);
                _channel.QueueBind(queue: "backup_received_send_event_queue", exchange: backupEx, routingKey: "key_received_backup_send_event");//备选队列-接收事件队列
                //死信交换机队列
                _channel.QueueDeclare("deadletter_received_send_realtimedata_queue", true, false, false, null);
                _channel.QueueBind(queue: "deadletter_received_send_realtimedata_queue", exchange: "deadLetterEx", routingKey: "key_received_deadletter_send_realtimedata");//死信队列-发送实时数据队列
                _channel.QueueDeclare("deadletter_received_send_realtimealarm_queue", true, false, false, null);
                _channel.QueueBind(queue: "deadletter_received_send_realtimealarm_queue", exchange: "deadLetterEx", routingKey: "key_received_deadletter_send_realtimealarm");//死信队列-发送实报警队列
                _channel.QueueDeclare("deadletter_received_send_event_queue", true, false, false, null);
                _channel.QueueBind(queue: "deadletter_received_send_event_queue", exchange: "deadLetterEx", routingKey: "key_received_deadletter_send_event");//死信队列-发送事件队列

                //接收消息---------------------------------------------------------------

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, args) =>
                {
                    byte[] body = args.Body.ToArray();
                    var msg = Encoding.UTF8.GetString(body);
                    //接收备份交换机与死信交换机内容
                    if (args.Exchange == "backupEx" && args.RoutingKey == "key_received_backup_send_realtimedata")//发送实时数据没有匹配到key
                    {
                        _rabbitReceived?.OnSendReadtimeData_AE(msg);
                    }
                    else if (args.Exchange == "backupEx" && args.RoutingKey == "key_received_backup_send_realtimealarm")//发送实时报警没有匹配到key
                    {
                        _rabbitReceived?.OnSendReadtimeAlarm_AE(msg);
                    }
                    else if (args.Exchange == "backupEx" && args.RoutingKey == "key_received_backup_send_event")//发送事件数据没有匹配到key
                    {
                        _rabbitReceived?.OnSendReadtimeEvent_AE(msg);
                    }
                    else if (args.Exchange == "deadLetterEx" && args.RoutingKey == "key_received_deadletter_send_realtimedata")
                    {
                        _rabbitReceived?.OnSendReadtimeData_DLX(msg);
                    }
                    else if (args.Exchange == "deadLetterEx" && args.RoutingKey == "key_received_deadletter_send_realtimealarm")
                    {
                        _rabbitReceived?.OnSendReadtimeAlarm_DLX(msg);
                    }
                    else if (args.Exchange == "deadLetterEx" && args.RoutingKey == "key_received_deadletter_send_event")
                    {
                        _rabbitReceived?.OnSendReadtimeEvent_DLX(msg);
                    }
                    //接收业务端回复
                    else if (args.Exchange == "normalEx" && args.RoutingKey == "key_received_realtimedata")
                    {
                        _rabbitReceived?.OnReceivedReadtimeData(msg);
                    }
                    else if (args.Exchange == "normalEx" && args.RoutingKey == "key_received_realtimealarm")
                    {
                        _rabbitReceived?.OnReceivedReadtimeAlarm(msg);
                    }
                    else if (args.Exchange == "normalEx" && args.RoutingKey == "key_received_event")
                    {
                        _rabbitReceived?.OnReceivedReadtimeEvent(msg);
                    }
                };
                //监听队列
                _channel.BasicConsume("backup_received_send_realtimedata_queue", true, consumer);
                _channel.BasicConsume("backup_received_send_realtimealarm_queue", true, consumer);
                _channel.BasicConsume("backup_received_send_event_queue", true, consumer);

                _channel.BasicConsume("deadletter_received_send_realtimedata_queue", true, consumer);
                _channel.BasicConsume("deadletter_received_send_realtimealarm_queue", true, consumer);
                _channel.BasicConsume("deadletter_received_send_event_queue", true, consumer);

                return "Succeed";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        public void PublishRealTimeData(string body)
        {
            
            try
            {
                //消息内容
                byte[] sends = Encoding.UTF8.GetBytes(body);
                //发送消息
                _channel.BasicPublish("normalEx", "key_send_realtimedata", null, sends);
            }
            catch { }
            
        }
        public void PublishRealTimeAlarm(string body)
        {
            try
            {
                //消息内容
                byte[] sends = Encoding.UTF8.GetBytes(body);
                //发送消息
                _channel.BasicPublish("normalEx", "key_send_realtimealarm", null, sends);
            }
            catch { }
            
        }
        public void PublishEvent(string body)
        {
            try
            {
                //消息内容
                byte[] sends = Encoding.UTF8.GetBytes(body);
                //发送消息
                _channel.BasicPublish("normalEx", "key_send_event", null, sends);
            }
            catch { }
            
        }
    }
}
