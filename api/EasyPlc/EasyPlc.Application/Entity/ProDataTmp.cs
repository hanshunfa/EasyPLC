namespace EasyPlc.Application;

[SugarTable("pro_data_tmp", TableDescription = "产品质量数据过度表")]
[Tenant(SqlsugarConst.DB_Default)]
public class ProDataTmp : BaseEntity
{
    /// <summary>
    /// 工单ID
    /// </summary>
    [SugarColumn(ColumnName = "OrderId", ColumnDescription = "工单ID")]
    public long OrderId { get; set; }
    /// <summary>
    /// 加工流程ID
    /// </summary>
    [SugarColumn(ColumnName = "WorkingStepId", ColumnDescription = "加工流程ID", IsNullable = true)]
    public long WorkingStepId { get; set; }
    /// <summary>
    /// 线缆SN
    /// </summary>
    [SugarColumn(ColumnName = "CableSN", ColumnDescription = "线缆SN", Length = 100, IsNullable = true)]
    public string CableSN { get; set; }
    /// <summary>
    /// 产品状态 ok repair scrap
    /// </summary>
    [SugarColumn(ColumnName = "ProductStatus", ColumnDescription = "产品状态", Length = 20, IsNullable = true)]
    public string ProductStatus { get; set; }

    //过站数据
    #region OP20



    #endregion
}
