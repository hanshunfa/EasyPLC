
namespace EasyPlc.Application;

/// <summary>
/// 添加镭射
/// </summary>
public class ProLaserAddInput : ProLaser
{
}

public class ProLaserPageInput : BasePageInput
{

}

/// <summary>
/// 编辑镭射
/// </summary>
public class ProLaserEditInput : ProLaserAddInput
{
    /// <summary>
    /// Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}
