namespace EasyPlc.Plugin.Scan;

public interface IConnectionFixedScan
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <returns></returns>
    void Init(FixedScan fixedScan);
    /// <summary>
    /// 读取扫码枪
    /// </summary>
    /// <returns></returns>
    bool ReadScan(ref string resStr);
    /// <summary>
    /// 获取读写日志
    /// </summary>
    /// <returns></returns>
    string GetScanLog();
    /// <summary>
    /// 设置读写日志
    /// </summary>
    /// <param name="strLog"></param>
    void SetScanlog(string strLog);
    /// <summary>
    /// 关闭TCP
    /// </summary>
    void CloseTcpClient();
}
