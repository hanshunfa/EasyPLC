// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

Console.WriteLine("Hello, World!");



IConnectionFactory factory = new ConnectionFactory//创建连接工厂对象
{
    HostName = "192.168.115.61",//IP地址
    Port = 5672,//端口号
    UserName = "shunfa",//用户账号
    Password = "123"//用户密码
};
//IConnectionFactory factory = new ConnectionFactory//创建连接工厂对象
//{
//    HostName = "192.168.115.45",//IP地址
//    Port = 5672,//端口号
//    UserName = "kstopa",//用户账号
//    Password = "kstopa"//用户密码
//};
IConnection con = factory.CreateConnection();//创建连接对象
IModel channel = con.CreateModel();//创建连接会话对象

string normalExchangeName = "normalEx", backupExchangeName = "backupEx";
var arguments = new Dictionary<string, object>
{
    { "alternate-exchange", backupExchangeName }
};

//声明normalEx交换机，且设置其备选交换机为backupEx
channel.ExchangeDeclare(normalExchangeName, ExchangeType.Direct, true, false, arguments);
//声明backupEx交换机
channel.ExchangeDeclare(backupExchangeName, ExchangeType.Fanout, true, false, null);
//声明备选交换机的队列
channel.QueueDeclare("backupQ", true, false, false, arguments);
//备选交换机与队列的绑定
channel.QueueBind(queue: "backupQ", exchange: backupExchangeName, routingKey: "efg");




//声明死信deadLetterEx交换机（就是一普通交换机）
channel.ExchangeDeclare("dlx_name", ExchangeType.Direct);

var queueArgs = new Dictionary<string, object>
{
    { "x-message-ttl", 10000 },//单位是毫秒
   //设置对应的死信交换机名为dlx_name
    { "x-dead-letter-exchange", "dlx_name" },
    //可选。指定死信一个新的routingKey，如果没设置则还沿用以前的。
    { "x-dead-letter-routing-key", "kef" }
};
channel.QueueDeclare(queue: "dealQ", durable: true, false, false, null);
channel.QueueBind(queue: "dealQ", exchange: "dlx_name", routingKey: "kef");

channel.QueueDeclare(queue:"noremalQ", durable:true, false, false, queueArgs);
//换机与队列的绑定
channel.QueueBind(queue: "noremalQ", exchange: normalExchangeName, routingKey: "abc");







//string name = "EasyPlc";
////声明一个队列
//channel.QueueDeclare(
//  queue: name,//消息队列名称
//  durable: false,//是否持久化,true持久化,队列会保存磁盘,服务器重启时可以保证不丢失相关信息。
//  exclusive: false,//是否排他,true排他的,如果一个队列声明为排他队列,该队列仅对首次声明它的连接可见,并在连接断开时自动删除.
//  autoDelete: false,//是否自动删除。true是自动删除。自动删除的前提是：致少有一个消费者连接到这个队列，之后所有与这个队列连接的消费者都断开时,才会自动删除.
//  arguments: null //设置队列的一些其它参数
//   );
string str = string.Empty;
do
{
    Console.WriteLine("发送内容:");
    str = Console.ReadLine();
    //消息内容
    byte[] body = Encoding.UTF8.GetBytes(str);
    //发送消息
    channel.BasicPublish(normalExchangeName, "abc", null, body);
    Console.WriteLine($"成功发送消息:[{DateTime.Now.ToString("yy-MM-dd HH:mm:ss:ffff")}]" + str);
} while (str.Trim().ToLower() != "exit");
con.Close();
channel.Close();
