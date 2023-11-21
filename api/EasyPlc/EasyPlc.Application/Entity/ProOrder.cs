namespace EasyPlc.Application;

[SugarTable("pro_order", TableDescription = "工单")]
[Tenant(SqlsugarConst.DB_Default)]
public class ProOrder : BaseEntity
{
    [SugarColumn(ColumnName = "OrderType", ColumnDescription = "工单类型，正常(Normal)返修(Repair)", Length = 50)]
    public string OrderType { get; set; }
    /// <summary>
    /// MES车间订单号
    /// </summary>
    [SugarColumn(ColumnName = "Sono", ColumnDescription = "车间订单号", Length = 200)]
    public string Sono { get; set; }
    /// <summary>
    /// 工艺路线ID
    /// </summary>
    [SugarColumn(ColumnName = "FlowId", ColumnDescription = "工艺路线ID")]
    public long FlowId { get; set; }
    /// <summary>
    /// 工艺路线名称
    /// </summary>
    [SugarColumn(ColumnName = "FlowName", ColumnDescription = "工艺路线名称", Length = 200)]
    public string FlowName { get; set; }
    /// <summary>
    /// 计划数量
    /// </summary>
    [SugarColumn(ColumnName = "PlanQty", ColumnDescription = "计划数量")]
    public int PlanQty { get; set; }
    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "Batch", ColumnDescription = "批次号", Length = 200)]
    public string Batch { get; set; }
    /// <summary>
    /// 投产数量
    /// </summary>
    [SugarColumn(ColumnName = "PutQty", ColumnDescription = "投产数量")]
    public int PutQty { get; set; }
    /// <summary>
    /// 在线加工数量
    /// </summary>
    [SugarColumn(ColumnName = "OnlineQty", ColumnDescription = "在线加工数量")]
    public int OnlineQty { get; set; }
    /// <summary>
    /// 合格品数量
    /// </summary>
    [SugarColumn(ColumnName = "OkQty", ColumnDescription = "合格品数量")]
    public int OkQty { get; set; }
    /// <summary>
    /// 待返修数量
    /// </summary>
    [SugarColumn(ColumnName = "RepairQty", ColumnDescription = "待返修数量")]
    public int RepairQty { get; set; }
    /// <summary>
    /// 报废数量
    /// </summary>
    [SugarColumn(ColumnName = "ScrapQty", ColumnDescription = "报废数量")]
    public int ScrapQty { get; set; }
    /// <summary>
    /// 状态 AWAIT READY RUN CLEAR STOP FINISHED
    /// </summary>
    [SugarColumn(ColumnName = "Status", ColumnDescription = "状态", Length = 200)]
    public string Status { get; set; }
}

public static class OrderStatus
{
    public static string AWAIT = "AWAIT";
    public static string READY = "READY";
    public static string RUN = "RUN";
    public static string CLEAR = "CLEAR";
    public static string STOP = "STOP";
    public static string FINISHED = "FINISHED";
}
