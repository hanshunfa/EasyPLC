
namespace EasyPlc.Application;

[SugarTable("sygole_rfid", TableDescription = "思谷RFID")]
[Tenant(SqlsugarConst.DB_Default)]
public class SygoleRfid : BaseEntity
{
    /// <summary>
    /// 名称
    ///</summary>
    [SugarColumn(ColumnName = "Name", ColumnDescription = "名称", Length = 200)]
    public string Name { get; set; }
    /// <summary>
    /// 编码
    ///</summary>
    [SugarColumn(ColumnName = "Code", ColumnDescription = "编码", Length = 200)]
    public string Code { get; set; }
    /// <summary>
    /// IP地址
    /// </summary>
    [SugarColumn(ColumnName = "Ip", ColumnDescription = "IP地址", Length = 200)]
    public string Ip { get; set; }
    /// <summary>
    /// 端口号
    /// </summary>
    [SugarColumn(ColumnName = "Port", ColumnDescription = "端口号")]
    public int Port { get; set; }
    /// <summary>
    /// ReaderId
    /// </summary>
    [SugarColumn(ColumnName = "ReaderId", ColumnDescription = "ReaderId")]
    public int ReaderId { get; set; }
    /// <summary>
    /// 排序码
    ///</summary>
    [SugarColumn(ColumnName = "SortCode", ColumnDescription = "排序码", IsNullable = true)]
    public int? SortCode { get; set; }

    /// <summary>
    /// 是否连接
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public bool IsConn { get;set; }
}
