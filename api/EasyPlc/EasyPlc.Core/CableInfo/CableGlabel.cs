
namespace EasyPlc.Core;

public static class CableGlabel
{
    public static bool IsWorking { get; set; } = false;
    public static string CarrierSN { get; set; }
    public static string CableSN { get; set; }
    public static long DataTmpId { get; set; }

    public static void StartWorking(string carrierSN)
    {
        IsWorking = true;
        CarrierSN = carrierSN;
        CableSN = string.Empty;
    }
    public static void EndWorking()
    {
        IsWorking = false;
        CarrierSN = string.Empty;
        CableSN = string.Empty;
    }
    public static void SetCalble(string cableSN)
    {
        CableSN = cableSN;
    }

    public static void SetDataTmpId(long dataTmpId)
    {
        DataTmpId = dataTmpId;
    }
    public static long GetDataTmpId()
    {
        return DataTmpId;
    }
}
