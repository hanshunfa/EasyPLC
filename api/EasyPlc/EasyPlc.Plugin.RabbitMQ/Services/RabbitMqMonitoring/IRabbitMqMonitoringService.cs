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
* 创建时间：2023/12/11 10:56:01
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


using EasyPlc.Application;
using EasyPlc.SqlSugar;

namespace EasyPlc.Plugin.RabbitMQ;

/// <summary>
/// RabbitMq监控服务
/// </summary>
public interface IRabbitMqMonitoringService : ISingleton
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<SqlSugarPagedList<RabbitMqRealtimeDataLog>> Page(RabbitPageInput input);
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task Add(RabbitMonitoringAddInput input);
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task Edit(RabbitMonitoringEditInput input);

}