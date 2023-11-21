
namespace EasyPlc.Application;

/// <summary>
/// 添加工单
/// </summary>
public class ProOrderAddInput : ProOrder
{
}

public class ProOrderPageInput : BasePageInput
{
    /// <summary>
    /// 工单ID
    /// </summary>
    public long OrderId { get; set; }
}
public class ProPageInput : BasePageInput
{
    /// <summary>
    /// 工单ID
    /// </summary>
    public long Id { get; set; }
}

/// <summary>
/// 编辑工单
/// </summary>
public class ProOrderEditInput : ProOrderAddInput
{
    /// <summary>
    /// Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}


public class ProOrderStatusInput : BaseIdInput
{
    /// <summary>
    /// 状态
    /// </summary>
    public string Status { get; set; }  
}