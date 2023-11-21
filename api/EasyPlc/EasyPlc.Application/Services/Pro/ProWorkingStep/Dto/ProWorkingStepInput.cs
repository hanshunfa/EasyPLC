
namespace EasyPlc.Application;

public class ProWorkingStepAddInput : ProWorkingStep
{
}

public class ProWorkingStepEditInput : ProWorkingStepAddInput
{
    /// <summary>
    /// Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}
