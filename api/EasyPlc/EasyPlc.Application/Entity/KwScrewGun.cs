
namespace EasyPlc.Application;

[SugarTable("kw_screw_gun", TableDescription = "螺丝枪")]
[Tenant(SqlsugarConst.DB_Default)]
public class KwScrewGun : BaseEntity
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
    /// 排序码
    ///</summary>
    [SugarColumn(ColumnName = "SortCode", ColumnDescription = "排序码", IsNullable = true)]
    public int? SortCode { get; set; }
}
