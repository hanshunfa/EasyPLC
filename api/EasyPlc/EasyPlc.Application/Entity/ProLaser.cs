
namespace EasyPlc.Application;
[SugarTable("pro_laser", TableDescription = "镭射配置表")]
[Tenant(SqlsugarConst.DB_Default)]
public class ProLaser : BaseEntity
{
    /// <summary>
    /// 工单ID
    /// </summary>
    [SugarColumn(ColumnName = "OrderId", ColumnDescription = "工单ID")]
    public long OrderId { get; set; }
    /// <summary>
    /// 模板路径
    /// </summary>
    [SugarColumn(ColumnName = "Path", ColumnDescription = "模板路径")]
    public string Path { get; set; }
    /// <summary>
    /// 序列号
    /// </summary>
    [SugarColumn(ColumnName = "SerialNum", ColumnDescription = "序列号")]
    public int SerialNum { get; set; } = 1;

    /// <summary>
    /// 预览信息
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public string PreviewJson { get; set; }
}

public class LaserSend
{
    public string Path { get; set; }
    public List<string> DataList { get; set; } = new List<string>();
}
