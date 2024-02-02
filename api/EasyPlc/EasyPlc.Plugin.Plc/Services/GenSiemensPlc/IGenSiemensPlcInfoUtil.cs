namespace EasyPlc.Plugin.Plc;

public interface IGenSiemensPlcInfoUtil : ITransient
{
    /// <summary>
    /// 根据配置信息生成PLC通讯信息
    /// </summary>
    /// <returns></returns>
    Task<List<SiemensPlcInfo>> GenSiemensPlcInfoList();
    /// <summary>
    /// 根据配置信息生成全局PLC信息
    /// </summary>
    /// <returns></returns>
    Task<List<SiemensPlcInfo>> GenSiemensPlcInfoList2Global();
    /// <summary>
    /// 根据配置信息生成所有类型定义
    /// </summary>
    /// <returns></returns>
    Task<List<Type>> GenSiemensPlcObjType2Global();
}
