/*=============================================================================================
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
* 创建时间：2023/11/17 15:01:18
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

namespace EasyPlc.Plugin.Plc;

/// <summary>
/// 事件信息
/// </summary>
public class EventInfo
{
    /// <summary>
    /// 事件索引
    /// </summary>
    public int Idx { get; set; }

    /// <summary>
    /// 事件完成标志
    /// </summary>
    public bool TriggerCompleted { get; set; }

    /// <summary>
    /// 事件读信息类
    /// </summary>
    public EventReadInfo ReadInfo { get; set; } = new();

    /// <summary>
    /// 事件写信息类
    /// </summary>
    public EventWriterInfo WriteInfo { get; set; } = new();
}