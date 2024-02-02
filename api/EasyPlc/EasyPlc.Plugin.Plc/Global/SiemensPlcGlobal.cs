namespace EasyPlc.Plugin.Plc.Global;

/// <summary>
/// SiemensPlc列表静态对象
/// </summary>
public static class SiemensPlcGlobal
{
    /// <summary>
    /// SiemensPlc列表集合
    /// </summary>
    public static List<SiemensPlcInfo> ListSiemensPlcInfo = new List<SiemensPlcInfo>();
    /// <summary>
    /// 定义类型列表
    /// </summary>
    public static List<Type> ListType = new List<Type>();

    public static Type GetMyType(this List<Type> types, string typeName)
    {
        return types.FirstOrDefault(it => it.Name == typeName);
    }
}