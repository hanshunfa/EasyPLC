namespace EasyPlc.Application;

/// <summary>
/// 参数分页查询
/// </summary>
public class PointPageInput : BasePageInput
{
    /// <summary>
    /// 载具ID
    /// </summary>
    public long CarrierId { get; set; }

    /// <summary>
    /// 载具ID列表
    /// </summary>
    public List<long> CarrierIds { get; set; }

    /// <summary>
    /// 分类
    /// </summary>
    public string Category { get; set; }
}

/// <summary>
/// 参数新增参数
/// </summary>
public class PointAddInput : MacPoint
{
    /// <summary>
    /// 载具ID
    /// </summary>
    [IdNotNull(ErrorMessage = "ModelId不能为空")]
    public override long CarrierId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "Name不能为空")]
    public override string Name { get; set; }
}

/// <summary>
/// 参数编辑参数
/// </summary>
public class PointEditInput : PointAddInput
{
    /// <summary>
    /// Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}

/// <summary>
/// 参数选择器参数
/// </summary>
public class PointSelectorInput
{
    /// <summary>
    /// 载具ID
    /// </summary>
    public long CarrierId { get; set; }

    /// <summary>
    /// 载具ID列表
    /// </summary>
    public List<long> CarrierIds { get; set; }

    /// <summary>
    /// 关键字
    /// </summary>
    public virtual string SearchKey { get; set; }
}