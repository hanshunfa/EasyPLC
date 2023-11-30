﻿/*=============================================================================================
* 
*      *******    *******         **    **
*      **         **              **    **
*      **         **              **    **
*      *******    *******   **    ********
*           **    **              **    **
*           **    **              **    **
*      *******    **              **    **
*
* 创建者：韩顺发
* CLR版本：4.0.30319.42000
* 电子邮箱：shunfa.han@kstopa.com.cn
* 创建时间：2023/11/29 17:25:11
* 版本：v1.0.0
* 描述：
*
* ==============================================================================================
* 修改人：
* 修改时间：
* 修改说明：
* 版本：
* 
===============================================================================================*/



namespace EasyPlc.Core;

public class BaseSortInput
{
    public List<SortColumn> Columns { get; set; }
}

public class SortColumn
{
    [Required(ErrorMessage = "Id不能为空")]
    public long Id { get; set; }
    [Required(ErrorMessage = "Sort不能为空")]
    public int Sort { get; set; }
}