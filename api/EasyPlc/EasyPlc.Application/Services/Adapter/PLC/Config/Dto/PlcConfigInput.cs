namespace EasyPlc.Application;

/// <summary>
/// 组织分页查询参数
/// </summary>
public class PlcConfigPageInput : BasePageInput
{
    /// <summary>
    /// 父ID
    /// </summary>
    public long ParentId { get; set; }
}

/// <summary>
/// 组织添加参数
/// </summary>
public class PlcConfigAddInput : PlcConfig
{
}

/// <summary>
/// 组织修改参数
/// </summary>
public class PlcConfigEditInput : PlcConfigAddInput
{
    /// <summary>
    /// Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}

/// <summary>
/// 组织复制参数
/// </summary>
public class PlcConfigCopyInput
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
/// 组织导入
/// </summary>
public class PlcConfigImportInput : ImportTemplateInput
{
    /// <summary>
    /// 名称
    ///</summary>
    [ImporterHeader(Name = "名称")]
    [Required(ErrorMessage = "名称不能为空")]
    public string Name { get; set; }

    /// <summary>
    /// 上级组织
    ///</summary>
    [ImporterHeader(Name = "上级组织")]
    [Required(ErrorMessage = "上级组织不能为空")]
    public string Names { get; set; }

    /// <summary>
    /// 分类
    ///</summary>
    [ImporterHeader(Name = "分类")]
    [Required(ErrorMessage = "分类不能为空")]
    public string Category { get; set; }

    /// <summary>
    /// 排序码
    ///</summary>
    [ImporterHeader(Name = "排序码")]
    public int SortCode { get; set; } = 1;

}

/// <summary>
/// 组织树查询参数
/// 懒加载用
/// </summary>
public class PlcConfigTreeInput
{
    /// <summary>
    /// 父Id
    /// </summary>
    public long? ParentId { get; set; }
}