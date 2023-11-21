namespace EasyPlc.Application;

/// <summary>
/// <inheritdoc cref="IMacPointService"/>
/// </summary>
public class MacPointService : DbRepository<MacPoint>, IMacPointService
{
    private readonly ISimpleCacheService _simpleCacheService;

    public MacPointService(ISimpleCacheService simpleCacheService)
    {
        _simpleCacheService = simpleCacheService;
    }

    /// <inheritdoc />
    public override async Task<List<MacPoint>> GetListAsync()
    {
        //先从Redis拿
        var macPoints = _simpleCacheService.Get<List<MacPoint>>(CacheConst.Cache_MacPoint);
        if (macPoints == null)
        {
            //redis没有就去数据库拿
            macPoints = await base.GetListAsync();
            if (macPoints.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(CacheConst.Cache_MacPoint, macPoints);
            }
        }
        return macPoints;
    }

    /// <inheritdoc/>
    public async Task<List<MacPoint>> PointSelector(PointSelectorInput input)
    {
        var positions = await GetListAsync();
        var result = positions
            .WhereIF(input.CarrierId > 0, it => it.CarrierId == input.CarrierId)//根据组织ID查询
            .WhereIF(input.CarrierIds != null, it => input.CarrierIds.Contains(it.CarrierId))//在指定型号列表查询
            .WhereIF(!string.IsNullOrEmpty(input.SearchKey), it => it.Name.Contains(input.SearchKey))//根据关键字查询
            .ToList();
        return result;
    }

    /// <inheritdoc />
    public async Task<MacPoint> GetMacPointById(long id)
    {
        var macPoints = await GetListAsync();
        var result = macPoints.Where(it => it.Id == id).FirstOrDefault();
        return result;
    }
    public async Task<MacPoint> GetMacPointByCarrierIdAndPoint(long carrierId, int point = 1)
    {
        var macPoints = await GetListAsync();
        var result = macPoints.Where(it => it.CarrierId == carrierId && it.Point == point).FirstOrDefault();
        return result;
    }

    /// <inheritdoc/>
    public async Task<SqlSugarPagedList<MacPoint>> Page(PointPageInput input)
    {
        var query = Context.Queryable<MacPoint>()
                         .WhereIF(input.CarrierId > 0, it => it.CarrierId == input.CarrierId)//根据载具ID查询
                         .WhereIF(input.CarrierIds != null, it => input.CarrierIds.Contains(it.CarrierId))//在指定型号列表查询
                         .WhereIF(!string.IsNullOrEmpty(input.SearchKey), it => it.Name.Contains(input.SearchKey))//根据关键字查询
                         .OrderByIF(!string.IsNullOrEmpty(input.SortField), $"{input.SortField} {input.SortOrder}")
                         .OrderBy(it => it.SortCode);//排序
        var pageInfo = await query.ToPagedListAsync(input.Current, input.Size);//分页
        return pageInfo;
    }

    /// <inheritdoc />
    public async Task Add(PointAddInput input, string name = EasyPlcConst.MacCarrierPosition)
    {
        await CheckInput(input, name);//检查参数
        var macPoint = input.Adapt<MacPoint>();//实体转换
        //macPoint.Code = RandomHelper.CreateRandomString(10);//赋值Code
        if (await InsertAsync(macPoint))//插入数据
            await RefreshCache();//刷新缓存
    }


    /// <inheritdoc />
    public async Task Edit(PointEditInput input, string name = EasyPlcConst.MacCarrierPosition)
    {
        await CheckInput(input, name);//检查参数
        var macPoint = input.Adapt<MacPoint>();//实体转换
        if (await UpdateAsync(macPoint))//更新数据
            await RefreshCache();//刷新缓存
    }

    /// <inheritdoc />
    public async Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.MacCarrierPosition)
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
        _simpleCacheService.Remove(CacheConst.Cache_MacPoint);//删除Key
        await GetListAsync();//重新写入缓存
    }

    #region 方法

    /// <summary>
    /// 检查输入参数
    /// </summary>
    /// <param name="macPoint"></param>
    /// <param name="name">名称</param>
    private async Task CheckInput(MacPoint macPoint, string name)
    {
        //所有分类放一个列表

        var macPoints = await GetListAsync();//获取全部
        if (macPoints.Any(it => it.CarrierId == macPoint.CarrierId && it.Name == macPoint.Name && it.Id != macPoint.Id))//判断同级是否有名称重复的
            throw Oops.Bah($"存在重复的{name}:{macPoint.Name}");
        if (macPoint.Id > 0)//如果ID大于0表示编辑
        {
            var postion = macPoints.Where(it => it.Id == macPoint.Id).FirstOrDefault();//获取当前参数
            if (postion != null)
            {
                if (postion.CarrierId != macPoint.CarrierId)//如果modelId不一样表示换型号了
                {
                   
                }
            }
            else
                throw Oops.Bah($"{name}不存在");
        }
    }

    #endregion 方法
}