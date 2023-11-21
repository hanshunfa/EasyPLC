namespace EasyPlc.Application;

[SugarTable("ext_working_step", TableDescription = "扩展产品加工过程")]
[Tenant(SqlsugarConst.DB_Default)]
public class ExtWorkingStep : BaseEntity
{
    /// <summary>
    /// 加工过程ID
    /// </summary>
    [SugarColumn(ColumnName = "WorkingStepId", ColumnDescription = "加工过程ID")]
    public long WorkingStepId { get; set; }
    /// <summary>
    /// 镭射信息
    /// </summary>
    [SugarColumn(ColumnName = "LaserJson", ColumnDescription = "镭射信息", ColumnDataType = StaticConfig.CodeFirst_BigString, IsNullable = true)]
    public string LaserJson { get; set; }
    /// <summary>
    /// 镭射信息
    /// </summary>
    [SugarColumn(ColumnName = "LabelJson", ColumnDescription = "标签信息", ColumnDataType = StaticConfig.CodeFirst_BigString, IsNullable = true)]
    public string LabelJson { get; set; }
}

public class LabelSend
{
    public string Path { get; set; }
    public string DriveName { get; set; }
    public List<LabelSendParam> Params { get; set; } = new List<LabelSendParam>();
}

public class LabelSendParam
{
    public string Key { get; set; }
    public string Value { get; set; }
}
