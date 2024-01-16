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
    public int Idx { get; set; }
    public string ReadAddr { get; set; }
    public ushort ReadLen { get; set; }
    public string ReadClassName { get; set; }
    public bool IsReset { get; set; } = true;
    public string WriteAddr { get; set; }
    public ushort WriteLen { get; set; }
    public string WriteClassName { get; set; }
    public byte[] WriteBuffer { get; set; }
    public bool TriggerCompleted { get; set; }
    public int SequenceIDR { get; set; } = -1;
    public int SequenceIDW { get; set; } = -1;
    public List<PlcResource> ObjR { get; set; }
    public List<PlcResource> ObjW { get; set; }
    public DateTime ReadTime { get; set; }
    public DateTime SendTime { get; set; }
}
