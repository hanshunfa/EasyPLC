namespace EasyPlc.Application;

[SugarTable("mac_carrier", TableDescription = "载具")]
[Tenant(SqlsugarConst.DB_Default)]
public class MacCarrier : BaseEntity
{
    /// <summary>
    /// 父id
    ///</summary>
    [SugarColumn(ColumnName = "ModelId", ColumnDescription = "型号Id")]
    public virtual long ModelId { get; set; }
    /// <summary>
    /// 名称
    ///</summary>
    [SugarColumn(ColumnName = "Name", ColumnDescription = "名称", Length = 200)]
    public virtual string Name { get; set; }

    /// <summary>
    /// 编码
    ///</summary>
    [SugarColumn(ColumnName = "Code", ColumnDescription = "编码", Length = 200)]
    public virtual string Code { get; set; }

    /// <summary>
    /// 分类
    ///</summary>
    [SugarColumn(ColumnName = "Category", ColumnDescription = "分类", Length = 200)]
    public virtual string Category { get; set; }

    /// <summary>
    /// 位置数量
    /// </summary>
    [SugarColumn(ColumnName = "NumberOfPosition", ColumnDescription = "位置数量")]
    public virtual int NumberOfPosition { get; set; }
    /// <summary>
    /// 载具状态
    /// </summary>
    [SugarColumn(ColumnName = "CarrierStatus", ColumnDescription = "载具状态", Length = 200)]
    public virtual string CarrierStatus { get; set; }

    /// <summary>
    /// 排序码
    ///</summary>
    [SugarColumn(ColumnName = "SortCode", ColumnDescription = "排序码", IsNullable = true)]
    public int? SortCode { get; set; }
}
