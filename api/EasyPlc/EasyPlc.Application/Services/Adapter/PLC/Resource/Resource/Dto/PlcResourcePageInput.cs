
namespace EasyPlc.Application;

public class PlcResourcePageInput : BasePageInput
{
    /// <summary>
    /// 父ID
    /// </summary>
    [Required(ErrorMessage = "ParentId不能为空")]
    public long? ParentId { get; set; }
}

/// <summary>
/// 资源复制参数
/// </summary>
public class PlcResourceCopyInput
{
    /// <summary>
    /// 目标ID
    /// </summary>
    public long TargetId { get; set; }

    public string StartAddr { get; set; }
    public int len { get; set; } = 0;

    /// <summary>
    /// 资源Id列表
    /// </summary>
    [Required(ErrorMessage = "Ids列表不能为空")]
    public List<long> Ids { get; set; }

    /// <summary>
    /// 是否包含下级
    /// </summary>
    public bool ContainsChild { get; set; } = false;
}
/// <summary>
/// 树查询参数
/// 懒加载用
/// </summary>
public class PlcResourceTreeInput
{
    /// <summary>
    /// 父Id
    /// </summary>
    public long? ParentId { get; set; }
}
/// <summary>
/// 排序参数
/// </summary>
public class PlcResourceSortInput: BaseSortInput
{
    [Required(ErrorMessage = "Pid不能为空")]
    public long Pid { get; set; }
}
