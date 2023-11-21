using Masuit.Tools.Models;

namespace EasyPlc.Application;

public class ProDataTmpService : DbRepository<ProDataTmp>, IProDataTmpService
{
    private readonly ISimpleCacheService _simpleCacheService;

    public ProDataTmpService(
        ISimpleCacheService simpleCacheService
        )
    {
        _simpleCacheService = simpleCacheService;
    }
    public override async Task<List<ProDataTmp>> GetListAsync()
    {
        //先从Redis拿
        var dataTmp = _simpleCacheService.Get<List<ProDataTmp>>(CacheConst.Cache_ProDataTmp);
        if (dataTmp == null)
        {
            //redis没有就去数据库拿
            dataTmp = await base.GetListAsync();
            if (dataTmp.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(CacheConst.Cache_ProDataTmp, dataTmp);
            }
        }
        return dataTmp;
    }
    public async Task<PagedList<ProDataTmp>> PageByOrderId(ProOrderPageInput input)
    {
        var listDataTmp = await GetListAsync();
        listDataTmp = listDataTmp
            .OrderByDescending(it => it.OrderId)
            .WhereIf(input.OrderId != 0, it => it.OrderId == input.OrderId)
            .ToList();
        return listDataTmp.ToPagedList(input.Current, input.Size);
    }
    public async Task<ProDataTmp> GetDataTmpById(long id)
    {
        if (id == 0) return null;
        var listDataTmp = await GetListAsync();
        return listDataTmp.Where(it=>it.Id  == id).FirstOrDefault();
    }

    public async Task Add(ProDataTmpAddInput input)
    {
        var dataTemp = input.Adapt<ProDataTmp>();//实体转换
        if (await InsertAsync(dataTemp))//插入数据
            await RefreshCache();//刷新缓存
    }

    public async Task<long> AddReturnId(ProDataTmpAddInput input)
    {
        var dataTemp = input.Adapt<ProDataTmp>();//实体转换
        var entity = await InsertReturnEntityAsync(dataTemp);//插入数据
        await RefreshCache();//刷新缓存
        return entity.Id;
    }
    public async Task Edit(ProDataTmpEditInput input)
    {
        var dataTemp = input.Adapt<ProDataTmp>();//实体转换
        if (await UpdateAsync(dataTemp))//插入数据
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

    public async Task RefreshCache()
    {
        _simpleCacheService.Remove(CacheConst.Cache_ProDataTmp);//从redis删除
        await GetListAsync();//刷新缓存
    }


  
}
