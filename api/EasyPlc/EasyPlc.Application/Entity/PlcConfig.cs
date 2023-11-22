using Org.BouncyCastle.Crypto.Agreement.JPake;

namespace EasyPlc.Application;

/// <summary>
/// 组织
///</summary>
[SugarTable("plc_config", TableDescription = "PLC配置")]
[Tenant(SqlsugarConst.DB_Default)]
[CodeGen]
public class PlcConfig : BaseEntity
{
    /// <summary>
    /// 父id
    ///</summary>
    [SugarColumn(ColumnName = "ParentId", ColumnDescription = "父id")]
    public long ParentId { get; set; }

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
    public List<PlcConfig> Children { get; set; }
    /// <summary>
    /// 设置为叶子节点(设置了loadData时有效)
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public bool? IsLeaf { get; set; }
}

/// <summary>
/// PLC扩展信息
/// </summary>
public class PlcExtJson
{
    /// <summary>
    /// 类型
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// 版本
    /// </summary>
    public string Version { get; set;}
    /// <summary>
    /// IP
    /// </summary>
    public string Ip { get; set; }
    /// <summary>
    /// 端口
    /// </summary>
    public int Port { get; set;}
    /// <summary>
    /// 支架号 
    /// </summary>
    public byte Rack { get; set; }
    /// <summary>
    /// 插槽号 
    /// </summary>
    public byte Slot { get; set; }
}
public class AddrExtJson
{
    /// <summary>
    /// 起始地址
    /// </summary>
    public string StartAddr { get; set; }
}