namespace EasyPlc.Application;

public class ProLabelService : DbRepository<ProLabel>, IProLabelService
{
    private readonly ISimpleCacheService _simpleCacheService;

    public ProLabelService(
        ISimpleCacheService simpleCacheService
        )
    {
        _simpleCacheService = simpleCacheService;
    }
    public override async Task<List<ProLabel>> GetListAsync()
    {
        //先从Redis拿
        var Labels = _simpleCacheService.Get<List<ProLabel>>(CacheConst.Cache_ProLabel);
        if (Labels == null)
        {
            //redis没有就去数据库拿
            Labels = await base.GetListAsync();
            if (Labels.Count > 0)
            {
                //插入Redis
                _simpleCacheService.Set(CacheConst.Cache_ProLabel, Labels);
            }
        }
        return Labels;
    }

    public async Task<ProLabel> GetProLabelById(int id)
    {
        var list = await GetListAsync();
        return list.Where(it => it.Id == id).FirstOrDefault();
    }

    public async Task<ProLabel> GetProLabelByOrderId(long orderId)
    {
        var list = await GetListAsync();
        return list.Where(it => it.OrderId == orderId).FirstOrDefault();
    }

    public async Task Add(ProLabelAddInput input, string name = "标签")
    {
        //await CheckInput(input, name);
        var Label = input.Adapt<ProLabel>();
        if (await InsertAsync(Label))//插入数据
            await RefreshCache();//刷新缓存
    }

    public async Task Edit(ProLabelAddInput input, string name = "标签")
    {
        //await CheckInput(input, name);
        var Label = input.Adapt<ProLabel>();
        if (await UpdateAsync(Label))//跟新数据
            await RefreshCache();//刷新缓存
    }

    public async Task Delete(List<BaseIdInput> input, string name = "标签")
    {
        var ids = input.Select(it => it.Id).ToList();
        if (await DeleteByIdsAsync(ids.Cast<object>().ToArray()))
            await RefreshCache();
    }

    #region 方法
    /// <summary>
    /// 检查输入参数
    /// </summary>
    /// <param name="Label"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    private async Task CheckInput(ProLabel Label, string name)
    {
        var Labels = await GetListAsync();//获取全部
    }

    public async Task RefreshCache()
    {
        _simpleCacheService.Remove(CacheConst.Cache_ProLabel);//从redis删除
        await GetListAsync();//刷新缓存
    }
    #endregion

    #region 其他

    public async Task<ProLabel> GetCurrentPreview(long orderId)
    {
        var label = await GetProLabelByOrderId(orderId);
        if (label == null) return null;
        var extJsonObj = label.ExtJson.ToObject<List<LabelParam>>();
        //转换成预览
        var parseResult = extJsonObj.Select(it => it.Value).ToList().ParseLabelValue("", label.SerialNum, DateTime.Now, false);
        if (parseResult.IsSucceed)
        {
            if (extJsonObj.Count == parseResult.ResultList.Count)
            {
                for (int i = 0; i < parseResult.ResultList.Count; i++)
                {
                    extJsonObj[i].Value = parseResult.ResultList[i];
                }
                label.PreviewJson = extJsonObj.ToJson();
            }
        }
        return label;
    }
    public async Task<ProLabel> GetCurrentPreviewAndAddX(long orderId, int x = 1)
    {
        var label = await GetProLabelByOrderId(orderId);
        if (label == null) return null;
        var extJsonObj = label.ExtJson.ToObject<List<LabelParam>>();
        //转换成预览
        var parseResult = extJsonObj.Select(it => it.Value).ToList().ParseLabelValue("", label.SerialNum, DateTime.Now, false);
        if (parseResult.IsSucceed)
        {
            if (extJsonObj.Count == parseResult.ResultList.Count)
            {
                for (int i = 0; i < parseResult.ResultList.Count; i++)
                {
                    extJsonObj[i].Value = parseResult.ResultList[i];
                }
                label.PreviewJson = extJsonObj.ToJson();
            }
        }
        var editInput = label.Adapt<ProLabelEditInput>();
        editInput.SerialNum += x;
        await Edit(editInput);

        return label;
    }

    #endregion 
}
