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
* 创建时间：2023/12/11 11:43:43
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


using EasyPlc.Core;
using EasyPlc.SqlSugar;

namespace EasyPlc.Plugin.RabbitMQ;

public class RabbitPageInput : BasePageInput
{
    /// <summary>
    /// 消息类别
    /// </summary>
    public EventStatus Status { get; set; }
}
public class RabbitMonitoringAddInput : RabbitMqInfoInput
{

}
public class RabbitMonitoringEditInput : RabbitMqInfoInput
{
    [Required(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}
