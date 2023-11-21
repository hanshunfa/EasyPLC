﻿namespace EasyPlc.Application;

public class BaseDataPageInput : BasePageInput
{
    /// <summary>
    /// 父ID
    /// </summary>
    [Required(ErrorMessage = "ParentId不能为空")]
    public long? ParentId { get; set; }
}


/// <summary>
/// 添加基础数据参数
/// </summary>
public class BaseDataAddInput : PlcResource
{
    /// <summary>
    /// 父ID
    /// </summary>
    [Required(ErrorMessage = "ParentId不能为空")]
    public override long? ParentId { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    [Required(ErrorMessage = "Title不能为空")]
    public override string Title { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Required(ErrorMessage = "Code不能为空")]
    public override string Code { get; set; }
}

public class BaseDataEditInput : BaseDataAddInput
{
    /// <summary>
    /// ID
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}
