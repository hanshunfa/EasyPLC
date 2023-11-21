
namespace EasyPlc.Application;

public interface IKwScrewGunService : ITransient
{
    #region  查询
    /// <summary>
    /// 查询所有RFID
    /// </summary>
    /// <returns></returns>
    Task<List<KwScrewGun>> GetListAsync();
    /// <summary>
    /// 根据ID查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<KwScrewGun> GetKwScrewGunById(int id);
    #endregion

    #region 新增
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task Add(KwScrewGunAddInput input, string name = EasyPlcConst.KwScrewGun);
    #endregion

    #region 编辑
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task Edit(KwScrewGunAddInput input, string name = EasyPlcConst.KwScrewGun);
    #endregion

    #region 删除
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.KwScrewGun);
    #endregion
}
