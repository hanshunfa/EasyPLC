namespace EasyPlc.Application;

[SugarTable("mac_flow", TableDescription = "流程")]
[Tenant(SqlsugarConst.DB_Default)]
public class MacFlow : BaseEntity
{
    /// <summary>
    /// 父id
    ///</summary>
    [SugarColumn(ColumnName = "ParentId", ColumnDescription = "父id")]
    public long ParentId { get; set; }
    /// <summary>
    /// 型号id
    ///</summary>
    [SugarColumn(ColumnName = "ModelId", ColumnDescription = "型号id")]
    public long ModelId { get; set; }
    /// <summary>
    /// 设备id
    /// </summary>
    [SugarColumn(ColumnName = "EquipmentId", ColumnDescription = "设备id")]
    public long EquipmentId { get; set; }
    /// <summary>
    /// 名称
    ///</summary>
    [SugarColumn(ColumnName = "Name", ColumnDescription = "名称", Length = 200)]
    public string Name { get; set; }
    /// <summary>
    /// 全称
    ///</summary>
    [SugarColumn(ColumnName = "Names", ColumnDescription = "全称", Length = 500)]
    public string Names { get; set; }
    /// <summary>
    /// 编码
    ///</summary>
    [SugarColumn(ColumnName = "Code", ColumnDescription = "编码", Length = 200)]
    public string Code { get; set; }

    /// <summary>
    /// 分类
    ///</summary>
    [SugarColumn(ColumnName = "Category", ColumnDescription = "分类", Length = 200)]
    public string Category { get; set; }
    /// <summary>
    /// 排序码
    ///</summary>
    [SugarColumn(ColumnName = "SortCode", ColumnDescription = "排序码", IsNullable = true)]
    public int? SortCode { get; set; }
    /// <summary>
    /// 子节点
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public List<MacFlow> Children { get; set; }
}
