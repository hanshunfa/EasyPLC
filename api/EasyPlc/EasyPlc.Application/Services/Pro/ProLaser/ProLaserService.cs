
namespace EasyPlc.Application;

public class ProLaserService : DbRepository<ProLaser>, IProLaserService
{
    private readonly ISimpleCacheService _simpleCacheService;

    public ProLaserService(
        ISimpleCacheService simpleCacheService
        )
    {
        _simpleCacheService = simpleCacheService;
    }
    public override async Task<List<ProLaser>> GetListAsync()
    {
        //先从Redis拿
        var Lasers = _simpleCacheService.Get<List<ProLaser>>(CacheConst.Cache_ProLaser);
        if (Lasers == null)
        {
            //redis没有就去数据库拿
            Lasers = await base.GetListAsync();
            if (Lasers.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(CacheConst.Cache_ProLaser, Lasers);
            }
        }
        return Lasers;
    }

    public async Task<ProLaser> GetProLaserById(int id)
    {
        var list = await GetListAsync();
        return list.Where(it=>it.Id == id).FirstOrDefault();
    }

    public async Task<ProLaser> GetProLaserByOrderId(long orderId)
    {
        var list = await GetListAsync();
        return list.Where(it => it.OrderId == orderId).FirstOrDefault();
    }

    public async Task Add(ProLaserAddInput input, string name = "镭射")
    {
        //await CheckInput(input, name);
        var Laser = input.Adapt<ProLaser>();
        if (await InsertAsync(Laser))//插入数据
            await RefreshCache();//刷新缓存
    }

    public async Task Edit(ProLaserAddInput input, string name = "镭射")
    {
        //await CheckInput(input, name);
        var Laser = input.Adapt<ProLaser>();
        if (await UpdateAsync(Laser))//跟新数据
            await RefreshCache();//刷新缓存
    }

    public async Task Delete(List<BaseIdInput> input, string name = "镭射")
    {
        var ids = input.Select(it => it.Id).ToList();
        if(await DeleteByIdsAsync(ids.Cast<object>().ToArray()))
            await RefreshCache();
    }

    #region 方法
    /// <summary>
    /// 检查输入参数
    /// </summary>
    /// <param name="Laser"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    private async Task CheckInput(ProLaser Laser, string name)
    {
        var Lasers = await GetListAsync();//获取全部
    }

    public async Task RefreshCache()
    {
        _simpleCacheService.Remove(CacheConst.Cache_ProLaser);//从redis删除
        await GetListAsync();//刷新缓存
    }
    #endregion

    #region 其他

    public async Task<ProLaser> GetCurrentPreview(long orderId)
    {
        var laser = await GetProLaserByOrderId(orderId);
        var extJsonObj = laser.ExtJson.ToObject<List<LaserParam>>();
        //转换成预览
        var parseResult = extJsonObj.Select(it => it.Value).ToList().ParseLaserValue("", laser.SerialNum, DateTime.Now, false);
        if(parseResult.IsSucceed)
        {
            if(extJsonObj.Count == parseResult.ResultList.Count)
            {
                for(int i = 0; i < parseResult.ResultList.Count; i++)
                {
                    extJsonObj[i].Value = parseResult.ResultList[i];
                }
                laser.PreviewJson = extJsonObj.ToJson();
            }
        }
        return laser;
    }

    public async Task<ProLaser> GetCurrentPreviewAndAddX(long orderId, int x = 1)
    {
        var laser = await GetProLaserByOrderId(orderId);
        if (laser == null) return null;
        var extJsonObj = laser.ExtJson.ToObject<List<LaserParam>>();
        //转换成预览
        var parseResult = extJsonObj.Select(it => it.Value).ToList().ParseLaserValue("", laser.SerialNum, DateTime.Now, false);
        if (parseResult.IsSucceed)
        {
            if (extJsonObj.Count == parseResult.ResultList.Count)
            {
                for (int i = 0; i < parseResult.ResultList.Count; i++)
                {
                    extJsonObj[i].Value = parseResult.ResultList[i];
                }
                laser.PreviewJson = extJsonObj.ToJson();
            }
        }
        var editInput = laser.Adapt<ProLaserEditInput>();
        editInput.SerialNum += x;
        await Edit(editInput);

        return laser;
    }

    #endregion
}
