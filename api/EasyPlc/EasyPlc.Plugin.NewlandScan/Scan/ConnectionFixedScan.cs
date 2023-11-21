using TouchSocket.Core;
using TouchSocket.Sockets;

namespace EasyPlc.Plugin.Scan;

public class ConnectionFixedScan : IConnectionFixedScan
{
    public FixedScan FixedScan { get; set; }
    public void Init(FixedScan fixedScan)
    {
        FixedScan = fixedScan;
        InitTcpClient();
    }
    private string m_log = string.Empty;
    private List<string> m_logs = new List<string>();
    private int maxLogCount = 50;//最多记录50条最新信息
    public string GetScanLog()
    {
        return m_log;
    }
    public void SetScanlog(string strLog)
    {
        m_logs.Add(strLog + "\r\n");
        if (m_logs.Count > maxLogCount)
        {
            var count = m_logs.Count;
            m_logs.RemoveRange(0, count - maxLogCount);
        }
        m_log = string.Empty;
        foreach (var log in m_logs)
        {
            m_log += log;
        }
    }

    public void CloseTcpClient()
    {
        if (tcpClient.IsClient)
        {
            tcpClient.Close();
            tcpClient.Dispose();
        }
    }

    private TcpClient tcpClient = null;
    private void InitTcpClient()
    {
        tcpClient = new TcpClient();
        tcpClient.Connected = OnConnected;//成功连接到服务器
        tcpClient.Disconnected = OnDisconnected;//从服务器断开连接，当连接不成功时不会触发。
        tcpClient.Received = OnReceived;//接收到数据

        //声明配置
        TouchSocketConfig config = new TouchSocketConfig();
        config.SetRemoteIPHost(new IPHost($"{FixedScan.Ip}:{FixedScan.Port}"))
            .UsePlugin()
            .ConfigurePlugins(a =>
            {
                a.UseReconnection(5, false, 1000);

            })
            .SetBufferLength(1024 * 10);


        //载入配置
        tcpClient.Setup(config);

        tcpClient.ConnectAsync(500);
    }
    /// <summary>
    /// 连接服务器成功
    /// </summary>
    /// <param name="client"></param>
    /// <param name="e"></param>
    private void OnConnected(ITcpClient client, MsgEventArgs e)
    {
        FixedScan.IsConn = true;
    }
    /// <summary>
    /// 从服务器断开连接，当连接不成功时不会触发
    /// </summary>
    /// <param name="client"></param>
    /// <param name="e"></param>
    private void OnDisconnected(ITcpClientBase client, DisconnectEventArgs e)
    {
        FixedScan.IsConn = false;
    }
    /// <summary>
    /// 接收到数据
    /// </summary>
    /// <param name="client"></param>
    /// <param name="byteBlock"></param>
    /// <param name="requestInfo"></param>
    private void OnReceived(ITcpClient client, ByteBlock byteBlock, IRequestInfo requestInfo)
    {
        reciveBytearr = byteBlock.ToArray();

        resetEvent.Set();//置位
    }

    private ManualResetEvent resetEvent = new ManualResetEvent(false);
    private byte[] reciveBytearr;
    public bool ReadScan(ref string resStr)
    {
        bool ret = false;
        if (FixedScan == null) { resStr = "请初始化后进行读操作"; return ret; }

        try
        {
            var cmd = ReadCommdCreate();
            tcpClient.Connect(500);
            resetEvent.Reset();
            reciveBytearr = null;
            tcpClient.Send(cmd);
            if (resetEvent.WaitOne(1500))//等待1500ms
            {
                resStr = Encoding.ASCII.GetString(reciveBytearr).Trim().Replace("\r", "").Replace("\n", "").Replace("$", "");
                if(resStr == "NG")//读取失败
                {
                    ret = false;
                }
                else
                {
                    ret = true;//读取成功
                }
            }
            else
            {
                //超时
                resStr = "接收超时";
                ret = false;
            }
        }
        catch (Exception ex)
        {
            resStr = ex.Message;
            ret = false;
        }
        return ret;
    }

  
    /// <summary>
    /// 读命令生成
    /// </summary>
    /// <returns></returns>
    private byte[] ReadCommdCreate()
    {
        return new byte[] { 0x01, 0x54, 0x04 };//新大陆触发扫码指令
    }
}
