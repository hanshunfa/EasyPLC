using System.Net;
using EasyPlc.System;
using Magicodes.ExporterAndImporter.Core.Extension;
using System.Data;
using TouchSocket.Sockets;
using TouchSocket.Core;

namespace EasyPlc.Plugin.SygoleRFID;

public class ConnectionSygoleRFID : IConnectionSygoleRFID
{
    public SygoleRfid RfidSygole { get; set; }
    public void Init(SygoleRfid rfidSygole)
    {
        RfidSygole = rfidSygole;
        InitTcpClient();
    }
    private string m_log = string.Empty;
    private List<string> m_logs = new List<string>();
    private int maxLogCount = 50;//最多记录50条最新信息
    public string GetRFIDLog()
    {
        return m_log;
    }
    public void SetRFIDlog(string strLog)
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
        config.SetRemoteIPHost(new IPHost($"{RfidSygole.Ip}:{RfidSygole.Port}" ))
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
        RfidSygole.IsConn = true;
    }
    /// <summary>
    /// 从服务器断开连接，当连接不成功时不会触发
    /// </summary>
    /// <param name="client"></param>
    /// <param name="e"></param>
    private void OnDisconnected(ITcpClientBase client, DisconnectEventArgs e)
    {
        RfidSygole.IsConn = false;

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
    public bool ReadRFID(int readLen, ref string resStr)
    {
        bool ret = false;
        resStr = "";
        if (RfidSygole == null) { resStr = "请初始化后进行读操作"; return ret; }

        try
        {
            var cmd = ReadCommdCreate(readLen);
            tcpClient.Connect(500);
            resetEvent.Reset();
            reciveBytearr = null;
            tcpClient.Send(cmd);
            if (resetEvent.WaitOne(500))//等待500ms
            {
                if (reciveBytearr.Length > 2 && reciveBytearr[0] == 0xFF && reciveBytearr[5] == 0x00)
                {
                    int dataLen = reciveBytearr[1];
                    byte[] revData = new byte[dataLen + 3];
                    Buffer.BlockCopy(reciveBytearr, 0, revData, 0, revData.Length);
                    resStr = global::System.Text.Encoding.ASCII.GetString(revData, 7, revData.Length - 9);
                    resStr = resStr.Split('\0')[0];
                }
                ret = true;
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

    public bool WriteRFID(string writeStr, ref string msg)
    {
        bool ret = false;
        msg = "";
        if (RfidSygole == null) { msg = "请初始化后进行读操作"; return false; }
       
        try
        {
            var cmd = WriteCommdCreate(writeStr);
            tcpClient.Connect(500);
            resetEvent.Reset();
            reciveBytearr = null;
            tcpClient.Send(cmd);
            if (resetEvent.WaitOne(500))//等待500ms
            {
                if (reciveBytearr.Length > 2 && reciveBytearr[0] == 0xFF && reciveBytearr[5] == 0x00)
                {
                    int dataLen = reciveBytearr[1];
                    byte[] revData = new byte[dataLen + 3];
                    Buffer.BlockCopy(reciveBytearr, 0, revData, 0, revData.Length);
                    msg = global::System.Text.Encoding.ASCII.GetString(revData, 7, revData.Length - 9);
                    msg = msg.Split('\0')[0];
                }
                ret = true;
            }
            else
            {
                //超时
                msg = "接收超时";
                ret = false;
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            ret = false;
        }
        return ret;
    }
    /// <summary>
    /// 读命令生成
    /// </summary>
    /// <returns></returns>
    private byte[] ReadCommdCreate(int readLen)
    {
        byte bt = (byte)(readLen & 0xFF);
        var bs = new byte[] { 0xFF, 0x08, 0x11, 0x00, 0x01, 0x00, 0x00, 0x00, bt };
        ushort res = GetCRC16(bs, bs.Length);

        byte ah = (byte)((res >> 8) & 0xff); ;//高8位
        byte al = (byte)(res & 0xff);//低8位
        byte[] data = new byte[bs.Length + 2];
        bs.CopyTo(data, 0);
        data[data.Length - 2] = ah;
        data[data.Length - 1] = al;

        return data;
    }
    /// <summary>
    /// 写命令生成
    /// </summary>
    /// <returns></returns>
    private byte[] WriteCommdCreate(string writeStr)
    {
        byte[] writeByte = global::System.Text.Encoding.ASCII.GetBytes(writeStr);
        int byteLen = writeByte.Length;

        byte bt = (byte)(byteLen & 0xFF);

        byte cmdLen = (byte)((byteLen + 8) & 0xFF); // 命令长度

        byte[] cmd_h = new byte[] { 0xFF, cmdLen, 0x12, 0x00, 0x01, 0x00, 0x00, 0x00, bt };

        byte[] bs = new byte[cmd_h.Length + writeByte.Length];
        cmd_h.CopyTo(bs, 0);
        writeByte.CopyTo(bs, cmd_h.Length);

        ushort res = GetCRC16(bs, bs.Length);

        byte ah = (byte)((res >> 8) & 0xff); ;//高8位
        byte al = (byte)(res & 0xff);//低8位
        byte[] data = new byte[bs.Length + 2];
        bs.CopyTo(data, 0);
        data[data.Length - 2] = ah;
        data[data.Length - 1] = al;

        return data;
    }
    private static ushort GetCRC16(byte[] data, int len)
    {
        ushort num = 0xffff;
        byte num3 = 0;
        for (int index = 0; index < len; index++)
        {
            num3 = data[index];
            num = (ushort)(wCRCTalbeAbs[(num3 ^ num) & 15] ^ (num >> 4));
            num = (ushort)(wCRCTalbeAbs[((num3 >> 4) ^ num) & 15] ^ (num >> 4));
        }
        return num;
    }
    private static ushort[] wCRCTalbeAbs = new ushort[] { 0, 0xcc01, 0xd801, 0x1400, 0xf001, 0x3c00, 0x2800, 0xe401, 0xa001, 0x6c00, 0x7800, 0xb401, 0x5000, 0x9c01, 0x8801, 0x4400 };
}
