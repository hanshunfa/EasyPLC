
namespace EasyPlc.Application;

/// <summary>
/// 添加RFID
/// </summary>
public class SygoleRfidAddInput : SygoleRfid
{
}

/// <summary>
/// 添加RFID
/// </summary>
public class SygoleRfidEditInput : SygoleRfidAddInput
{
    /// <summary>
    /// Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}
