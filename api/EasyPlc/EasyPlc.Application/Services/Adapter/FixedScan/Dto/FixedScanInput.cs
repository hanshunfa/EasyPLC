
namespace EasyPlc.Application;

/// <summary>
/// 添加RFID
/// </summary>
public class FixedScanAddInput : FixedScan
{
}

/// <summary>
/// 添加RFID
/// </summary>
public class FixedScanEditInput : FixedScanAddInput
{
    /// <summary>
    /// Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}
