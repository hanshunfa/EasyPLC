
namespace EasyPlc.Plugin.Scan;

public class FixedScanFactoryService : IFixedScanFactoryService
{
    private readonly ILogger<FixedScanFactoryService> _logger;
    private readonly IFixedScanService _fixedScanService;

    public FixedScanFactoryService(
        ILogger<FixedScanFactoryService> logger,
        IFixedScanService fixedScanService
        )
    {
        _logger = logger;
        _fixedScanService = fixedScanService;
    }
    private List<ConnectionFixedScan> m_connectionFixedScanList = new List<ConnectionFixedScan>();
    public async Task InitFactory(bool qz = false)
    {
        if (!qz)
        {
            if (m_connectionFixedScanList.Count > 0) { return; }
        }
        else
        {
            //释放资源
            m_connectionFixedScanList.ForEach(it => {
                it.CloseTcpClient();
            });
            m_connectionFixedScanList.Clear();
        }
        var rfids = await _fixedScanService.GetListAsync();
        foreach (var rfid in rfids)
        {
            var connScan = new ConnectionFixedScan();
            connScan.Init(rfid);
            m_connectionFixedScanList.Add(connScan);
        }
    }
    public List<ConnectionFixedScan> GetConnections()
    {
        return m_connectionFixedScanList;
    }

    public bool ReadScan(IConnectionFixedScan fixedScan, ref string resStr)
    {
        var r = fixedScan.ReadScan(ref resStr);
        var rstr = r ? "成功" : "失败";
        fixedScan.SetScanlog($"{DateTime.Now:G} 读取编码 {rstr} {resStr}");
        return r;
    }

    public bool ReadScan(string name, ref string resStr)
    {
        var fixedScan = m_connectionFixedScanList.Where(it => it.FixedScan.Name == name).FirstOrDefault();
        if (fixedScan == null) { return false; }
        var r = fixedScan.ReadScan(ref resStr);
        var rstr = r ? "成功" : "失败";
        fixedScan.SetScanlog($"{DateTime.Now:G} 读取编码 {rstr} {resStr}");
        return r;
    }

    public string GetScanLog(IConnectionFixedScan fixedScan)
    {
        return fixedScan.GetScanLog();
    }

    public string GetScanLog(string name)
    {
        var fixedScan = m_connectionFixedScanList.Where(it => it.FixedScan.Name == name).FirstOrDefault();
        if (fixedScan == null) { return string.Empty; }
        return GetScanLog(fixedScan);
    }
}
