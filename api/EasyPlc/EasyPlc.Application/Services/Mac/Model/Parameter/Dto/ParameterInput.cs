namespace EasyPlc.Application;

/// <summary>
/// 参数分页查询
/// </summary>
public class ParameterPageInput : BasePageInput
{
    /// <summary>
    /// 型号ID
    /// </summary>
    public long ModelId { get; set; }

    /// <summary>
    /// 参数列表
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
public class ParameterAddInput : MacModelParam
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
    /// 分类
    /// </summary>
    [Required(ErrorMessage = "Category不能为空")]
    public override string Category { get; set; }
}

/// <summary>
/// 参数编辑参数
/// </summary>
public class ParameterEditInput : ParameterAddInput
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
public class ParameterSelectorInput
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
/// <summary>
/// 参数拷贝
/// </summary>
public class ParameterCopyInput
{
    /// <summary>
    /// 拷贝对象
    /// </summary>
    public long SelfId { get; set; }
    /// <summary>
    /// 目标对象
    /// </summary>
    public long TargetId { get; set; }
}