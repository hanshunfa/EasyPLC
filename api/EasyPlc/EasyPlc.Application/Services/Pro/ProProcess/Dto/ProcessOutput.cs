
namespace EasyPlc.Application;

public class CableOutput
{
    public bool IsSucceed { get; set; }
    public int Code { get; set; }
    public string Msg { get; set; }
}

public class RepairGetOutput
{
    public bool IsSucceed { get; set; }
    public int Code { get; set; }
    public string Msg { get; set; }
    public List<MyFlow> Flows { get; set; }
}

public class MyFlow
{
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsChecked { get; set; }
}

public class RepairSetOutput
{
    public bool IsSucceed { get; set; }
    public int Code { get; set; }
    public string Msg { get; set; }
}
