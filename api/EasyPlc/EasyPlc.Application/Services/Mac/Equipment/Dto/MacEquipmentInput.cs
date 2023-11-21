namespace EasyPlc.Application;

/// <summary>
/// 设备分页查询参数
/// </summary>
public class MacEquipmentPageInput : BasePageInput
{
    /// <summary>
    /// 父ID
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// 机构列表
    /// </summary>
    public List<long> EquipmentIds { get; set; }
}
/// <summary>
/// 设备添加参数
/// </summary>
public class MacEquipmentAddInput : MacEquipment
{
}

/// <summary>
/// 设备修改参数
/// </summary>
public class MacEquipmentEditInput : MacEquipmentAddInput
{
    /// <summary>
    /// Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}

/// <summary>
/// 设备复制参数
/// </summary>
public class MacEquipmentCopyInput
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
/// 设备树查询参数
/// 懒加载用
/// </summary>
public class MacEquipmentTreeInput
{
    /// <summary>
    /// 父Id
    /// </summary>
    public long? ParentId { get; set; }
}
