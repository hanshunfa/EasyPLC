namespace EasyPlc.Application;

/// <summary>
/// 流程分页查询参数
/// </summary>
public class MacFlowPageInput : BasePageInput
{
    /// <summary>
    /// 父ID
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// 机构列表
    /// </summary>
    public List<long> FlowIds { get; set; }
}
/// <summary>
/// 流程添加参数
/// </summary>
public class MacFlowAddInput : MacFlow
{
}

/// <summary>
/// 流程修改参数
/// </summary>
public class MacFlowEditInput : MacFlowAddInput
{
    /// <summary>
    /// Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}

/// <summary>
/// 流程复制参数
/// </summary>
public class MacFlowCopyInput
{
    /// <summary>
    /// 目标ID
    /// </summary>
    public long TargetId { get; set; }

    /// <summary>
    /// 组织Id列表
    /// </summary>
    [Required(ErrorMessage = "Ids列表不能为空")]
    public List<long> Ids { get; set; }

    /// <summary>
    /// 是否包含下级
    /// </summary>
    public bool ContainsChild { get; set; } = false;
}
/// <summary>
/// 流程树查询参数
/// 懒加载用
/// </summary>
public class MacFlowTreeInput
{
    /// <summary>
    /// 父Id
    /// </summary>
    public long? ParentId { get; set; }
}


/// <summary>
/// 流程授权设备参数
/// </summary>
public class FlowGrantEquipmentInput
{
    /// <summary>
    /// Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 设备信息
    /// </summary>
    [Required(ErrorMessage = "EquipmentIdList不能为空")]
    public List<long> EquipmentIdList { get; set; }
}
