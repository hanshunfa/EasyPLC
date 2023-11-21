
using EasyPlc.System;

namespace EasyPlc.Plugin.SygoleRFID;

public interface IConnectionSygoleRFID
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <returns></returns>
    void Init(SygoleRfid rfidSygole);
    /// <summary>
    /// 读取RFID
    /// </summary>
    /// <returns></returns>
    bool ReadRFID(int readLen, ref string resStr);
    /// <summary>
    /// 写入RFID
    /// </summary>
    /// <returns></returns>
    bool WriteRFID(string writeStr, ref string msg);
    /// <summary>
    /// 获取读写日志
    /// </summary>
    /// <returns></returns>
    string GetRFIDLog();
    /// <summary>
    /// 设置读写日志
    /// </summary>
    /// <param name="strLog"></param>
    void SetRFIDlog(string strLog);
    /// <summary>
    /// 关闭TCP
    /// </summary>
    void CloseTcpClient();
}
