
using Masuit.Tools.Models;
using NewLife.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace EasyPlc.Application;

public class ProOrderService : DbRepository<ProOrder>, IProOrderService
{
    private readonly ISimpleCacheService _simpleCacheService;
    private readonly IMacModelParamService _macModelParamService;
    private readonly IProLaserService _proLaserService;

    public ProOrderService(
        ISimpleCacheService simpleCacheService,
        IMacModelParamService macModelParamService,
        IProLaserService proLaserService
        )
    {
        _simpleCacheService = simpleCacheService;
        _macModelParamService = macModelParamService;
        _proLaserService = proLaserService;
    }
    public override async Task<List<ProOrder>> GetListAsync()
    {
        //先从Redis拿
        var orders = _simpleCacheService.Get<List<ProOrder>>(CacheConst.Cache_ProOrder);
        if (orders == null)
        {
            //redis没有就去数据库拿
            orders = await base.GetListAsync();
            if (orders.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(CacheConst.Cache_ProOrder, orders);
            }
        }
        return orders;
    }
    public async Task<List<ProOrder>> GetListByStatusDes()
    {
        var orderList = await GetListAsync();
        //倒序排序
        orderList = orderList.OrderByDescending(it => it.Id).ToList();
        var statuses = new List<string>() { "READY","RUN", "CLEAR", "STOP" };
        return orderList.Where(it=>statuses.Contains(it.Status)).ToList();
    }
    public async Task<ProOrder> GetWorkingOrder()
    {
        var orderList = await GetListAsync();
        var statuses = new List<string>() { "RUN", "CLEAR"};
        return orderList.Where(it => statuses.Contains(it.Status)).LastOrDefault();
    }
    public async Task<ProOrder> GetWorkingOrderNoSelf(long selfId)
    {
        var orderList = await GetListAsync();
        var statuses = new List<string>() { "RUN", "CLEAR" };
        return orderList.Where(it => statuses.Contains(it.Status) && it.Id != selfId).LastOrDefault();
    }
    public async Task<PagedList<ProOrder>> Page(ProOrderPageInput input)
    {
        var orderList = await GetListAsync();
        //倒序排序
        orderList = orderList.OrderByDescending(it => it.Id).ToList();
        //分页
        var pageInfo = orderList.ToPagedList(input.Current, input.Size);
        return pageInfo;
    }

    public async Task<ProOrder> GetProOrderById(long id)
    {
        var list = await GetListAsync();
        return list.Where(it=>it.Id == id).FirstOrDefault();
    }

    public async Task Add(ProOrderAddInput input, string name = "工单")
    {
        await CheckInput(input, name);
        var order = input.Adapt<ProOrder>();
        if (await InsertAsync(order))//插入数据
            await RefreshCache();//刷新缓存
    }

    public async Task Edit(ProOrderAddInput input, string name = "工单")
    {
        await CheckInput(input, name);
        var order = input.Adapt<ProOrder>();
        if (await UpdateAsync(order))//跟新数据
            await RefreshCache();//刷新缓存
    }
    public async Task EditPutQty(BaseIdInput input, int putQty,int onlineQty)
    {
        if(await UpdateSetColumnsTrueAsync(it => new ProOrder { PutQty = putQty ,OnlineQty=onlineQty}, it => it.Id == input.Id))
        {
            await RefreshCache();//刷新缓存
        }
    }
    public async Task EditQty(BaseIdInput input, int okQty, int onlineQty, int repairQty, int scrapQty)
    {
        if (await UpdateSetColumnsTrueAsync(it => new ProOrder { OkQty = okQty, OnlineQty = onlineQty, RepairQty = repairQty, ScrapQty = scrapQty }, it => it.Id == input.Id))
        {
            await RefreshCache();//刷新缓存
        }
    }
    public async Task SetStatus(ProOrderStatusInput input)
    {
        if(await UpdateSetColumnsTrueAsync(it=> new ProOrder { Status = input.Status } , it=>it.Id == input.Id))
            await RefreshCache();//刷新缓存
    }
    public async Task ReadyOrder(BaseIdInput input)
    {
        await SetStatus(new ProOrderStatusInput { Id = input.Id, Status = OrderStatus.READY });
    }

    public async Task Delete(List<BaseIdInput> input, string name = "工单")
    {
        var ids = input.Select(it => it.Id).ToList();
        if(await DeleteByIdsAsync(ids.Cast<object>().ToArray()))
            await RefreshCache();
    }

    #region 方法
    /// <summary>
    /// 检查输入参数
    /// </summary>
    /// <param name="order"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    private async Task CheckInput(ProOrder order, string name)
    {
        var orders = await GetListAsync();//获取全部
        if (orders.Any(it=> it.Sono == order.Sono && it.Id != order.Id))//判断名称重复的
            throw Oops.Bah($"存在重复名称{name}:{order.Sono}");
    }

    public async Task RefreshCache()
    {
        _simpleCacheService.Remove(CacheConst.Cache_ProOrder);//从redis删除
        await GetListAsync();//刷新缓存
    }
    #endregion
}
