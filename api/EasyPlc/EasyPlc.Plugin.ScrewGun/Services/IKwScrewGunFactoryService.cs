
namespace EasyPlc.Plugin.ScrewGun;

public interface IKwScrewGunFactoryService : ISingleton
{
    /// <summary>
    /// 设置需要连接的螺丝枪信息
    /// </summary>
    /// <returns></returns>
    Task SetKwScrewGunInfoList();
    /// <summary>
    /// 开始服务
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    /// <returns></returns>
    Task<bool> StartTcpServer(string ip = "127.0.0.1", ushort port = 12345);
    /// <summary>
    /// 停止服务
    /// </summary>
    /// <returns></returns>
    void StopTcpServer();
    /// <summary>
    /// 获取螺丝枪
    /// </summary>
    /// <returns></returns>
    List<KwScrewGunOutput> GetKwScrewGuns();
    /// <summary>
    /// 获取指定IP螺丝枪最新反馈信息
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    RecvOutput GetNewRecvInfo(string ip);
    /// <summary>
    /// 获取指定名称螺丝枪最新反馈信息
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    RecvOutput GetNewRecvInfoByName(string name);
    /// <summary>
    /// /// 获取指定IP螺丝枪最新反馈信息
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="maxGet"></param>
    /// <param name="interval"></param>
    /// <returns></returns>
    RecvOutput GetNewRecvInfoAndUsed(string name, int maxGet = 10, int interval = 100);
    /// <summary>
    /// 设置螺丝枪反馈已被使用
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="code"></param>
    void SetRecvInfoUse(string ip, string code);
}
