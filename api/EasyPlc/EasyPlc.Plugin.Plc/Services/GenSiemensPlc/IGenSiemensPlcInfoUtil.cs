namespace EasyPlc.Plugin.Plc;

public interface IGenSiemensPlcInfoUtil : ITransient
{
    SiemensPlcInfo[] GetSiemensPLCInfo();
}
