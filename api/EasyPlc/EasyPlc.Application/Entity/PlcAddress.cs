namespace EasyPlc.Application;

/// <summary>
/// 地址
/// </summary>
[SugarTable("plc_address", TableDescription = "PLC地址")]
[Tenant(SqlsugarConst.DB_Default)]
[CodeGen]
public class PlcAddress : BaseEntity
{
    /// <summary>
    /// PLC id
    ///</summary>
    [SugarColumn(ColumnName = "PlcId", ColumnDescription = "PLCid", IsNullable = true)]
    public virtual long PlcId { get; set; }

    /// <summary>
    /// 资源 id
    ///</summary>
    [SugarColumn(ColumnName = "ResourceId", ColumnDescription = "资源id", IsNullable = true)]
    public virtual long ResourceId { get; set; }
    /// <summary>
    /// 名称
    ///</summary>
    [SugarColumn(ColumnName = "Name", ColumnDescription = "名称", Length = 200, IsNullable = true)]
    public virtual string Name { get; set; }

    /// <summary>
    /// 编码
    ///</summary>
    [SugarColumn(ColumnName = "Code", ColumnDescription = "编码", Length = 200, IsNullable = false)]
    public string Code { get; set; }

    /// <summary>
    /// 分类
    ///</summary>
    [SugarColumn(ColumnName = "Category", ColumnDescription = "分类", Length = 200, IsNullable = true)]
    public virtual string Category { get; set; }

    /// <summary>
    /// 开始地址
    /// </summary>
    [SugarColumn(ColumnName = "StartAddr", ColumnDescription = "开始地址", Length = 200, IsNullable = false)]
    public virtual string StartAddr { get; set; }

    /// <summary>
    /// 地址长度-通过对象计算得出
    /// </summary>
    [SugarColumn(ColumnName = "AddrLenght", ColumnDescription = "地址长度", Length = 200, IsNullable = true)]
    public virtual int? AddrLenght { get; set; }

    /// <summary>
    /// 排序码
    ///</summary>
    [SugarColumn(ColumnName = "SortCode", ColumnDescription = "排序码", IsNullable = true)]
    public int? SortCode { get; set; }
}
