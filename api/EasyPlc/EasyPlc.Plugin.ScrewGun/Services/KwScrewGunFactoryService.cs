
using TouchSocket.Core;
using TouchSocket.Sockets;

namespace EasyPlc.Plugin.ScrewGun;

public class KwScrewGunFactoryService : IKwScrewGunFactoryService
{
    private readonly ILogger<KwScrewGunService> _logger;
    private readonly IKwScrewGunService _screwGunService;

    public KwScrewGunFactoryService(
        ILogger<KwScrewGunService> logger,
        IKwScrewGunService screwGunService
        )
    {
        _logger = logger;
        _screwGunService = screwGunService;

    }
    
    //螺丝枪列表
    private List<KwScrewGunOutput> screwGunInfoList = new List<KwScrewGunOutput>();

    private async Task<List<KwScrewGunOutput>> GetScrewGunsAsync()
    {
        var list = new List<KwScrewGunOutput>();
        var kw = await _screwGunService.GetListAsync();
        kw.ForEach(it => {
            list.Add(it.Adapt<KwScrewGunOutput>());
        });
        return list;
    }

    public async Task SetKwScrewGunInfoList()
    {
        screwGunInfoList = await GetScrewGunsAsync();
    }

    private TcpService service { get; set; }
    public async Task<bool> StartTcpServer(string ip = "127.0.0.1", ushort port = 12345)
    {
        if (service == null)
        {
            if(screwGunInfoList.Count == 0)
                screwGunInfoList = await GetScrewGunsAsync();//获取螺丝枪信息

            service = new TcpService();
            service.Connected = OnConnected;
            service.Disconnected = OnDisconnected;
            service.Received = OnReceived;
            service.Setup(
                new TouchSocketConfig()//载入配置     
                    .SetListenIPHosts(new IPHost[] {
                        new IPHost($"{ip}:{port}")
                    })//同时监听两个地址
                    )
                .Start();//启动
            return true;
        }
        else
        {
            //服务已启动
            return true;
        }
    }
    public void StopTcpServer()
    {
        service.Clear();
    }

    public List<KwScrewGunOutput> GetKwScrewGuns() { 
        return screwGunInfoList;
    }
    public RecvOutput GetNewRecvInfo(string ip)
    {
        var kw = screwGunInfoList.Find(it => it.Ip == ip);
        if (kw != null)
        {
            var ri = kw.RecvOutputList.LastOrDefault();
            if(ri != null)
            {
                if(!ri.IsUse)
                {
                    return ri;
                }
            }
        }
        return null;
    }
    public void SetRecvInfoUse(string ip, string code)
    {
        var kw = screwGunInfoList.Find(it => it.Ip == ip);
        if (kw != null)
        {
            var ri = kw.RecvOutputList.Where(it => it.Code == code).FirstOrDefault();
            if(ri != null)
            {
                ri.IsUse = true;//已被使用
            }
        }
    }
    public RecvOutput GetNewRecvInfoByName(string name)
    {
        var kw = screwGunInfoList.Find(it => it.Name == name);
        if (kw != null)
        {
            var ri = kw.RecvOutputList.LastOrDefault();
            if (ri != null)
            {
                if (!ri.IsUse)
                {
                    return ri;
                }
            }
        }
        return null;
    }
    public RecvOutput GetNewRecvInfoAndUsed(string name, int maxGet = 10, int interval = 100)
    {
        ManualResetEvent mre = new ManualResetEvent(false);
        int count = maxGet;
        RecvOutput recvInfo = null;
        do
        {
            recvInfo = GetNewRecvInfoByName(name);
            if(recvInfo != null)
            {
                recvInfo.IsUse = true;//标记使用过
                //添加日志，告知界面已被使用
                AddLog(name, recvInfo);//添加日志
                mre.Set();//循环会结束
            }
            else
            {
                //没有拿到数据
                if (count <= 0) {
                    mre.Set();//循环会结束
                }
            }
            count--;
        }
        while (!mre.WaitOne(interval));

        return recvInfo;
    }

    /// <summary>
    /// 客户端连接
    /// </summary>
    /// <param name="client"></param>
    /// <param name="e"></param>
    private void OnConnected(ISocketClient client, TouchSocketEventArgs e)
    {
        string ip = client.IP;
        ushort port = (ushort)client.Port;
        
        var kw = screwGunInfoList.Find(it => it.Ip == ip);
        if (kw != null)
        {
            kw.IsConn = true;
            kw.ClientId = client.ID;
            kw.AcceptTime = DateTime.Now;
        }
    }
    /// <summary>
    /// 客户端断开连接
    /// </summary>
    /// <param name="client"></param>
    /// <param name="e"></param>
    private void OnDisconnected(ISocketClient client, DisconnectEventArgs e)
    {
        string ip = client.IP;
        ushort port = (ushort)client.Port;
        var kw = screwGunInfoList.Find(it => it.Ip == ip);
        if (kw != null)
        {
            kw.IsConn = false;
            kw.CloseTime = DateTime.Now;
        }
    }
    /// <summary>
    /// 接收到数据
    /// </summary>
    /// <param name="client"></param>
    /// <param name="byteBlock"></param>
    /// <param name="requestInfo"></param>
    private void OnReceived(ISocketClient client, ByteBlock byteBlock, IRequestInfo requestInfo)
    {
        string ip = client.IP;
        ushort port = (ushort)client.Port;

        //解析返回数据
        var resultKw = KwUtil.PaseKw(byteBlock.ToArray());
        if (!resultKw.PaseResult)
        {
            _logger.LogError($"解析螺丝枪数据异常：{resultKw.PaseMsg}");
        }

        var kw = screwGunInfoList.Find(it => it.Ip == ip);
        if (kw != null)
        {
            var ri = resultKw.Adapt<RecvOutput>();
            ri.RevTime = DateTime.Now;
            ri.IsUse = false;
            ri.Code = RandomHelper.CreateRandomString(20);

            //最多存放kw.MaxCount
            if(kw.RecvOutputList.Count > kw.MaxCount)
            {
                kw.RecvOutputList.RemoveRange(0, kw.RecvOutputList.Count - kw.MaxCount);
            }
            kw.RecvOutputList.Add(ri);
            AddLog(kw, ri);//添加日志
        }
    }
    
    private void AddLog(KwScrewGunOutput kw, RecvOutput info)
    {
        string msg = string.Empty;
        if (!info.PaseResult)
        {
            msg += $"{info.RevTime:G} 解析异常:{info.PaseMsg}";
        }
        else
        {
            var r = info.Result ? "OK" : "NOK";
            msg += $"{info.RevTime:G} {r} SetTorque:{info.SetTorque} SetAngle:{info.SetAngle} RunTime:{info.RunTimeS} Torque:{info.Torque} Angle:{info.Angle} IsUse:{info.IsUse}";
        }
        kw.Logs.Add(msg);
        if (kw.Logs.Count > kw.MaxCount)
        {
            kw.Logs.RemoveRange(0, kw.Logs.Count - kw.MaxCount);
        }
        kw.Log = string.Empty;
        foreach (var log in kw.Logs)
        {
            kw.Log += (log + "\r\n");
        }
    }
    private void AddLog(string name, RecvOutput info)
    {
        string msg = string.Empty;
        if (!info.PaseResult)
        {
            msg += $"{info.RevTime:G} 解析异常:{info.PaseMsg}";
        }
        else
        {
            var r = info.Result ? "OK" : "NOK";
            msg += $"{info.RevTime:G} {r} SetTorque:{info.SetTorque} SetAngle:{info.SetAngle} RunTime:{info.RunTimeS} Torque:{info.Torque} Angle:{info.Angle} IsUse:{info.IsUse}";
        }
        var kw = screwGunInfoList.Find(it => it.Name == name);
        if (kw != null)
        {
            kw.Logs.Add(msg);
            if (kw.Logs.Count > kw.MaxCount)
            {
                kw.Logs.RemoveRange(0, kw.Logs.Count - kw.MaxCount);
            }
            kw.Log = string.Empty;
            foreach (var log in kw.Logs)
            {
                kw.Log += (log + "\r\n");
            }
        }
    }
}
