

using Masuit.Tools.Models;

namespace EasyPlc.Application;

public class ProWorkingStepService : DbRepository<ProWorkingStep>, IProWorkingStepService
{
    private readonly ISimpleCacheService _simpleCacheService;
    private readonly IProDataTmpService _proDataTmpService;
    private readonly IMacPointService _macPointService;

    public ProWorkingStepService(
        ISimpleCacheService simpleCacheService,
        IProDataTmpService proDataTmpService,
        IMacPointService macPointService
        )
    {
        _simpleCacheService = simpleCacheService;
        _proDataTmpService = proDataTmpService;
        _macPointService = macPointService;
    }
    public override async Task<List<ProWorkingStep>> GetListAsync()
    {
        //先从Redis拿
        var workingSteps = _simpleCacheService.Get<List<ProWorkingStep>>(CacheConst.Cache_ProWorkingStep);
        if (workingSteps == null)
        {
            //redis没有就去数据库拿
            workingSteps = await base.GetListAsync();
            if (workingSteps.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(CacheConst.Cache_ProWorkingStep, workingSteps);
            }
        }
        return workingSteps;
    }

    public async Task<ProWorkingStep> GetWorkingStepById(long id)
    {
        if(id == 0) return null;
        List<ProWorkingStep> listWorkingStep = await GetListAsync();
        var model=new ProWorkingStep();
        model= listWorkingStep.Where(it => it.Id == id).FirstOrDefault();
        return model;
        return listWorkingStep.Where(it => it.Id == id).FirstOrDefault();
    }

    public async Task<List<ProWorkingStep>> GetListByOrderId(long orderId)
    {
        var listWorkingStep = await GetListAsync();
        return listWorkingStep.Where(it => it.OrderId == orderId).ToList();
    }

    public async Task<PagedList<ProWorkingStep>> PageByOrderId(ProOrderPageInput input,long orderId = 0)
    {
        var listWorkingStep = await GetListAsync();
        listWorkingStep = listWorkingStep.OrderByDescending(it => it.OrderId).WhereIf(orderId != 0,it => it.OrderId == orderId).ToList();
        return listWorkingStep.ToPagedList(input.Current, input.Size);
    }

    public async Task<PagedList<ProWorkingStep>> PageById(ProPageInput input)
    {
        var listWorkingStep = await GetListAsync();
        listWorkingStep = listWorkingStep.Where(it => it.Id == input.Id).ToList();
        return listWorkingStep.ToPagedList(input.Current, input.Size);
    }

    public async Task Add(ProWorkingStepAddInput input)
    {
        var workingStep = input.Adapt<ProWorkingStep>();//实体转换
        if (await InsertAsync(workingStep))//插入数据
            await RefreshCache();//刷新缓存
    }
    public async Task<long> AddReturnId(ProWorkingStepAddInput input)
    {
        var workingStep = input.Adapt<ProWorkingStep>();//实体转换
        var endtity = await InsertReturnEntityAsync(workingStep);//插入数据
        await RefreshCache();//刷新缓存
        return endtity.Id;
    }

    public async Task Edit(ProWorkingStepEditInput input)
    {
        var workingStep = input.Adapt<ProWorkingStep>();//实体转换
        if (await UpdateAsync(workingStep))//插入数据
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


    public async Task DeleteAndDataTmp(List<BaseIdInput> input)
    {

        //获取所有ID
        var ids = input.Select(it => it.Id).ToList();
        if (ids.Count > 0)
        {
            var result = await itenant.UseTranAsync(async () =>
            {
                await DeleteByIdsAsync(ids.Cast<object>().ToArray());
                var dataTmpRep = ChangeRepository<DbRepository<ProDataTmp>>();//切换仓储
                var dataTmpList = await dataTmpRep.GetListAsync(it => ids.Contains(it.WorkingStepId));
                await dataTmpRep.DeleteAsync(it => ids.Contains(it.WorkingStepId));
                var pointRep = ChangeRepository<DbRepository<MacPoint>>();//切换仓储
                //解绑载具
                var dtIdList = dataTmpList.Select(it=>it.Id).ToList();
                await pointRep.UpdateSetColumnsTrueAsync(it => new MacPoint { BindStatus = "UNBIND" }, it => dtIdList.Contains(it.BindCode));
            });
            if (result.IsSuccess)//如果成功了
            {
                await RefreshCache();
                await _proDataTmpService.RefreshCache();
                await _macPointService.RefreshCache();
            }
            else
            {
                //写日志
                //_logger.LogError(result.ErrorMessage, result.ErrorException);
                throw Oops.Oh(ErrorCodeEnum.A0002);
            }
        }
        
    }
    public async Task DeleteByOrderId(long orderId)
    {
        await DeleteAsync(it=>it.OrderId == orderId);
        await RefreshCache();
    }


    public async Task RefreshCache()
    {
        _simpleCacheService.Remove(CacheConst.Cache_ProWorkingStep);//从redis删除
        await GetListAsync();//刷新缓存
    }
}
