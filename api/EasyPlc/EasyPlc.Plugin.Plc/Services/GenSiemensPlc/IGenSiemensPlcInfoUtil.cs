namespace EasyPlc.Plugin.Plc;

public interface IGenSiemensPlcInfoUtil : ITransient
{
    /// <summary>
    /// 根据配置信息生成PLC通讯信息
    /// </summary>
    /// <returns></returns>
    Task<List<SiemensPlcInfo>> GenSiemensPLCInfoList();
}
