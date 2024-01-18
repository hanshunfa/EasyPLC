using EasyPlc.Plugin.Plc.Utils;
using EasyPlc.Plugin.RabbitMQ;
using EasyRabbitMQ;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace EasyPlc.Plugin.Plc;

/// <summary>
/// 西门子PLC工厂
/// </summary>
public class SiemensPlcFactoryService : ISiemensPlcFactoryService
{
    private readonly IGenSiemensPlcInfoUtil _genSiemensPlcInfoUtil;
    private readonly IRabbitMQManagemerntService _rabbitMQManagementService;

    public SiemensPlcFactoryService(
        IGenSiemensPlcInfoUtil genSiemensPlcInfoUtil,
        IRabbitMQManagemerntService rabbitMQManagementService
        )
    {
        _genSiemensPlcInfoUtil = genSiemensPlcInfoUtil;
        _rabbitMQManagementService = rabbitMQManagementService;
    }

    #region 回调监听，全部都是同步的，里面不能有阻塞操作
    private async Task OnErr(string errMsg)
    {
        
    }
    private async Task OnInfo(string Info)
    {

    }
    /// <summary>
    /// 公共区读取内容
    /// </summary>
    /// <param name="plc"></param>
    private async Task OnPublic(SiemensPlcInfo plc)
    {
        var readBody = plc.CreateRealtimeDataJsonString();
        var writeBody = plc.CreateRealtimeDataJsonString(CreateJsonStringUtil.EnumBody.WriteBody);
        await _rabbitMQManagementService.PublishRealtimeData(new RabbitMqInfoInput
        {
            Id = CommonUtils.GetSingleId(),
            Name = plc.Name,
            Ip = plc.IP,
            Version = plc.Version,
            ReadTime = plc.PI.ReadTime,
            SendTime = plc.PI.SendTime,
            ReadBody = readBody,
            WriteBody = writeBody,
            Status = EventStatus.Ready
        });
    }
    /// <summary>
    /// 事件区读取内容
    /// </summary>
    /// <param name="plc"></param>
    /// <param name="eventIdx"></param>
    private async Task OnEvent(SiemensPlcInfo plc, int eventIdx)
    {
        //提取PLC事件信息
        var readBody = plc.CreateEventDataJsonString(eventIdx, CreateJsonStringUtil.EnumBody.ReadBody);
        var writeBody = plc.CreateEventDataJsonString(eventIdx, CreateJsonStringUtil.EnumBody.WriteBody);
        await _rabbitMQManagementService.PublishRealtimeEvent(
            new RabbitMqInfoInput
            {
                Id = CommonUtils.GetSingleId(),
                Name = plc.Name,
                Ip = plc.IP,
                Version = plc.Version,
                ReadTime = plc.EIs[eventIdx].ReadTime,
                SendTime = plc.EIs[eventIdx].SendTime,
                ReadBody = readBody,
                WriteBody = writeBody,
                Status = EventStatus.Ready
            });
    }

    #endregion

    private bool IsUse { get; set; } = false;
    public void Use()
    {
        IsUse = true;
    }
    private List<ConnectionSiemensPLC> ListConnectionSiemensPLC = new List<ConnectionSiemensPLC>();
    /// <summary>
    /// 初始化工厂，创建PLC通讯实列
    /// </summary>
    public async Task<string> InitFactory()
    {
        if (!IsUse) return "工厂不能使用，请联系管理员";
        if (ListConnectionSiemensPLC.Count > 0) return "工厂已存在";
        var plcInfos = await _genSiemensPlcInfoUtil.GenSiemensPLCInfoList();
        if (plcInfos == null || plcInfos.Count == 0) return "没有配置的PLC";
        //创建实例
        foreach ( var plcInfo in plcInfos )
        {
            var connectionSiemensPlc = new ConnectionSiemensPLC();
            connectionSiemensPlc.SetPlcInfo(plcInfo);
            ListConnectionSiemensPLC.Add(connectionSiemensPlc);

            //监听回调
            connectionSiemensPlc.OnErr += OnErr;
            connectionSiemensPlc.OnInfo += OnInfo;
            connectionSiemensPlc.OnPublicCallback += OnPublic;
            connectionSiemensPlc.OnEventCallback += OnEvent;
        }

        return "成功";
    }
    /// <summary>
    /// 关闭工厂
    /// </summary>
    /// <returns></returns>
    public string CloseFactory()
    {
        if (!IsUse) return "工厂不能使用，请联系管理员";
        if (ListConnectionSiemensPLC.Count == 0) return "工厂不存在";
        //取消监听
        ListConnectionSiemensPLC.ForEach(connPlc => {
            connPlc.OnErr -= OnErr;
            connPlc.OnInfo -= OnInfo;
            connPlc.OnPublicCallback -= OnPublic;
            connPlc.OnEventCallback -= OnEvent;
        });
        //关闭所有PLC
        StopPLC();
        ListConnectionSiemensPLC.Clear();
        return "成功";
    }
    public void StartPLC()
    {
        if (!IsUse) return;
        foreach (var connection in ListConnectionSiemensPLC)
        {
            connection.StartWork();
        }
    }
    public string StartPLC(ConnectionSiemensPLC connectionSiemensPLC)
    {
        if (!IsUse) return "工厂不能使用，请联系管理员";
        return connectionSiemensPLC.StartWork();
    }
    public string StartPLC(string ip)
    {
        if (!IsUse) return "工厂不能使用，请联系管理员";
        var plc = ListConnectionSiemensPLC.Where(it=>it.PlcInfo.IP == ip).FirstOrDefault();
        if (plc == null) return $"IP[{ip}]不在在线PLC列表中";
        return plc.StartWork();
    }
    public string StopPLC(ConnectionSiemensPLC connectionSiemensPLC)
    {
        if (!IsUse) return "工厂不能使用，请联系管理员";
        return connectionSiemensPLC.StopWork();
    }
    public string StopPLC(string ip)
    {
        if (!IsUse) return "工厂不能使用，请联系管理员";
        var plc = ListConnectionSiemensPLC.Where(it => it.PlcInfo.IP == ip).FirstOrDefault();
        if (plc == null) return $"IP[{ip}]不在在线PLC列表中";
        return plc.StopWork();
    }
    public void StopPLC()
    {
        if (!IsUse) return ;
        foreach (var connection in ListConnectionSiemensPLC)
        {
            connection.StopWork();
        }
    }
    public List<ConnectionSiemensPLC> GetConnectionSiemensPLCList()
    {
        return ListConnectionSiemensPLC;
    }

}
