namespace EasyPlc.Application;

/// <summary>
/// <inheritdoc cref="IArrDataService"/>
/// </summary>
public class ArrDataService : DbRepository<PlcResource>, IArrDataService
{
    private readonly ILogger<ArrDataService> _logger;
    private readonly IPlcResourceService _resourceService;
    private readonly IPlcRelationService _relationService;
    private readonly IEventPublisher _eventPublisher;

    public ArrDataService(ILogger<ArrDataService> logger, 
        IPlcResourceService resourceService,
        IPlcRelationService relationService,
        IEventPublisher eventPublisher)
    {
        _logger = logger;
        _resourceService = resourceService;
        _relationService = relationService;
        _eventPublisher = eventPublisher;
    }

    /// <inheritdoc/>
    public async Task<SqlSugarPagedList<PlcResource>> Page(ArrDataPageInput input)
    {
        var query = Context.Queryable<PlcResource>()
            .Where(it => it.ParentId == input.ParentId && it.Category == CateGoryConst.Resource_ArrData)
            .WhereIF(!string.IsNullOrEmpty(input.SearchKey), it => it.Title.Contains(input.SearchKey))//根据关键字查询
            .OrderByIF(!string.IsNullOrEmpty(input.SortField), $"{input.SortField} {input.SortOrder}")
            .OrderBy(it => it.SortCode);//排序
        var pageInfo = await query.ToPagedListAsync(input.Current, input.Size);//分页
        return pageInfo;
    }

    /// <inheritdoc />
    public async Task<long> Add(ArrDataAddInput input)
    {
        await CheckInput(input);//检查参数
        var PlcResource = input.Adapt<PlcResource>();//实体转换
        var entity = await InsertReturnEntityAsync(PlcResource);//插入数据
        await _resourceService.RefreshCache(CateGoryConst.Resource_ArrData);//刷新缓存
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<List<long>> AddBatch(ArrDataAddInput input)
    {
        var PlcResources = new List<PlcResource>();//基础数据列表
        var codeList = new List<string>() { "Add", "Edit", "Delete", "BatchDelete", "Import", "Export", "BatchEdit" };//code后缀
        var titleList = new List<string>() { "新增", "编辑", "删除", "批量删除", "导入", "导出", "批量编辑" };//title前缀
        var idList = new List<long>();//Id列表
        for (var i = 0; i < codeList.Count; i++)
        {
            var id = CommonUtils.GetSingleId();
            PlcResources.Add(new PlcResource
            {
                Id = id,
                Title = titleList[i] + input.Title,//标题等于前缀输入的值
                Code = input.Code + codeList[i],//code等于输入的值加后缀
                ParentId = input.ParentId,
                SortCode = i + 1
            });
            idList.Add(id);
        }
        //遍历列表
        foreach (var PlcResource in PlcResources)
        {
            await CheckInput(PlcResource);//检查基础数据参数
        }
        //添加到数据库
        if (await InsertRangeAsync(PlcResources))//插入数据
        {
            await _resourceService.RefreshCache(CateGoryConst.Resource_ArrData);//刷新缓存
            return PlcResources.Select(it => it.Id).ToList();
        }
        else
        {
            return new List<long>();
        }
    }

    /// <inheritdoc />
    public async Task<long> Edit(ArrDataEditInput input)
    {
        await CheckInput(input);//检查参数
        var PlcResource = input.Adapt<PlcResource>();//实体转换
        //事务
        var result = await itenant.UseTranAsync(async () =>
        {
             await UpdateAsync(PlcResource);//更新基础数据
        });
        if (result.IsSuccess)//如果成功了
        {
            await _resourceService.RefreshCache(CateGoryConst.Resource_ArrData);//资源表基础数据刷新缓存
        }
        else
        {
            //写日志
            _logger.LogError(result.ErrorMessage, result.ErrorException);
            throw Oops.Oh(ErrorCodeEnum.A0002);
        }
        return PlcResource.Id;
    }

    /// <inheritdoc />
    public async Task Delete(List<BaseIdInput> input)
    {
        //获取所有ID
        var ids = input.Select(it => it.Id).ToList();

        //事务
        var result = await itenant.UseTranAsync(async () =>
        {
            var deleteIds = new List<long>();
            for(var i = 0; i < ids.Count; i++)
            {
                deleteIds.AddRange((await _resourceService.GetChildListById(ids[i])).Select(it => it.Id).ToList());
            }
            await base.DeleteByIdAsync(deleteIds.Cast<object>().ToArray());
        });
        if (result.IsSuccess)//如果成功了
        {
            await _resourceService.RefreshCache();//资源表基础数据刷新缓存
        }
        else
        {
            //写日志
            _logger.LogError(result.ErrorMessage, result.ErrorException);
            throw Oops.Oh(ErrorCodeEnum.A0002);
        }
    }
    public async Task DeleteNoSelf(List<BaseIdInput> input)
    {
        //获取所有ID
        var ids = input.Select(it => it.Id).ToList();

        //事务
        var result = await itenant.UseTranAsync(async () =>
        {
            var deleteIds = new List<long>();
            for (var i = 0; i < ids.Count; i++)
            {
                deleteIds.AddRange((await _resourceService.GetChildListById(ids[i], false, true)).Select(it => it.Id).ToList());
            }
            await base.DeleteByIdAsync(deleteIds.Cast<object>().ToArray());
        });
        if (result.IsSuccess)//如果成功了
        {
            await _resourceService.RefreshCache();//资源表基础数据刷新缓存
        }
        else
        {
            //写日志
            _logger.LogError(result.ErrorMessage, result.ErrorException);
            throw Oops.Oh(ErrorCodeEnum.A0002);
        }
    }
    #region 方法

    /// <summary>
    /// 检查输入参数
    /// </summary>
    /// <param name="PlcResource"></param>
    private async Task CheckInput(PlcResource plcResource)
    {
        //获取所有数据
        var dataList = await _resourceService.GetListAsync(new List<string> { CateGoryConst.Resource_BaseData, CateGoryConst.Resource_StructData, CateGoryConst.Resource_ArrData });
        //判断code在继承中是否存在重复

        var brotherList = new List<PlcResource>();
        if (plcResource.Id == 0 && plcResource.ParentId == 0)//顶级新增
        {
            throw Oops.Bah($"数组不能放在顶级，顶级只能是结构对象");
        }
        else
            brotherList = _resourceService.GetBrotherListById(dataList, plcResource.ParentId.Value, false);
        if (brotherList.Any(it => it.Code == plcResource.Code && it.Id != plcResource.Id))
            throw Oops.Bah($"存在重复的同级或者父级编码:{plcResource.Code}");
        //判断父级是否存在
        if (plcResource.ParentId != 0 && !dataList.Any(it => it.Id == plcResource.ParentId))
            throw Oops.Bah($"不存在的父级:{plcResource.ParentId}");
        plcResource.Category = CateGoryConst.Resource_ArrData;//设置分类为基础数据
    }


    #endregion 方法
}