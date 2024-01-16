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
* 创建时间：2023/12/7 14:39:13
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

using EasyRabbitMQ;


namespace EasyPlc.Plugin.RabbitMQ;

/// <summary>
/// 接收业务发送内容及备份+死信交换机消息
/// </summary>
public class OnRabbitReceived : IRabbitReceived
{
    public void OnReceivedReadtimeAlarm(string body)
    {
        //未使用
    }

    public void OnReceivedReadtimeData(string body)
    {
        
    }

    public void OnReceivedReadtimeEvent(string body)
    {
        
    }
    public void OnSendReadtimeData_AE(string body)
    {

    }
    public void OnSendReadtimeAlarm_AE(string body)
    {
        
    }
    public void OnSendReadtimeEvent_AE(string body)
    {

    }
    public void OnSendReadtimeAlarm_DLX(string body)
    {

    }
    public void OnSendReadtimeData_DLX(string body)
    {
        Console.WriteLine($"接收死信交换机实时数据[-]{body}");
    }
    public void OnSendReadtimeEvent_DLX(string body)
    {
        
    }
}
