
namespace EasyPlc.Application;

[SugarTable("fixed_scan", TableDescription = "固定扫码枪")]
[Tenant(SqlsugarConst.DB_Default)]
public class FixedScan : BaseEntity
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
    /// 排序码
    ///</summary>
    [SugarColumn(ColumnName = "SortCode", ColumnDescription = "排序码", IsNullable = true)]
    public int? SortCode { get; set; }
    /// <summary>
    /// 是否连接
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public bool IsConn { get; set; }
}
