namespace EasyPlc.Plugin.Plc;

/// <summary>
/// 西门子PLC工厂
/// </summary>
public class SiemensPlcFactoryService : ISiemensPlcFactoryService
{
    private readonly IGenSiemensPlcInfoUtil _genSiemensPlcInfoUtil;

    public SiemensPlcFactoryService(
        IGenSiemensPlcInfoUtil genSiemensPlcInfoUtil
        )
    {
        _genSiemensPlcInfoUtil = genSiemensPlcInfoUtil;
    }
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
        await _genSiemensPlcInfoUtil.GenSiemensPLCInfoList();



        if (!IsUse) return "工厂不能使用，请联系管理员";
        if (ListConnectionSiemensPLC.Count > 0) return "工厂已存在";
        //根据动态生成的PLC信息，若返回NULL，表示PLC还没有动态生成
        var plcInfos =await _genSiemensPlcInfoUtil.GenSiemensPLCInfoList();
        if (plcInfos == null) return "请通过代码生成器生成代码，重新编译测试";
        //创建实例
        foreach ( var plcInfo in plcInfos )
        {
            var connectionSiemensPLC = new ConnectionSiemensPLC();
            connectionSiemensPLC.SetPlcInfo(plcInfo);
            ListConnectionSiemensPLC.Add(connectionSiemensPLC);
        }

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
