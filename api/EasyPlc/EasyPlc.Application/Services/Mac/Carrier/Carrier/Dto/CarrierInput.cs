namespace EasyPlc.Application;

/// <summary>
/// 参数分页查询
/// </summary>
public class CarrierPageInput : BasePageInput
{
    /// <summary>
    /// 型号ID
    /// </summary>
    public long ModelId { get; set; }

    /// <summary>
    /// 型号ID列表
    /// </summary>
    public List<long> ModelIds { get; set; }

    /// <summary>
    /// 分类
    /// </summary>
    public string Category { get; set; }
}

/// <summary>
/// 参数新增参数
/// </summary>
public class CarrierAddInput : MacCarrier
{
    /// <summary>
    /// 型号ID
    /// </summary>
    [IdNotNull(ErrorMessage = "ModelId不能为空")]
    public override long ModelId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "Name不能为空")]
    public override string Name { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "Code不能为空")]
    public override string Code { get; set; }
}

/// <summary>
/// 参数编辑参数
/// </summary>
public class CarrierEditInput : CarrierAddInput
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
public class CarrierSelectorInput
{
    /// <summary>
    /// 型号ID
    /// </summary>
    public long ModelId { get; set; }

    /// <summary>
    /// 型号ID列表
    /// </summary>
    public List<long> ModelIds { get; set; }

    /// <summary>
    /// 关键字
    /// </summary>
    public virtual string SearchKey { get; set; }
}