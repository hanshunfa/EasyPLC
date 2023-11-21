namespace EasyPlc.Application;

/// <summary>
/// <inheritdoc cref="IMacCarrierService"/>
/// </summary>
public class MacCarrierService : DbRepository<MacCarrier>, IMacCarrierService
{
    private readonly ILogger<ILogger> _logger;
    private readonly ISimpleCacheService _simpleCacheService;
    private readonly IMacModelService _macModelService;
    private readonly IMacPointService _macPointService;

    public MacCarrierService(
        ILogger<ILogger> logger,
        ISimpleCacheService simpleCacheService, 
        IMacModelService macModelService, 
        IMacPointService macPointService
        )
    {
        _logger = logger;
        _simpleCacheService = simpleCacheService;
        _macModelService = macModelService;
        _macPointService = macPointService;
    }

    /// <inheritdoc />
    public override async Task<List<MacCarrier>> GetListAsync()
    {
        //先从Redis拿
        var macCarriers = _simpleCacheService.Get<List<MacCarrier>>(CacheConst.Cache_MacCarrier);
        if (macCarriers == null)
        {
            //redis没有就去数据库拿
            macCarriers = await base.GetListAsync();
            if (macCarriers.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(CacheConst.Cache_MacCarrier, macCarriers);
            }
        }
        return macCarriers;
    }

    /// <inheritdoc/>
    public async Task<List<MacCarrier>> CarrierSelector(CarrierSelectorInput input)
    {
        var modelIds = await _macModelService.GetModelChildIds(input.ModelId);//获取下级型号
        var positions = await GetListAsync();
        var result = positions.WhereIF(input.ModelId > 0, it => modelIds.Contains(it.ModelId))//父级
                         .WhereIF(input.ModelIds != null, it => input.ModelIds.Contains(it.ModelId))//在指定型号列表查询
                         .WhereIF(!string.IsNullOrEmpty(input.SearchKey), it => it.Name.Contains(input.SearchKey))//根据关键字查询
                         .ToList();
        return result;
    }

    /// <inheritdoc />
    public async Task<MacCarrier> GetMacCarrierById(long id)
    {
        var macCarriers = await GetListAsync();
        var result = macCarriers.Where(it => it.Id == id).FirstOrDefault();
        return result;
    }

    public async Task<List<MacCarrier>> GetListByModelId(long modelId)
    {
        var macCarriers = await GetListAsync();
        var result = macCarriers.Where(it => it.ModelId == modelId).ToList();
        return result;
    }

    /// <inheritdoc/>
    public async Task<SqlSugarPagedList<MacCarrier>> Page(CarrierPageInput input)
    {
        var modelIds = await _macModelService.GetModelChildIds(input.ModelId);//获取下级型号
        var query = Context.Queryable<MacCarrier>()
                         .WhereIF(input.ModelId > 0, it => modelIds.Contains(it.ModelId))//根据组织ID查询
                         .WhereIF(input.ModelIds != null, it => input.ModelIds.Contains(it.ModelId))//在指定型号列表查询
                         .WhereIF(!string.IsNullOrEmpty(input.Category), it => it.Category == input.Category)//根据分类
                         .WhereIF(!string.IsNullOrEmpty(input.SearchKey), it => it.Name.Contains(input.SearchKey))//根据关键字查询
                         .OrderByIF(!string.IsNullOrEmpty(input.SortField), $"{input.SortField} {input.SortOrder}")
                         .OrderBy(it => it.SortCode);//排序
        var pageInfo = await query.ToPagedListAsync(input.Current, input.Size);//分页
        return pageInfo;
    }

    /// <inheritdoc />
    public async Task Add(CarrierAddInput input, string name = EasyPlcConst.MacCarrier)
    {
        await CheckInput(input, name);//检查参数
        var macCarrier = input.Adapt<MacCarrier>();//实体转换
        //插入载具时候需要动态插入载具位置
        var result = await itenant.UseTranAsync(async () => 
        {
            long id = await InsertReturnSnowflakeIdAsync(macCarrier);//插入载具数据
            for (int i = 0; i < input.NumberOfPosition; i++)
            {
                await _macPointService.Add(new PointAddInput
                {
                    CarrierId = id,
                    Name = input.Name + "-" + (i + 1).ToString().PadLeft(2, '0'),
                    Code = input.Code + "-" + (i + 1).ToString().PadLeft(2, '0'),
                    Point = i + 1,
                    BindStatus = "UNBIND",
                    SortCode = i + 1,
                }) ;
            }
        });
        if(result.IsSuccess)
        {
            await RefreshCache();//刷新缓存
        }
        else
        {
            //写日志
            _logger.LogError(result.ErrorMessage, result.ErrorException);
            throw Oops.Oh(ErrorCodeEnum.A0003);
        }
    }

    /// <inheritdoc />
    public async Task Edit(CarrierEditInput input, string name = EasyPlcConst.MacCarrier)
    {
        await CheckInput(input, name);//检查参数
        var macCarrier = input.Adapt<MacCarrier>();//实体转换
        if (await UpdateAsync(macCarrier))//更新数据
            await RefreshCache();//刷新缓存
    }

    /// <inheritdoc />
    public async Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.MacCarrier)
    {
        //获取所有ID
        var ids = input.Select(it => it.Id).ToList();
        if (ids.Count > 0)
        {
            
            /*
             * 
             */
            //删除参数
            if (await DeleteByIdsAsync(ids.Cast<object>().ToArray()))
                await RefreshCache();//刷新缓存
        }
    }

    /// <inheritdoc />
    public async Task RefreshCache()
    {
        _simpleCacheService.Remove(CacheConst.Cache_MacCarrier);//删除Key
        await GetListAsync();//重新写入缓存
    }

    #region 方法

    /// <summary>
    /// 检查输入参数
    /// </summary>
    /// <param name="macCarrier"></param>
    /// <param name="name">名称</param>
    private async Task CheckInput(MacCarrier macCarrier, string name)
    {
        //所有分类放一个列表
        var positionCategorys = new List<string>() { CateGoryConst.Mac_CARRIER_LINE };
        if (!positionCategorys.Contains(macCarrier.Category))
            throw Oops.Bah($"{name}所属分类错误:{macCarrier.Category}");
        var macCarriers = await GetListAsync();//获取全部
        if (macCarriers.Any(it => it.ModelId == macCarrier.ModelId && it.Name == macCarrier.Name && it.Id != macCarrier.Id))//判断同级是否有名称重复的
            throw Oops.Bah($"存在重复的{name}:{macCarrier.Name}");
        if (macCarrier.Id > 0)//如果ID大于0表示编辑
        {
            var postion = macCarriers.Where(it => it.Id == macCarrier.Id).FirstOrDefault();//获取当前参数
            if (postion != null)
            {
                if (postion.ModelId != macCarrier.ModelId)//如果modelId不一样表示换型号了
                {
                   
                }
            }
            else
                throw Oops.Bah($"{name}不存在");
        }
    }

    #endregion 方法
}