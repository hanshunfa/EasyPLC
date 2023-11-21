namespace EasyPlc.Application;

/// <inheritdoc cref="IRelationService"/>
public class PlcRelationservice : DbRepository<PlcRelation>, IPlcRelationService
{
    private readonly ILogger<PlcRelationservice> _logger;
    private readonly ISimpleCacheService _simpleCacheService;
    private readonly IResourceService _resourceService;

    public PlcRelationservice(ILogger<PlcRelationservice> logger, ISimpleCacheService simpleCacheService, IResourceService resourceService)
    {
        this._logger = logger;
        _simpleCacheService = simpleCacheService;
        this._resourceService = resourceService;
    }

    /// <inheritdoc/>
    public async Task<List<PlcRelation>> GetRelationByCategory(string category)
    {
        var key = CacheConst.Cache_PlcRelation + category;
        //先从Redis拿
        var sysRelations = _simpleCacheService.Get<List<PlcRelation>>(key);
        if (sysRelations == null)
        {
            //redis没有就去数据库拿
            sysRelations = await base.GetListAsync(it => it.Category == category);
            if (sysRelations.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(key, sysRelations);
            }
        }
        return sysRelations;
    }

    /// <inheritdoc/>
    public async Task<List<PlcRelation>> GetRelationListByObjectIdAndCategory(long objectId, string category)
    {
        var sysRelations = await GetRelationByCategory(category);
        var result = sysRelations.Where(it => it.ObjectId == objectId).ToList();//获取关系集合
        return result;
    }

    /// <inheritdoc/>
    public async Task<List<PlcRelation>> GetRelationListByObjectIdListAndCategory(List<long> objectIds, string category)
    {
        var sysRelations = await GetRelationByCategory(category);
        var result = sysRelations.Where(it => objectIds.Contains(it.ObjectId)).ToList();//获取关系集合
        return result;
    }

    /// <inheritdoc/>
    public async Task<List<PlcRelation>> GetRelationListByTargetIdAndCategory(string targetId, string category)
    {
        var sysRelations = await GetRelationByCategory(category);
        var result = sysRelations.Where(it => it.TargetId == targetId).ToList();//获取关系集合
        return result;
    }

    /// <inheritdoc/>
    public async Task<List<PlcRelation>> GetRelationListByTargetIdListAndCategory(List<string> targetIds, string category)
    {
        var sysRelations = await GetRelationByCategory(category);
        var result = sysRelations.Where(it => targetIds.Contains(it.TargetId)).ToList();//获取关系集合
        return result;
    }

    /// <inheritdoc/>
    public async Task RefreshCache(string category)
    {
        var key = CacheConst.Cache_PlcRelation + category;//key
        _simpleCacheService.Remove(key);//删除redis
        await GetRelationByCategory(category);//更新缓存
    }

    /// <inheritdoc/>
    public async Task SaveRelationBatch(string category, long objectId, List<string> targetIds, List<string> extJsons, bool clear)
    {
        var sysRelations = new List<PlcRelation>();//要添加的列表
        for (int i = 0; i < targetIds.Count; i++)
        {
            sysRelations.Add(new PlcRelation
            {
                ObjectId = objectId,
                TargetId = targetIds[i],
                Category = category,
                ExtJson = extJsons == null ? null : extJsons[i]
            });
        }
        //事务
        var result = await itenant.UseTranAsync(async () =>
       {
           if (clear)
               await DeleteAsync(it => it.ObjectId == objectId && it.Category == category);//删除老的
           await InsertRangeAsync(sysRelations);//添加新的
       });
        if (result.IsSuccess)//如果成功了
        {
            await RefreshCache(category);
        }
        else
        {
            //写日志
            _logger.LogError(result.ErrorMessage, result.ErrorException);
            throw Oops.Oh(ErrorCodeEnum.A0003);
        }
    }

    /// <inheritdoc/>
    public async Task SaveRelation(string category, long objectId, string targetId, string extJson, bool clear, bool refreshCache = true)
    {
        var sysRelation = new PlcRelation
        {
            ObjectId = objectId,
            TargetId = targetId,
            Category = category,
            ExtJson = extJson
        };
        //事务
       var result = await itenant.UseTranAsync(async () =>
       {
           if (clear)
               await DeleteAsync(it => it.ObjectId == objectId && it.Category == category);//删除老的
           await InsertAsync(sysRelation);//添加新的
       });
        if (result.IsSuccess)//如果成功了
        {
            if (refreshCache)
                await RefreshCache(category);
        }
        else
        {
            //写日志
            _logger.LogError(result.ErrorMessage, result.ErrorException);
            throw Oops.Oh(ErrorCodeEnum.A0003);
        }
    }
}