using NewLife.Reflection;
using EasyPlc.System;
using EasyPlc.Application;

namespace EasyPlc.Plugin.SygoleRFID;

public class SygoleFactoryService : ISygoleFactoryService
{
    private readonly ILogger<SygoleFactoryService> _logger;
    private readonly ISygoleRfidService _sygoleRfidService;

    public SygoleFactoryService(
        ILogger<SygoleFactoryService> logger,
        ISygoleRfidService sygoleRfidService
        )
    {
        _logger = logger;
        _sygoleRfidService = sygoleRfidService;
    }
    private List<ConnectionSygoleRFID> m_connectionSygoleRFIDList = new List<ConnectionSygoleRFID>();
    public async Task InitFactory(bool qz = false)
    {
        if (!qz)
        {
            if (m_connectionSygoleRFIDList.Count > 0) { return; }
        }
        else
        {
            //释放资源
            m_connectionSygoleRFIDList.ForEach(it => {
                it.CloseTcpClient();
            });
            m_connectionSygoleRFIDList.Clear();
        }
        var rfids = await _sygoleRfidService.GetListAsync();
        foreach ( var rfid in rfids )
        {
            var connRfid = new ConnectionSygoleRFID();
            connRfid.Init(rfid);
            m_connectionSygoleRFIDList.Add(connRfid);
        }
    }

    public List<ConnectionSygoleRFID> GetConnections()
    {
        return m_connectionSygoleRFIDList;
    }

    public string GetRFIDLog(IConnectionSygoleRFID sygoleRFID)
    {
        return sygoleRFID.GetRFIDLog();
    }
    public string GetRFIDLog(string name)
    {
        var sygoleRFID = m_connectionSygoleRFIDList.Where(it => it.RfidSygole.Name == name).FirstOrDefault();
        if (sygoleRFID == null) { return $"通过名称[{name}]查找对象失败"; }
        return sygoleRFID.GetRFIDLog();
    }

    public bool ReadRFID(IConnectionSygoleRFID sygoleRFID, int readLen, ref string resStr)
    {
        var r = sygoleRFID.ReadRFID(readLen, ref resStr);
        var rstr = r ? "成功": "失败";
        sygoleRFID.SetRFIDlog($"{DateTime.Now:G} 读取RFID {rstr} {resStr}");
        return r;
    }
    public bool ReadRFID(string name, int readLen, ref string resStr)
    {
        var sygoleRFID = m_connectionSygoleRFIDList.Where(it=>it.RfidSygole.Name == name).FirstOrDefault();
        if(sygoleRFID == null) { return false; }
        var r = sygoleRFID.ReadRFID(readLen, ref resStr);
        var rstr = r ? "成功" : "失败";
        sygoleRFID.SetRFIDlog($"{DateTime.Now:G} 读取RFID {rstr} {resStr}");
        return r;
    }

    public bool WriteRFID(IConnectionSygoleRFID sygoleRFID, string writeStr, ref string msg)
    {
        var w = sygoleRFID.WriteRFID(writeStr, ref msg);
        var wstr = w ? "成功" : "失败";
        sygoleRFID.SetRFIDlog($"{DateTime.Now:G} 写入RFID内容{writeStr} {wstr}");
        return w;
    }
    public bool WriteRFID(string name, string writeStr, ref string msg)
    {
        var sygoleRFID = m_connectionSygoleRFIDList.Where(it => it.RfidSygole.Name == name).FirstOrDefault();
        if (sygoleRFID == null) { return false; }
        var w = sygoleRFID.WriteRFID(writeStr, ref msg);
        var wstr = w ? "成功" : "失败";
        sygoleRFID.SetRFIDlog($"{DateTime.Now:G} 写入RFID内容{writeStr} {wstr}");
        return w;
    }

    
}
