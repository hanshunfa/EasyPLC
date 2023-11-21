
namespace EasyPlc.Application;
[SugarTable("pro_label", TableDescription = "标签配置表")]
[Tenant(SqlsugarConst.DB_Default)]
public class ProLabel : BaseEntity
{
    /// <summary>
    /// 工单ID
    /// </summary>
    [SugarColumn(ColumnName = "OrderId", ColumnDescription = "工单ID")]
    public long OrderId { get; set; }
    /// <summary>
    /// 驱动名称
    /// </summary>
    [SugarColumn(ColumnName = "DriveName", ColumnDescription = "驱动名称")]
    public string DriveName { get; set; }
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
