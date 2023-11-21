
namespace EasyPlc.Application;

public class SygoleRfidService : DbRepository<SygoleRfid>, ISygoleRfidService
{
    private readonly ISimpleCacheService _simpleCacheService;

    public SygoleRfidService(
        ISimpleCacheService simpleCacheService
        )
    {
        _simpleCacheService = simpleCacheService;
    }
    public override async Task<List<SygoleRfid>> GetListAsync()
    {
        //先从Redis拿
        var rfids = _simpleCacheService.Get<List<SygoleRfid>>(CacheConst.Cache_SygoleRfid);
        if (rfids == null)
        {
            //redis没有就去数据库拿
            rfids = await base.GetListAsync();
            rfids = rfids.OrderBy(r => r.SortCode ?? 99).ToList();//排序
            if (rfids.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(CacheConst.Cache_SygoleRfid, rfids);
            }
        }
        return rfids;
    }

    public async Task<SygoleRfid> GetSygoleRfidById(int id)
    {
        var list = await GetListAsync();
        return list.Where(it=>it.Id == id).FirstOrDefault();
    }

    public async Task Add(SygoleRfidAddInput input, string name = "思谷RFID")
    {
        await CheckInput(input, name);
        var rfid = input.Adapt<SygoleRfid>();
        rfid.Code = RandomHelper.CreateRandomString(10);//赋值Code
        if (await InsertAsync(rfid))//插入数据
            await RefreshCache();//刷新缓存
    }

    public async Task Edit(SygoleRfidAddInput input, string name = "思谷RFID")
    {
        await CheckInput(input, name);
        var rfid = input.Adapt<SygoleRfid>();
        if (await UpdateAsync(rfid))//跟新数据
            await RefreshCache();//刷新缓存
    }

    public async Task Delete(List<BaseIdInput> input, string name = "思谷RFID")
    {
        var ids = input.Select(it => it.Id).ToList();
        if(await DeleteByIdsAsync(ids.Cast<object>().ToArray()))
            await RefreshCache();
    }

    #region 方法
    /// <summary>
    /// 检查输入参数
    /// </summary>
    /// <param name="rfid"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    private async Task CheckInput(SygoleRfid rfid, string name)
    {
        var rfids = await GetListAsync();//获取全部
        if (rfids.Any(it=> it.Name == rfid.Name && it.Id != rfid.Id))//判断名称重复的
            throw Oops.Bah($"存在重复名称{name}:{rfid.Name}");
    }

    public async Task RefreshCache()
    {
        _simpleCacheService.Remove(CacheConst.Cache_SygoleRfid);//从redis删除
        await GetListAsync();//刷新缓存
    }
    #endregion
}
