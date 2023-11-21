
namespace EasyPlc.Application;

/// <summary>
/// 添加RFID
/// </summary>
public class KwScrewGunAddInput : KwScrewGun
{
}

/// <summary>
/// 添加RFID
/// </summary>
public class KwScrewGunEditInput : KwScrewGunAddInput
{
    /// <summary>
    /// Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}
