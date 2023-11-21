
namespace EasyPlc.Plugin.SygoleRFID;

public interface ISygoleFactoryService : ISingleton
{
    /// <summary>
    /// 初始化工厂
    /// </summary>
    /// <param name="qz">强制初始化工厂</param>
    /// <returns></returns>
    Task InitFactory(bool qz = false);

    /// <summary>
    /// 获取当前RFID
    /// </summary>
    /// <returns></returns>
    List<ConnectionSygoleRFID> GetConnections();

    /// <summary>
    /// 读取RFID
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="readLen"></param>
    /// <param name="resStr"></param>
    /// <returns></returns>
    bool ReadRFID(IConnectionSygoleRFID sygoleRFID, int readLen, ref string resStr);
    /// <summary>
    /// 读取RFID
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="readLen"></param>
    /// <param name="resStr"></param>
    /// <returns></returns>
    bool ReadRFID(string name, int readLen, ref string resStr);
    /// <summary>
    /// 写RFID
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="writeStr"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    bool WriteRFID(IConnectionSygoleRFID sygoleRFID, string writeStr, ref string msg);
    /// <summary>
    /// 写RFID
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="writeStr"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    bool WriteRFID(string name, string writeStr, ref string msg);
    /// <summary>
    /// 获取读写日志
    /// </summary>
    /// <param name="sygoleRFID"></param>
    /// <returns></returns>
    string GetRFIDLog(IConnectionSygoleRFID sygoleRFID);
    /// <summary>
    /// 获取读写日志
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    string GetRFIDLog(string name);
}
