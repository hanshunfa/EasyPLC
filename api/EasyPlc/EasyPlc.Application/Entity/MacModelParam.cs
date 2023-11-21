namespace EasyPlc.Application;

[SugarTable("mac_model_param", TableDescription = "型号参数")]
[Tenant(SqlsugarConst.DB_Default)]
public class MacModelParam : BaseEntity
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
    public string Code { get; set; }

    /// <summary>
    /// 分类
    ///</summary>
    [SugarColumn(ColumnName = "Category", ColumnDescription = "分类", Length = 200)]
    public virtual string Category { get; set; }

    /// <summary>
    /// 参数类型
    /// </summary>
    [SugarColumn(ColumnName = "ParamType", ColumnDescription = "参数类型", Length = 200)]
    public virtual string ParamType { get; set; }
    /// <summary>
    /// 参数值
    /// </summary>
    [SugarColumn(ColumnName = "ParamValue", ColumnDescription = "参数值", Length = 200)]
    public virtual string ParamValue { get; set; }
    /// <summary>
    /// 参数单位
    /// </summary>
    [SugarColumn(ColumnName = "ParamUnit", ColumnDescription = "参数单位", Length = 200)]
    public string ParamUnit { get; set; }

    /// <summary>
    /// 排序码
    ///</summary>
    [SugarColumn(ColumnName = "SortCode", ColumnDescription = "排序码", IsNullable = true)]
    public int? SortCode { get; set; }
}
