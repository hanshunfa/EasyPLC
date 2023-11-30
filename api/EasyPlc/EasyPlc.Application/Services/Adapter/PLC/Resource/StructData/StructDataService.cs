using Castle.Core.Resource;

namespace EasyPlc.Application;

/// <summary>
/// <inheritdoc cref="IStructDataService"/>
/// </summary>
public class StructDataService : DbRepository<PlcResource>, IStructDataService
{
    private readonly ILogger<StructDataService> _logger;
    private readonly IPlcResourceService _resourceService;
    private readonly IPlcRelationService _relationService;
    private readonly IEventPublisher _eventPublisher;
    private readonly IAddressService _addressService;

    public StructDataService(ILogger<StructDataService> logger,
        IPlcResourceService resourceService, 
        IPlcRelationService relationService, 
        IAddressService addressService,
        IEventPublisher eventPublisher)
    {
        _logger = logger;
        _resourceService = resourceService;
        _relationService = relationService;
        _eventPublisher = eventPublisher;
        _addressService = addressService;
    }

    /// <inheritdoc/>
    public async Task<SqlSugarPagedList<PlcResource>> Page(StructDataPageInput input)
    {
        var query = Context.Queryable<PlcResource>()
            .Where(it => it.ParentId == input.ParentId && it.Category == CateGoryConst.Resource_StructData)
            .WhereIF(!string.IsNullOrEmpty(input.SearchKey), it => it.Title.Contains(input.SearchKey))//根据关键字查询
            .OrderByIF(!string.IsNullOrEmpty(input.SortField), $"{input.SortField} {input.SortOrder}")
            .OrderBy(it => it.SortCode);//排序
        var pageInfo = await query.ToPagedListAsync(input.Current, input.Size);//分页
        return pageInfo;
    }

    public override Task<List<PlcResource>> GetListAsync()
    {
        //定义结果
        return _resourceService.GetListAsync(new List<string> { CateGoryConst.Resource_StructData });
    }
    /// <inheritdoc />
    public async Task Add(StructDataAddInput input)
    {
        await CheckInput(input, true);//检查参数
        var plcResource = input.Adapt<PlcResource>();//实体转换
        if (await InsertAsync(plcResource))//插入数据
        {
            await _resourceService.RefreshCache(CateGoryConst.Resource_StructData);//刷新缓存
            //排序
            await _resourceService.Sort(new PlcResourceSortInput { Pid = input.ParentId.Value });
        }
    }

    /// <inheritdoc />
    public async Task Edit(StructDataEditInput input)
    {
        await CheckInput(input);//检查参数
        var plcResource = input.Adapt<PlcResource>();//实体转换
        //事务
        var result = await itenant.UseTranAsync(async () =>
        {
            await UpdateAsync(plcResource);//更新基础数据
        });
        if (result.IsSuccess)//如果成功了
        {
            await _resourceService.RefreshCache(CateGoryConst.Resource_StructData);//资源表基础数据刷新缓存
        }
        else
        {
            //写日志
            _logger.LogError(result.ErrorMessage, result.ErrorException);
            throw Oops.Oh(ErrorCodeEnum.A0002);
        }
    }

    /// <inheritdoc />
    public async Task Delete(List<BaseIdInput> input)
    {
        //获取所有ID
        var ids = input.Select(it => it.Id).ToList();

        //事务
        var result = await itenant.UseTranAsync(async () =>
        {
            //获取对应的子集
            var deleteIds = new List<long>();
            for (int i = 0; i < ids.Count; i++)
            {
                deleteIds.AddRange((await _resourceService.GetChildListById(ids[i])).Select(it => it.Id).ToList());
            }
            await base.DeleteByIdAsync(deleteIds.Cast<object>().ToArray());
            //查找关联

           var  plcAddressRep = ChangeRepository<DbRepository<PlcAddress>>();//切换仓储
           await plcAddressRep.DeleteAsync(it=>ids.Contains(it.PlcId));

        });
        if (result.IsSuccess)//如果成功了
        {
            await _resourceService.RefreshCache();//资源表基础数据刷新缓存
            await _addressService.RefreshCache();//地址表刷新数据刷新缓存
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
    private async Task CheckInput(PlcResource plcResource, bool isAdd = false)
    {
        //获取所有数据
        var dataList = await _resourceService.GetListAsync(new List<string> { CateGoryConst.Resource_BaseData, CateGoryConst.Resource_StructData, CateGoryConst.Resource_ArrData });
        //判断code在继承中是否存在重复

        if (!isAdd)
        {
            var brotherList = new List<PlcResource>();
            if (plcResource.Id == 0 && plcResource.ParentId == 0)//顶级新增
            {
                brotherList = dataList.Where(it => it.ParentId == 0).ToList();
            }
            else
                brotherList = _resourceService.GetBrotherListById(dataList, plcResource.ParentId.Value, false);
            if (brotherList.Any(it => it.Code == plcResource.Code && it.Id != plcResource.Id))
                throw Oops.Bah($"存在重复的同级或者父级编码:{plcResource.Code}");
        }
        else
        {
            //不能出现重复的
            if (dataList.Any(it => it.Code == plcResource.Code && it.Id != plcResource.Id))
                throw Oops.Bah($"存在相同的编码:{plcResource.Code}");
        }
        //判断父级是否存在
        if (plcResource.ParentId != 0 && !dataList.Any(it => it.Id == plcResource.ParentId))
            throw Oops.Bah($"不存在的父级:{plcResource.ParentId}");

        plcResource.Category = CateGoryConst.Resource_StructData;//设置分类为基础数据
    }

    #endregion 方法
}