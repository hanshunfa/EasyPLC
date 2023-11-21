namespace EasyPlc.Application;

public class ProDataAddInput : ProData
{

}

public class ProDataEditInput : ProDataAddInput
{
    /// <summary>
    /// Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}
public class ProDataPageInput : BasePageInput
{
    /// <summary>
    /// 订单ID
    /// </summary>
    public long OrderId { get; set; }
}