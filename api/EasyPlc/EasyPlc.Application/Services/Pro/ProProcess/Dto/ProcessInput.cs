
namespace EasyPlc.Application;

public class CableInput
{
    public string CableSN { get; set; }
}

public class RepairGetInput
{
    public long WorkingStepId { get; set; }
}

public class RepairSetInput
{
    public long WorkingStepId { get; set; }
    public long FlowId { get; set; }
}
