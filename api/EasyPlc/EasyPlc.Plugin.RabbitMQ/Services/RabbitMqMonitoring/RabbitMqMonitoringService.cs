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
using EasyPlc.Cache;
using EasyPlc.SqlSugar;
using EasyPlc.System;

namespace EasyPlc.Plugin.RabbitMQ;

public class RabbitMqMonitoringService : DbRepository<RabbitMqRealtimeDataLog>, IRabbitMqMonitoringService
{
    private readonly ISimpleCacheService _simpleCacheService;

    public RabbitMqMonitoringService(
        ISimpleCacheService simpleCacheService
        )
    {
        _simpleCacheService = simpleCacheService;
    }
    public override async Task<List<RabbitMqRealtimeDataLog>> GetListAsync()
    {
        //先从Redis拿
        var list = _simpleCacheService.Get<List<RabbitMqRealtimeDataLog>>(CacheConst.Cache_RabbitMq);
        if (list == null)
        {
            //redis没有就去数据库拿
            list = await base.GetListAsync();
            if (list.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(CacheConst.Cache_RabbitMq, list);
            }
        }
        return list;
    }
    public async Task<SqlSugarPagedList<RabbitMqRealtimeDataLog>> Page(RabbitPageInput input)
    {
        var query = Context.Queryable<RabbitMqRealtimeDataLog>()
           .Where(it=>it.Status == input.Status)
           .WhereIF(!string.IsNullOrEmpty(input.SearchKey), it => it.Name.Contains(input.SearchKey))//根据关键字查询
           .OrderByIF(!string.IsNullOrEmpty(input.SortField), $"{input.SortField} {input.SortOrder}")
           .OrderBy(it => it.Id);//排序
        var pageInfo = await query.ToPagedListAsync(input.Current, input.Size);//分页
        return pageInfo;
    }
    public async Task Add(RabbitMonitoringAddInput input)
    {
        var monitoring = input.Adapt<RabbitMqRealtimeDataLog>();//实体转换
        if (await InsertAsync(monitoring))//插入数据
            await RefreshCache();//刷新缓存
    }

    public async Task Edit(RabbitMonitoringEditInput input)
    {
        var monitoring = input.Adapt<RabbitMqRealtimeDataLog>();//实体转换
        if (await UpdateAsync(monitoring))//更新数据
            await RefreshCache();//刷新缓存
    }
    #region 其他

    /// <inheritdoc />
    public async Task RefreshCache()
    {
        _simpleCacheService.Remove(CacheConst.Cache_RabbitMq);//从redis删除
        await GetListAsync();//刷新缓存
    }
    #endregion
}
