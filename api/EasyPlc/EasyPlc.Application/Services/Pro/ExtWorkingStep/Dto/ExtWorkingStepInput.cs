
namespace EasyPlc.Application;

public class ExtWorkingStepAddInput : ExtWorkingStep
{

}

public class ExtWorkingStepEditInput : ExtWorkingStepAddInput
{
    /// <summary>
    /// Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}
