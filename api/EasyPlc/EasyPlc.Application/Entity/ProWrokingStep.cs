
namespace EasyPlc.Application;

[SugarTable("pro_working_step", TableDescription = "产品加工过程")]
[Tenant(SqlsugarConst.DB_Default)]
public class ProWorkingStep : BaseEntity
{
    /// <summary>
    /// 工单ID
    /// </summary>
    [SugarColumn(ColumnName = "OrderId", ColumnDescription = "工单ID")]
    public long OrderId { get; set; }
    /// <summary>
    /// 产品状态 ok repair scrap
    /// </summary>
    [SugarColumn(ColumnName = "ProductStatus", ColumnDescription = "产品状态", IsNullable = true)]
    public string ProductStatus { get; set; }
    /// <summary>
    /// 当前工位
    /// </summary>
    [SugarColumn(ColumnName = "CurrentStep", ColumnDescription = "当前工位", IsNullable = true)]
    public string CurrentStep { get; set; }
    /// <summary>
    /// 下一工位
    /// </summary>
    [SugarColumn(ColumnName = "NextStep", ColumnDescription = "下一工位", IsNullable = true)]
    public string NextStep { get; set; }
    /// <summary>
    /// Ng工位
    /// </summary>
    [SugarColumn(ColumnName = "NgStep", ColumnDescription = "Ng工位", IsNullable = true)]
    public string NgStep { get; set; }
    /// <summary>
    /// 返修流程ID
    /// </summary>
    [SugarColumn(ColumnName = "RepairFlowId", ColumnDescription = "返修流程ID", IsNullable = true)]
    public long RepairFlowId { get; set; }
    /// <summary>
    /// 返修次数 >0 表示产品在返修状态 数据表示第几次返修
    /// </summary>
    [SugarColumn(ColumnName = "RepairCount", ColumnDescription = "返修次数", IsNullable = true)]
    public int RepairCount { get; set; }
}
