namespace EasyPlc.Plugin.Plc;

/// <summary>
/// 西门子PLC
/// </summary>
public class SiemensPlcInfo : BasePlcInfo
{
    public byte Rack { get; set; }
    public byte Slot { get; set; }
    public string Version { get; set; }
}