
using Furion.DependencyInjection;

namespace EasyPlc.Plugin.Plc;

public interface IConnectionSiemensPLC : ITransient
{
    /// <summary>
    /// 设置PLCInfo
    /// </summary>
    /// <param name="plcInfo"></param>
    void SetPlcInfo(SiemensPlcInfo plcInfo);
    /// <summary>
    /// 启动plc
    /// </summary>
    /// <returns></returns>
    string StartWork();
    /// <summary>
    /// 停止PLC
    /// </summary>
    /// <returns></returns>
    string StopWork();
    /// <summary>
    /// 设置写公共区内容
    /// </summary>
    /// <param name="publicInfo"></param>
    /// <returns></returns>
    bool SetWritePi(PublicInfo publicInfo);
    /// <summary>
    /// 设置写事件区内容
    /// </summary>
    /// <param name="ei"></param>
    /// <returns></returns>
    bool SetEventInfo(EventInfo ei);
}
