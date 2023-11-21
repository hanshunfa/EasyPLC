namespace EasyPlc.Plugin.Plc;

/// <summary>
///SiemensPLC信息设定工具类
/// <summary>
public class SiemensPLCInfoUtil : IGenSiemensPlcInfoUtil
{
    /// <summary>
    ///SiemensPLC获取信息
    /// <summary>
    public SiemensPlcInfo[] GetSiemensPLCInfo()
    {
        return new SiemensPlcInfo[]
        {
            new SiemensPlcInfo()
            {
                Name = "OP10",
                IP = "192.168.0.50",
                Port = 102,
                Rack = 0,
                Slot = 0,
                Version = "S1500",
                PI = new PublicInfo()
                {
                    ReadAddr = "DB5001.0",
                    ReadClassName = "General_PI_PlcToEap",
                    WriteAddr = "DB5000.0",
                    WriteClassName = "General_PI_EapToPlc",
                },
                 EIs = new List<EventInfo>
                {
                    new EventInfo()
                    {
                        Idx = 0,
                        ReadAddr = "DB5001.200",
                        ReadClassName = "Base_PlcToEap_IN",
                        WriteAddr = "DB5000.200",
                        WriteClassName = "Base_EapToPlc_IN",
                    },
                    new EventInfo()
                    {
                        Idx = 1,
                        ReadAddr = "DB5001.202",
                        ReadClassName = "Base_PlcToEap_OUT",
                        WriteAddr = "DB5000.414",
                        WriteClassName = "Base_EapToPlc_OUT",
                    },
                    new EventInfo()
                    {
                        Idx = 2,
                        ReadAddr = "DB5001.206",
                        ReadClassName = "Base_PlcToEap_IN",
                        WriteAddr = "DB5000.526",
                        WriteClassName = "Base_EapToPlc_IN",
                    },
                    new EventInfo()
                    {
                        Idx = 3,
                        ReadAddr = "DB5001.208",
                        ReadClassName = "Base_PlcToEap_OUT",
                        WriteAddr = "DB5000.740",
                        WriteClassName = "Base_EapToPlc_OUT",
                    },
                    new EventInfo()
                    {
                        Idx = 4,
                        ReadAddr = "DB5001.212",
                        ReadClassName = "Base_PlcToEap_IN",
                        WriteAddr = "DB5000.852",
                        WriteClassName = "Base_EapToPlc_IN",
                    },
                    new EventInfo()
                    {
                        Idx = 5,
                        ReadAddr = "DB5001.214",
                        ReadClassName = "OP2001_Base_PlcToEap_OUT",
                        WriteAddr = "DB5000.1066",
                        WriteClassName = "Base_EapToPlc_OUT",
                    },
                    new EventInfo()
                    {
                        Idx = 6,
                        ReadAddr = "DB5001.234",
                        ReadClassName = "Base_PlcToEap_IN",
                        WriteAddr = "DB5000.1178",
                        WriteClassName = "Base_EapToPlc_IN",
                    },
                    new EventInfo()
                    {
                        Idx = 7,
                        ReadAddr = "DB5001.236",
                        ReadClassName = "OP2002_Base_PlcToEap_OUT",
                        WriteAddr = "DB5000.1392",
                        WriteClassName = "Base_EapToPlc_OUT",
                    },
                    new EventInfo()
                    {
                        Idx = 8,
                        ReadAddr = "DB5001.268",
                        ReadClassName = "Base_PlcToEap_IN",
                        WriteAddr = "DB5000.1504",
                        WriteClassName = "Base_EapToPlc_IN",
                    },
                    new EventInfo()
                    {
                        Idx = 9,
                        ReadAddr = "DB5001.270",
                        ReadClassName = "OP3001_Base_PlcToEap_OUT",
                        WriteAddr = "DB5000.1718",
                        WriteClassName = "Base_EapToPlc_OUT",
                    },
                    new EventInfo()
                    {
                        Idx = 10,
                        ReadAddr = "DB5001.324",
                        ReadClassName = "Base_PlcToEap_IN",
                        WriteAddr = "DB5000.1830",
                        WriteClassName = "Base_EapToPlc_IN",
                    },
                    new EventInfo()
                    {
                        Idx = 11,
                        ReadAddr = "DB5001.326",
                        ReadClassName = "OP3002_Base_PlcToEap_OUT",
                        WriteAddr = "DB5000.2044",
                        WriteClassName = "Base_EapToPlc_OUT",
                    },
                    new EventInfo()
                    {
                        Idx = 12,
                        ReadAddr = "DB5001.338",
                        ReadClassName = "Base_PlcToEap_IN",
                        WriteAddr = "DB5000.2156",
                        WriteClassName = "Base_EapToPlc_IN",
                    },
                    new EventInfo()
                    {
                        Idx = 13,
                        ReadAddr = "DB5001.340",
                        ReadClassName = "Base_PlcToEap_OUT",
                        WriteAddr = "DB5000.2370",
                        WriteClassName = "Base_EapToPlc_OUT",
                    },
                    new EventInfo()
                    {
                        Idx = 14,
                        ReadAddr = "DB5001.344",
                        ReadClassName = "Base_PlcToEap_IN",
                        WriteAddr = "DB5000.2482",
                        WriteClassName = "Base_EapToPlc_IN",
                    },
                    new EventInfo()
                    {
                        Idx = 15,
                        ReadAddr = "DB5001.346",
                        ReadClassName = "OP4002_Base_PlcToEap_OUT",
                        WriteAddr = "DB5000.2696",
                        WriteClassName = "Base_EapToPlc_OUT",
                    },
                    new EventInfo()
                    {
                        Idx = 16,
                        ReadAddr = "DB5001.388",
                        ReadClassName = "Base_PlcToEap_IN",
                        WriteAddr = "DB5000.2808",
                        WriteClassName = "Base_EapToPlc_IN",
                    },
                    new EventInfo()
                    {
                        Idx = 17,
                        ReadAddr = "DB5001.390",
                        ReadClassName = "OP5001_Base_PlcToEap_OUT",
                        WriteAddr = "DB5000.3022",
                        WriteClassName = "Base_EapToPlc_OUT",
                    },
                    new EventInfo()
                    {
                        Idx = 18,
                        ReadAddr = "DB5001.398",
                        ReadClassName = "Base_PlcToEap_IN",
                        WriteAddr = "DB5000.3134",
                        WriteClassName = "Base_EapToPlc_IN",
                    },
                    new EventInfo()
                    {
                        Idx = 19,
                        ReadAddr = "DB5001.400",
                        ReadClassName = "OP5002_Base_PlcToEap_OUT",
                        WriteAddr = "DB5000.3348",
                        WriteClassName = "Base_EapToPlc_OUT",
                    },
                    new EventInfo()
                    {
                        Idx = 20,
                        ReadAddr = "DB5001.430",
                        ReadClassName = "OP3001_Base_PlcToEap_ScrewGun",
                        WriteAddr = "DB5000.3460",
                        WriteClassName = "OP3001_Base_EapToPlc_ScrewGun",
                    },
                    new EventInfo()
                    {
                        Idx = 21,
                        ReadAddr = "DB5001.478",
                        ReadClassName = "OP4002_Base_PlcToEap_ScrewGun",
                        WriteAddr = "DB5000.3554",
                        WriteClassName = "OP4002_Base_EapToPlc_ScrewGun",
                    },
                    new EventInfo()
                    {
                        Idx = 22,
                        ReadAddr = "DB5001.526",
                        ReadClassName = "OP5002_Base_PlcToEap_ScrewGun",
                        WriteAddr = "DB5000.3648",
                        WriteClassName = "OP5002_Base_EapToPlc_ScrewGun",
                    },
                }
            },
        };
    }
}
