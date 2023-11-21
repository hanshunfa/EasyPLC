namespace EasyPlc.Plugin.Gen;

/// <summary>
/// 代码生成基础服务
/// </summary>
public interface IGenbasicService : ITransient
{
    /// <summary>
    /// 执行代码生成:对象结构定义
    /// </summary>
    /// <param></param>
    /// <returns></returns>
    Task ExecGenObjDefinedPro();

    /// <summary>
    /// 执行代码生成:PLC信息
    /// </summary>
    /// <param ></param>
    /// <returns></returns>
    Task ExecGenSiemensPlcInfoPro();

}