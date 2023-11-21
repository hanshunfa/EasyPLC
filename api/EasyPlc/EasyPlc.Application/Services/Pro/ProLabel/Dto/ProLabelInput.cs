
namespace EasyPlc.Application;

/// <summary>
/// 添加标签
/// </summary>
public class ProLabelAddInput : ProLabel
{
}

public class ProLabelPageInput : BasePageInput
{

}

/// <summary>
/// 编辑标签
/// </summary>
public class ProLabelEditInput : ProLabelAddInput
{
    /// <summary>
    /// Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}
