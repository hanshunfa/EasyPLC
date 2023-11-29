
namespace EasyPlc.Application;
/// <summary>
/// PLC资源
/// </summary>
[SugarTable("plc_resource", TableDescription = "PLC资源")]
[Tenant(SqlsugarConst.DB_Default)]
public class PlcResource : BaseEntity
{
    /// <summary>
    /// 父id
    ///</summary>
    [SugarColumn(ColumnName = "ParentId", ColumnDescription = "父id", IsNullable = true)]
    public virtual long? ParentId { get; set; }

    /// <summary>
    /// 标题
    ///</summary>
    [SugarColumn(ColumnName = "Title", ColumnDescription = "标题", Length = 200)]
    public virtual string Title { get; set; }

    /// <summary>
    /// 别名
    ///</summary>
    [SugarColumn(ColumnName = "Name", ColumnDescription = "别名", Length = 200, IsNullable = true)]
    public string Name { get; set; }

    /// <summary>
    /// 编码
    ///</summary>
    [SugarColumn(ColumnName = "Code", ColumnDescription = "编码", Length = 200, IsNullable = true)]
    public virtual string Code { get; set; }

    /// <summary>
    /// 数据类型
    /// </summary>
    [SugarColumn(ColumnName = "ValueType", ColumnDescription = "数据类型", Length = 200, IsNullable = true)]
    public string ValueType { get; set; }
    /// <summary>
    /// 数据长度
    /// </summary>
    [SugarColumn(ColumnName = "ValueLength", ColumnDescription = "数据长度")]
    public int ValueLength { get; set; }
    /// <summary>
    /// 暂用字节数量
    /// </summary>
    [SugarColumn(ColumnName = "ByteCount", ColumnDescription = "暂用字节数量")]
    public int ByteCount { get; set; }
    /// <summary>
    /// 分类
    ///</summary>
    [SugarColumn(ColumnName = "Category", ColumnDescription = "分类", Length = 200)]
    public string Category { get; set; }

    /// <summary>
    /// 开始地址
    /// </summary>
    [SugarColumn(ColumnName = "StartAdrr", ColumnDescription = "开始地址", Length = 200, IsNullable = true)]
    public string StartAdrr { get; set; }

    /// <summary>
    /// 排序码
    ///</summary>
    [SugarColumn(ColumnName = "SortCode", ColumnDescription = "排序码", IsNullable = true)]
    public int? SortCode { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public dynamic Value { get; set; }
    /// <summary>
    /// 字节点
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public List<PlcResource> Children { get; set; }
    /// <summary>
    /// 设置为叶子节点(设置了loadData时有效)
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public bool? IsLeaf { get; set; }
}

