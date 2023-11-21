namespace EasyPlc.Plugin.Scan;

public interface IFixedScanFactoryService : ISingleton
{
    /// <summary>
    /// 初始化工厂
    /// </summary>
    /// <param name="qz">强制初始化工厂</param>
    /// <returns></returns>
    Task InitFactory(bool qz = false);

    /// <summary>
    /// 获取当前扫码枪
    /// </summary>
    /// <returns></returns>
    List<ConnectionFixedScan> GetConnections();

    /// <summary>
    /// 读取扫码器
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="readLen"></param>
    /// <param name="resStr"></param>
    /// <returns></returns>
    bool ReadScan(IConnectionFixedScan fixedScan, ref string resStr);
    /// <summary>
    /// 读取扫码枪
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="readLen"></param>
    /// <param name="resStr"></param>
    /// <returns></returns>
    bool ReadScan(string name, ref string resStr);
    /// <summary>
    /// 获取读写日志
    /// </summary>
    /// <param name="fixedScan"></param>
    /// <returns></returns>
    string GetScanLog(IConnectionFixedScan fixedScan);
    /// <summary>
    /// 获取读写日志
    /// </summary>
    /// <param name="fixedScan"></param>
    /// <returns></returns>
    string GetScanLog(string name);
}
