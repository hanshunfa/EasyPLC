
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
}
