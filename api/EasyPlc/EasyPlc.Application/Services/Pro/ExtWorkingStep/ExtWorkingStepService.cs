using EasyPlc.Cache;

namespace EasyPlc.Application;

public class ExtWorkingStepService : DbRepository<ExtWorkingStep>, IExtWorkingStepService
{
    private readonly ISimpleCacheService _simpleCacheService;
    public ExtWorkingStepService(
        ISimpleCacheService simpleCacheService
        )
    {
        _simpleCacheService = simpleCacheService;
    }

    public override async Task<List<ExtWorkingStep>> GetListAsync()
    {
        //先从Redis拿
        var extWorkingSteps = _simpleCacheService.Get<List<ExtWorkingStep>>(CacheConst.Cache_ExtWorkingStep);
        if (extWorkingSteps == null)
        {
            //redis没有就去数据库拿
            extWorkingSteps = await base.GetListAsync();
            if (extWorkingSteps.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(CacheConst.Cache_ExtWorkingStep, extWorkingSteps);
            }
        }
        return extWorkingSteps;
    }

    public async Task<ExtWorkingStep> GetByWorkingStepId(long workingStepId)
    {
        var extWsList = await GetListAsync();
        return extWsList.Where(it=>it.WorkingStepId == workingStepId).FirstOrDefault();
    }

    public async Task Add(ExtWorkingStepAddInput input)
    {
        var workingStep = input.Adapt<ExtWorkingStep>();//实体转换
        if (await InsertAsync(workingStep))//插入数据
            await RefreshCache();//刷新缓存
    }

    public async Task<ExtWorkingStep> AddReturnEntityAsync(ExtWorkingStepAddInput input)
    {
        var workingStep = input.Adapt<ExtWorkingStep>();//实体转换
        var entity = await InsertReturnEntityAsync(workingStep);
        await RefreshCache();//刷新缓存
        return entity;
    }

    public async Task Edit(ExtWorkingStepEditInput input)
    {
        var workingStep = input.Adapt<ExtWorkingStep>();//实体转换
        if (await UpdateAsync(workingStep))//插入数据
            await RefreshCache();//刷新缓存
    }

    public async Task Delete(List<BaseIdInput> input)
    {
        //获取所有ID
        var ids = input.Select(it => it.Id).ToList();
        if (ids.Count > 0)
        {
            await DeleteByIdAsync(ids);
            await RefreshCache();
        }
    }
    public async Task DeleteByWorkingStepIdAsync(long id)
    {
        if( await DeleteAsync(it=>it.WorkingStepId==id))
        await RefreshCache();
    }

    public async Task RefreshCache()
    {
        _simpleCacheService.Remove(CacheConst.Cache_ExtWorkingStep);//从redis删除
        await GetListAsync();//刷新缓存
    }
}
