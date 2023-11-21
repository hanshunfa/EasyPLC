
using Masuit.Tools.Models;

namespace EasyPlc.Application;

public interface IProLabelService : ITransient
{
    #region  查询
    /// <summary>
    /// 查询所有标签
    /// </summary>
    /// <returns></returns>
    Task<List<ProLabel>> GetListAsync();
    /// <summary>
    /// 根据ID查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ProLabel> GetProLabelById(int id);
    /// <summary>
    /// 根据工单ID查询
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<ProLabel> GetProLabelByOrderId(long orderId);
    #endregion

    #region 新增
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task Add(ProLabelAddInput input, string name = EasyPlcConst.ProLabel);
    #endregion

    #region 编辑
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task Edit(ProLabelAddInput input, string name = EasyPlcConst.ProLabel);
    #endregion

    #region 删除
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(List<BaseIdInput> input, string name = EasyPlcConst.ProLabel);
    #endregion

    /// <summary>
    /// 刷新
    /// </summary>
    /// <returns></returns>
    Task RefreshCache();
    /// <summary>
    /// 获取当前预览结果
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<ProLabel> GetCurrentPreview(long orderId);
    /// <summary>
    /// 获取当前预览结果并流水+X 默认+1
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    Task<ProLabel> GetCurrentPreviewAndAddX(long orderId, int x = 1);
}
