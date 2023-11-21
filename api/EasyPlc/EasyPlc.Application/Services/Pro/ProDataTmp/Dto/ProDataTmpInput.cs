namespace EasyPlc.Application;

public class ProDataTmpAddInput : ProDataTmp
{

}

public class ProDataTmpEditInput : ProDataTmpAddInput
{
    /// <summary>
    /// Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}
