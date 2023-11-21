namespace EasyPlc.Application;

public class ProDataService : DbRepository<ProData>, IProDataService
{
    public ProDataService(
        )
    {
        
    }

    public async Task Add(ProDataAddInput input)
    {
        var data = input.Adapt<ProData>();//实体转换
        await InsertAsync(data);//插入数据
    }

    public async Task<SqlSugarPagedList<ProData>> PageByOrderId(ProDataPageInput input)
    {
        var query = Context.Queryable<ProData>()
            .WhereIF(input.OrderId > 0, it=>it.OrderId == input.OrderId)
            .WhereIF(!string.IsNullOrEmpty(input.SearchKey), it => it.CableSN.Contains(input.SearchKey))//根据关键字查询
            .OrderByIF(!string.IsNullOrEmpty(input.SortField), $"{input.SortField} {input.SortOrder}");
        var pageInfo = await query.ToPagedListAsync(input.Current, input.Size);//分页
        return pageInfo;
    }
}
