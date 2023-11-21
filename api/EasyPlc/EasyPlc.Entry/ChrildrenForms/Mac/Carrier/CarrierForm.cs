using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using Microsoft.EntityFrameworkCore.Infrastructure;
using EasyPlc.System;
using DevExpress.XtraEditors;
using EasyPlc.Entry.ChrildrenForms.Org;
using DevExpress.XtraPrinting;

namespace EasyPlc.Entry.ChrildrenForms.Mac.Carrier;

/// <summary>
/// 载具管理界面
/// </summary>
public partial class CarrierForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IMacModelService _macModelService;
    private readonly IMacCarrierService _macCarrierService;
    private readonly IMacPointService _macPointService;

    public CarrierForm(
        IMacModelService macModelService,
        IMacCarrierService macCarrierService,
        IMacPointService macPointService
        )
    {
        InitializeComponent();

        _macModelService = macModelService;
        _macCarrierService = macCarrierService;
        _macPointService = macPointService;
    }
    private async void CarrierForm_Load(object sender, EventArgs e)
    {
        treeList1.SetConfigTreeList();
        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
        await RefreshTreeList();
        //载具
        gridView1.SetConfigGridView();
        paginationControl1.Method = LoadCarrier;
        //位置
        gridView2.SetConfigGridView();
        paginationControl2.Method = LoadPoint;
    }
    #region 左侧树-型号

   
    private List<MacModel> _macModels = new List<MacModel>();
    /// <summary>
    /// 刷新左边树
    /// </summary>
    private async Task RefreshTreeList()
    {
        _macModels = await _macModelService.GetListAsync();
        treeList1.DataSource = _macModels;
        treeList1.RefreshDataSource();//刷新reeList1
        treeList1.ExpandAll();//展开所有节点
    }
    private MacModel _macModel = null;
    /// <summary>
    /// 左边树发生节点变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
    {
        TreeList tree = sender as TreeList;
        TreeListNode node = tree.FocusedNode;
        if (null != node)
        {
            var id = node.GetDisplayText("Id").ToLong();
            _macModel = _macModels.Where(it => it.Id == id).FirstOrDefault();

            //右上载具刷新
            await RefreshGrid_Carrier(1, paginationControl1.PageSize);
        }
    }

    #endregion

    #region 右上-载具

  
    private async Task LoadCarrier(int page = 1)
    {
        await RefreshGrid_Carrier(page, paginationControl1.PageSize);
    }
    /// <summary>
    /// 显示行号
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
    {
        if (e.Info.IsRowIndicator && e.RowHandle >= 0)
        {
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
    private List<MacCarrier> _macCarriers = new List<MacCarrier>();
    /// <summary>
    /// 刷新载具
    /// </summary>
    /// <param name="currentPage"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    private async Task RefreshGrid_Carrier(int currentPage, int pageSize)
    {
        try
        {
            var pageList = await _macCarrierService.Page(new CarrierPageInput()
            {
                ModelId = _macModel.Id,
                Current = currentPage,
                Size = pageSize
            });
            //自定义变换
            pageList.Records.ForEach(it =>
            {
                it.CarrierStatus = it.CarrierStatus == "ENABLE" ? "启用" : "禁用";
            });
            gridControl1.DataSource = null;
            gridControl1.DataSource = _macCarriers = pageList.Records.ToList();
            paginationControl1.SetPage(pageList.Current, pageList.Total);
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
        }
    }
    private MacCarrier _macCarrier = null;
    /// <summary>
    /// 载具行变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
    {
        var srs = gridView1.GetSelectedRows();
        if (srs.Length > 0)
        {
            //获得选中的行，如果是单选模式，则直接取第一个
            int selectRow = srs[0];
            //从绑定的行数据直接取数据
            _macCarrier = _macCarriers[selectRow];
        }
        else
        {
            _macCarrier = null;
        }
        //跟新该载具的位置信息
        await RefreshGrid_Point(1, paginationControl2.PageSize);
    }
    #endregion

    #region 右下-载具位置

    /// <summary>
    /// 行号
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
    {
        if (e.Info.IsRowIndicator && e.RowHandle >= 0)
        {
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
    private async Task LoadPoint(int page = 1)
    {
        await RefreshGrid_Point(page, paginationControl2.PageSize);
    }
    private List<MacPoint> _macPoints = new List<MacPoint>();
    /// <summary>
    /// 刷新位置
    /// </summary>
    /// <param name="currentPage"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    private async Task RefreshGrid_Point(int currentPage, int pageSize)
    {
        if (_macCarrier != null)
        {
            try
            {
                var pageList = await _macPointService.Page(new PointPageInput()
                {
                    CarrierId = _macCarrier.Id,
                    Current = currentPage,
                    Size = pageSize
                });
                //自定义变换
                pageList.Records.ForEach(it =>
                {
                    it.BindStatus = it.BindStatus == "BIND" ? "绑定" : (it.BindStatus == "UNBIND" ? "解绑" : "未定义");
                });
                gridControl2.DataSource = null;
                gridControl2.DataSource = _macPoints = pageList.Records.ToList();
                paginationControl2.SetPage(1, pageList.Total);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }
        else
        {
            //清空
            gridControl2.DataSource = null;
            paginationControl2.SetPage(1, 0);
        }
    }
    private MacPoint _macPoint = null;
    private void gridView2_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
    {
        var srs = gridView2.GetSelectedRows();
        if (srs.Length > 0)
        {
            //获得选中的行，如果是单选模式，则直接取第一个
            int selectRow = srs[0];
            //从绑定的行数据直接取数据
            _macPoint = _macPoints[selectRow];
        }
        else
        {
            _macPoint = null;
        }
    }

    #endregion

    #region 操作
    /// <summary>
    /// 批量新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var editForm = Native.CreateInstance<CarrierBatchAddForm>(_macModels);
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            //刷新
            await RefreshGrid_Carrier(1, paginationControl1.PageSize);
        }
    }
    /// <summary>
    /// 新增载具
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var editForm = Native.CreateInstance<CarrierEditForm>(_macModels, new MacCarrier());
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            //刷新
            await RefreshGrid_Carrier(1, paginationControl1.PageSize);
        }
    }
    /// <summary>
    /// 编辑载具
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (_macCarrier != null)
        {
            var editForm = Native.CreateInstance<CarrierEditForm>(_macModels, _macCarrier);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                //刷新
                await RefreshGrid_Carrier(1, paginationControl1.PageSize);
            }
        }
        else
        {
            XtraMessageBox.Show("请选择需要编辑行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    /// <summary>
    /// 删除载具
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    /// <summary>
    /// 刷新载具
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    /// <summary>
    /// 新增位置
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    /// <summary>
    /// 编辑位置
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    /// <summary>
    /// 删除位置
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    /// <summary>
    /// 刷新位置
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }

    #endregion

}