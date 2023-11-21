using Castle.Core.Resource;
using Masuit.Tools;

namespace EasyPlc.Application;

public class AddressService : DbRepository<PlcAddress>, IAddressService
{
    private readonly ILogger<AddressService> _logger;
    private readonly ISimpleCacheService _simpleCacheService;
    private readonly IPlcResourceService _plcResourceService;
    private readonly IPlcConfigService _plcConfigService;

    public AddressService(
        ILogger<AddressService> logger,
        ISimpleCacheService simpleCacheService,
        IPlcResourceService plcResourceService,
        IPlcConfigService plcConfigService
        )
    {
        _logger = logger;
        _simpleCacheService = simpleCacheService;
        _plcResourceService = plcResourceService;
        _plcConfigService = plcConfigService;
    }
    #region 查询
    public override async Task<List<PlcAddress>> GetListAsync()
    {
        //先从Redis拿
        var addresses = _simpleCacheService.Get<List<PlcAddress>>(CacheConst.Cache_PlcAddress);
        if (addresses == null)
        {
            //redis没有就去数据库拿
            addresses = (await base.GetListAsync()).OrderBy(it=>it.SortCode ?? 9999).ToList();
            if (addresses.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(CacheConst.Cache_PlcAddress, addresses);
            }
        }
        return addresses;
    }

    public async Task<PlcAddress> GetAddressById(long id)
    {
        var addrList = await GetListAsync();
        return addrList.Where(it=>it.Id == id).FirstOrDefault();
    }

    public async Task<List<PlcAddress>> GetListByPlcId(long plcId)
    {
        var addrs = await GetListAsync();
        return addrs.Where(it=>it.PlcId == plcId).ToList();
    }

    public async Task<List<PlcResource>> GetResourceListByPlcId(long plcId)
    {
        var plcResourceList = new List<PlcResource>();
        var addrList = await GetListByPlcId(plcId);

        //获取PLC对于开始地址
        var plc = await _plcConfigService.GetPlcConfigById(plcId);
        var aej = plc.ExtJson.ToObject<AddrExtJson>();
        string startAddr = aej.StartAddr;
        var pci = new PlcResourceCopyInput
        {
            StartAddr = startAddr,
            ContainsChild = true,
        };
        foreach ( var addr in addrList)
        {
            //拷贝
            pci.TargetId = addr.Id;
            pci.Ids = new List<long> { addr.ResourceId };

            var getCopyResult = await _plcResourceService.GetCopy(pci);
            plcResourceList.AddRange(getCopyResult);
        }
      
        return plcResourceList;
    }
   

    public async Task RefreshCache()
    {
        _simpleCacheService.Remove(CacheConst.Cache_PlcAddress);//删除KEY
        await GetListAsync();//重新缓存
    }
    #endregion

    #region 新增
    public async Task Add(AddressAddInput input)
    {
        await CheckInput(input);
        //实体转换
        var addr = input.Adapt<PlcAddress>();
        addr.Code = RandomHelper.CreateRandomString(10);//赋值Code
        if(await InsertAsync(addr))
            await RefreshCache();
        //排序
        await Sort();
    }
    #endregion

    #region 编辑
    public Task Edit(AddressEditInput input)
    {
        throw new NotImplementedException();
    }

    public async Task Sort(AddressSortInput input = null)
    {
        var addrList = await GetListAsync();
        //排序时候以10的整数倍，方便后续操作
        if(input == null)
        {
            //整理排序
            for(int i = 0; i < addrList.Count; i ++)
            {
                addrList[i].SortCode = (i + 1) * 10;
            }
            await UpdateRangeAsync(addrList);
            await RefreshCache();
        }
        else
        {
            //排序变更
            foreach(var column in input.Columns)
            {
                var addr = addrList.Where(it => it.Id == column.PlcId).FirstOrDefault();
                if(addr != null)
                {
                    addr.SortCode = column.Sort;
                }
            }
            await UpdateRangeAsync(addrList);
            await RefreshCache();
            await Sort();
        }
        
    }
    #endregion

    #region 删除
    public async Task Delete(List<BaseIdInput> input)
    {
        //获取所有ID
        var ids = input.Select(it => it.Id).ToList();
        var result = await itenant.UseTranAsync(async () =>
        {
            await DeleteByIdsAsync(ids.Cast<object>().ToArray());
        });
        if (result.IsSuccess)//如果成功了
        {
            await RefreshCache();
        }
        else
        {
            //写日志
            _logger.LogError(result.ErrorMessage, result.ErrorException);
            throw Oops.Oh(ErrorCodeEnum.A0002);
        }
    }

    #endregion

    #region 方法

    private async Task CheckInput(PlcAddress address)
    {
        //获取所有数据 
        var addrList = await GetListAsync();
        //根据地址分类 只有事件区是可以对应多个数据结构，其余的只能对应一个
        var plcConfig = await _plcConfigService.GetPlcConfigById(address.PlcId);
        if(plcConfig == null) throw Oops.Bah($"所属PLC不存在:{address.PlcId}");
        
        var categoryList = new List<string>() {
            CateGoryConst.Plc_CUSTOM_R,
            CateGoryConst.Plc_CUSTOM_W,
            CateGoryConst.Plc_GGQ_R,
            CateGoryConst.Plc_GGQ_W,
            CateGoryConst.Plc_SJQ_R,
            CateGoryConst.Plc_SJQ_W
        };
        if (!categoryList.Contains(plcConfig.Category)) throw Oops.Bah($"所属PLC分类不能分配地址:{plcConfig.Category}");

        //不能有相同

        if(plcConfig.Category == CateGoryConst.Plc_SJQ_R || plcConfig.Category == CateGoryConst.Plc_SJQ_W)
        {
            //可以加多个数据对象
        }
        else
        {
            //最多一个
            var listAddr = await GetListByPlcId(address.PlcId);
            if(listAddr.Count >1) throw Oops.Bah($"已有对象:{address.PlcId}");
        }

        //资源
        var resource = await _plcResourceService.GetResurceById(address.ResourceId);
        if(resource == null) throw Oops.Bah($"分配数据资源不存在:{address.ResourceId}");


        address.StartAddr = plcConfig.ExtJson.ToObject<AddrExtJson>().StartAddr;
    }

    

    #endregion
}
