namespace EasyPlc.Application;

/// <summary>
/// 型号分页查询参数
/// </summary>
public class MacModelPageInput : BasePageInput
{
    /// <summary>
    /// 父ID
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// 机构列表
    /// </summary>
    public List<long> ModelIds { get; set; }
}
/// <summary>
/// 型号添加参数
/// </summary>
public class MacModelAddInput : MacModel
{
}

/// <summary>
/// 型号修改参数
/// </summary>
public class MacModelEditInput : MacModelAddInput
{
    /// <summary>
    /// Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}

/// <summary>
/// 型号复制参数
/// </summary>
public class MacModelCopyInput
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
/// 型号树查询参数
/// 懒加载用
/// </summary>
public class MacModelTreeInput
{
    /// <summary>
    /// 父Id
    /// </summary>
    public long? ParentId { get; set; }
}
