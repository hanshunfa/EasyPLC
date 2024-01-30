namespace EasyPlc.SqlSugar;

/// <summary>
/// 主键实体基类
/// </summary>
public abstract class PrimaryKeyEntity
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [SugarColumn(ColumnDescription = "Id", IsPrimaryKey = true)]
    public virtual long Id { get; set; }

    /// <summary>
    /// 拓展信息
    /// </summary>
    [SugarColumn(ColumnName = "ExtJson", ColumnDescription = "扩展信息", ColumnDataType = StaticConfig.CodeFirst_BigString, IsNullable = true)]
    public virtual string ExtJson { get; set; }
}

/// <summary>
/// 框架实体基类
/// </summary>
public class BaseEntity : PrimaryKeyEntity
{
    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(ColumnDescription = "创建时间", IsOnlyIgnoreUpdate = true, IsNullable = true)]
    public virtual DateTime? CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    [SugarColumn(ColumnDescription = "更新时间", IsOnlyIgnoreInsert = true, IsNullable = true)]
    public virtual DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 创建者Id
    /// </summary>
    [SugarColumn(ColumnDescription = "创建者Id", IsOnlyIgnoreUpdate = true, IsNullable = true)]
    public virtual long? CreateUserId { get; set; }

    /// <summary>
    /// 修改者Id
    /// </summary>
    [SugarColumn(ColumnDescription = "修改者Id", IsOnlyIgnoreInsert = true, IsNullable = true)]
    public virtual long? UpdateUserId { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [SugarColumn(ColumnDescription = "创建人", IsOnlyIgnoreUpdate = true, IsNullable = true)]
    public virtual string CreateUser { get; set; }

    /// <summary>
    /// 更新人
    /// </summary>
    [SugarColumn(ColumnDescription = "更新人", IsOnlyIgnoreInsert = true, IsNullable = true)]
    public virtual string UpdateUser { get; set; }

    /// <summary>
    /// 软删除
    /// </summary>
    [SugarColumn(ColumnDescription = "软删除", IsNullable = true)]
    public virtual bool IsDelete { get; set; } = false;
}

/// <summary>
/// 业务数据实体基类(数据权限)
/// </summary>
public abstract class DataEntityBase : BaseEntity
{
    /// <summary>
    /// 创建者部门Id
    /// </summary>
    [SugarColumn(ColumnDescription = "创建者部门Id")]
    public virtual long CreateOrgId { get; set; }
}
public abstract class RabbitMqaLog : BaseEntity
{
    [SugarColumn(ColumnDescription = "EventId")]
    public virtual long EventId { get; set; }
    [SugarColumn(ColumnDescription = "名称")]
    public virtual string Name { get; set; }
    [SugarColumn(ColumnDescription = "地址")]
    public virtual string Ip { get; set; }
    [SugarColumn(ColumnDescription = "CPU型号")]
    public virtual string Version { get; set; }
    [SugarColumn(ColumnDescription = "读地址时间", IsNullable = true)]
    public virtual DateTime ReadTime { get; set; }
    [SugarColumn(ColumnDescription = "RabbitMq发送时间", IsNullable = true)]
    public virtual DateTime SendTime { get; set; }
    [SugarColumn(ColumnDescription = "接收业务端回复时间", IsNullable = true)]
    public virtual DateTime ReceivedTime { get; set; }
    [SugarColumn(ColumnDescription = "读对象类型", IsIgnore = true)]
    public virtual Type ReadType { get; set; }
    [SugarColumn(ColumnDescription = "Plc信息", ColumnDataType = StaticConfig.CodeFirst_BigString, IsNullable = true)]
    public virtual string ReadBody { get; set; }
    [SugarColumn(ColumnDescription = "写对象类型", IsIgnore = true)]
    public virtual Type WriteType { get; set; }
    [SugarColumn(ColumnDescription = "Eap信息", ColumnDataType = StaticConfig.CodeFirst_BigString, IsNullable = true)]
    public virtual string WriteBody { get; set; }
    [SugarColumn(ColumnDescription = "事件状态")]
    public virtual EventStatus Status { get; set; }
    [SugarColumn(ColumnDescription = "处理结果")]
    public virtual DealResult DealResult { get; set; }
    [SugarColumn(ColumnDescription = "处理提示")]
    public virtual string DealResultMsg { get; set; }
}
public enum EventStatus
{
    Ready = 1,//收到事件，正在处理中
    UnAck,//业务端拒绝处理
    TimeOut,//业务端超时接收
    Repetitive,//PLC重复发送（存在正在处理的事件）
    Finished//处理完成
}
public enum DealResult
{
    Success = 1,//处理成功
    Fail,//处理失败
}
