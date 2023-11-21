namespace EasyPlc.Application;

[SugarTable("mac_point", TableDescription = "载具位置")]
[Tenant(SqlsugarConst.DB_Default)]
public class MacPoint : BaseEntity
{
    /// <summary>
    /// 父id
    ///</summary>
    [SugarColumn(ColumnName = "CarrierId", ColumnDescription = "载具Id")]
    public virtual long CarrierId { get; set; }
    /// <summary>
    /// 名称
    ///</summary>
    [SugarColumn(ColumnName = "Name", ColumnDescription = "名称", Length = 200)]
    public virtual string Name { get; set; }

    /// <summary>
    /// 编码
    ///</summary>
    [SugarColumn(ColumnName = "Code", ColumnDescription = "编码", Length = 200)]
    public string Code { get; set; }

    /// <summary>
    /// 位置
    /// </summary>
    [SugarColumn(ColumnName = "Point", ColumnDescription = "位置")]
    public virtual int Point { get; set; }
    /// <summary>
    /// 绑定对象编码
    /// </summary>
    [SugarColumn(ColumnName = "BindCode", ColumnDescription = "绑定对象编码")]
    public virtual long BindCode { get; set; }
    /// <summary>
    /// 绑定状态
    /// </summary>
    [SugarColumn(ColumnName = "BindStatus", ColumnDescription = "绑定状态", Length = 200)]
    public virtual string BindStatus { get; set; }

    /// <summary>
    /// 排序码
    ///</summary>
    [SugarColumn(ColumnName = "SortCode", ColumnDescription = "排序码", IsNullable = true)]
    public int? SortCode { get; set; }
}
