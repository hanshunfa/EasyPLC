namespace EasyPlc.Application;

/// <summary>
/// <inheritdoc cref="IMacModelParamService"/>
/// </summary>
public class MacModelParamService : DbRepository<MacModelParam>, IMacModelParamService
{
    private readonly ISimpleCacheService _simpleCacheService;
    private readonly IMacModelService _macModelService;

    public MacModelParamService(ISimpleCacheService simpleCacheService, IMacModelService macModelService)
    {
        _simpleCacheService = simpleCacheService;
        this._macModelService = macModelService;
    }

    /// <inheritdoc />
    public override async Task<List<MacModelParam>> GetListAsync()
    {
        //先从Redis拿
        var macParameters = _simpleCacheService.Get<List<MacModelParam>>(CacheConst.Cache_MacParameter);
        if (macParameters == null)
        {
            //redis没有就去数据库拿
            macParameters = await base.GetListAsync();
            if (macParameters.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(CacheConst.Cache_MacParameter, macParameters);
            }
        }
        return macParameters;
    }

   

    /// <inheritdoc />
    public async Task<MacModelParam> GetMacParameterById(long id)
    {
        var macParameters = await GetListAsync();
        var result = macParameters.Where(it => it.Id == id).FirstOrDefault();
        return result;
    }

    /// <inheritdoc/>
    public async Task<SqlSugarPagedList<MacModelParam>> Page(ParameterPageInput input)
    {
        var orgIds = await _macModelService.GetModelChildIds(input.ModelId);//获取下级型号
        var query = Context.Queryable<MacModelParam>()
                         .WhereIF(input.ModelId > 0, it => orgIds.Contains(it.ModelId))//根据组织ID查询
                         .WhereIF(input.ModelIds != null, it => input.ModelIds.Contains(it.ModelId))//在指定型号列表查询
                         .WhereIF(!string.IsNullOrEmpty(input.Category), it => it.Category == input.Category)//根据分类
                         .WhereIF(!string.IsNullOrEmpty(input.SearchKey), it => it.Name.Contains(input.SearchKey))//根据关键字查询
                         .OrderByIF(!string.IsNullOrEmpty(input.SortField), $"{input.SortField} {input.SortOrder}")
                         .OrderBy(it => it.SortCode);//排序
        var pageInfo = await query.ToPagedListAsync(input.Current, input.Size);//分页
        return pageInfo;
    }
    public async Task<List<MacModelParam>> GetListByModelId(long id)
    {
        var macParameters = await GetListAsync();
        var result = macParameters.Where(it => it.ModelId == id).ToList();
        return result;
    }

    /// <inheritdoc />
    public async Task Add(ParameterAddInput input, string name = EasyPlcConst.MacModelParam)
    {
        await CheckInput(input, name);//检查参数
        var macParameter = input.Adapt<MacModelParam>();//实体转换
        //macParameter.Code = RandomHelper.CreateRandomString(10);//赋值Code
        if (await InsertAsync(macParameter))//插入数据
            await RefreshCache();//刷新缓存
    }

    /// <inheritdoc />
    public async Task Edit(ParameterEditInput input, string name = EasyPlcConst.MacModelParam)
    {
        await CheckInput(input, name);//检查参数
        var macParameter = input.Adapt<MacModelParam>();//实体转换
        if (await UpdateAsync(macParameter))//更新数据
            await RefreshCache();//刷新缓存
    }

    public async Task Copy(ParameterCopyInput input)
    {
        var result = await itenant.UseTranAsync( async () => {
            await DeleteAsync(it => it.ModelId == input.TargetId);
            //获取
            var macParameters = await GetListAsync();
            macParameters = macParameters.Where(it=>it.ModelId == input.SelfId).ToList();
            macParameters.ForEach(it => {
                RedirectPlcResource(it);
                it.ModelId = input.TargetId;
            });
            await InsertRangeAsync(macParameters);
        });
        if (result.IsSuccess)
        {
            await RefreshCache();//刷新缓存
        }
        else
        {
            //写日志
            throw Oops.Bah(result.ErrorMessage);
        }
    }

    /// <inheritdoc />
    public async Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.MacModelParam)
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
        _simpleCacheService.Remove(CacheConst.Cache_MacParameter);//删除Key
        await GetListAsync();//重新写入缓存
    }

    #region 方法

    /// <summary>
    /// 检查输入参数
    /// </summary>
    /// <param name="macParameter"></param>
    /// <param name="name">名称</param>
    private async Task CheckInput(MacModelParam macParameter, string name)
    {
        //所有分类放一个列表
        var positionCategorys = new List<string>() { CateGoryConst.Mac_PARAMETER };
        if (!positionCategorys.Contains(macParameter.Category))
            throw Oops.Bah($"{name}所属分类错误:{macParameter.Category}");
        var macParameters = await GetListAsync();//获取全部
        if (macParameters.Any(it => it.ModelId == macParameter.ModelId && it.Name == macParameter.Name && it.Id != macParameter.Id))//判断同级是否有名称重复的
            throw Oops.Bah($"存在重复的{name}:{macParameter.Name}");
        if (macParameter.Id > 0)//如果ID大于0表示编辑
        {
            var postion = macParameters.Where(it => it.Id == macParameter.Id).FirstOrDefault();//获取当前参数
            if (postion != null)
            {
                if (postion.ModelId != macParameter.ModelId)//如果modelId不一样表示换型号了
                {
                   
                }
            }
            else
                throw Oops.Bah($"{name}不存在");
        }
    }

    /// <summary>
    /// 重新生成参数实体
    /// </summary>
    /// <param orgName="org"></param>
    private void RedirectPlcResource(MacModelParam res, long? newId = null)
    {
        //重新生成ID并赋值
        if (newId == null) newId = CommonUtils.GetSingleId();
        res.Id = newId.Value;
        res.CreateTime = DateTime.Now;
        res.CreateUser = UserManager.UserAccount;
        res.CreateUserId = UserManager.UserId;
    }

    #endregion 方法
}