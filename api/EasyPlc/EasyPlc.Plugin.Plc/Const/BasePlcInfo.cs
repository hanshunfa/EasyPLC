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
* 创建时间：2023/11/17 14:56:50
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

public class BasePlcInfo
{
    public virtual string Name { get; set; }
    public virtual string IP { get; set; }
    public virtual int Port { get; set; }
    public virtual bool IsConn { get; set; }
    public virtual PublicInfo PI { get; set; }
    public virtual List<EventInfo> EIs { get; set; }
}
