
namespace EasyPlc.Plugin.Plc;


public interface ISiemensPlcFactoryService : ISingleton
{
    /// <summary>
    /// 使用
    /// </summary>
    void Use();
    /// <summary>
    /// 初始化工厂，创建PLC通讯实列
    /// </summary>
    string InitFactory();
    /// <summary>
    /// 全部开始
    /// </summary>
    /// <param name="connectionSiemensPLC"></param>
    /// <returns></returns>
    void StartPLC();
    /// <summary>
    /// 开始
    /// </summary>
    /// <param name="connectionSiemensPLC"></param>
    /// <returns></returns>
    string StartPLC(ConnectionSiemensPLC connectionSiemensPLC);
    /// <summary>
    /// 开始
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    string StartPLC(string ip);
    /// <summary>
    /// 结束
    /// </summary>
    /// <param name="connectionSiemensPLC"></param>
    /// <returns></returns>
    string StopPLC(ConnectionSiemensPLC connectionSiemensPLC);
    /// <summary>
    /// 结束
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    string StopPLC(string ip);
    /// <summary>
    /// 全部结束
    /// </summary>
    /// <param name="connectionSiemensPLC"></param>
    /// <returns></returns>
    void StopPLC();
    /// <summary>
    /// 获取连接PLC
    /// </summary>
    /// <returns></returns>
    List<ConnectionSiemensPLC> GetConnectionSiemensPLCList();
}
