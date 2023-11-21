
namespace EasyPlc.Application;

public interface IFixedScanService : ITransient
{
    #region  查询
    /// <summary>
    /// 查询所有RFID
    /// </summary>
    /// <returns></returns>
    Task<List<FixedScan>> GetListAsync();
    /// <summary>
    /// 根据ID查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<FixedScan> GetFixedScanById(int id);
    #endregion

    #region 新增
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task Add(FixedScanAddInput input, string name = EasyPlcConst.FixedScan);
    #endregion

    #region 编辑
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task Edit(FixedScanAddInput input, string name = EasyPlcConst.FixedScan);
    #endregion

    #region 删除
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.FixedScan);
    #endregion
}
