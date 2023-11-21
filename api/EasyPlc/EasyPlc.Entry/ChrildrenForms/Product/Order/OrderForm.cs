
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraBars.ToastNotifications;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Tile;
using EasyPlc.Entry.ChrildrenForms.Mac.Carrier;
using EasyPlc.System;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class OrderForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IProOrderService _proOrderService;
    private readonly IProProcessService _proProcessService;
    private readonly IMacFlowService _macFlowService;
    private readonly IMacModelParamService _macModelParamService;

    public OrderForm(
        IProOrderService proOrderService,
        IProProcessService proProcessService,
        IMacFlowService macFlowService,
        IMacModelParamService macModelParamService
        )
    {
        _proOrderService = proOrderService;
        _proProcessService = proProcessService;
        _macFlowService = macFlowService;
        _macModelParamService = macModelParamService;


        InitializeComponent();
    }
    private async void OrderForm_Load(object sender, EventArgs e)
    {

        gridView1.SetConfigGridView();
        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
        await RefreshGridView(1, paginationControl1.PageSize);
        //分页
        paginationControl1.Method = LoadOrder;
    }
    private async Task LoadOrder(int page = 1)
    {
        await RefreshGridView(page, paginationControl1.PageSize);
    }

    private List<ProOrder> _proOrderList = new List<ProOrder>();
    private async Task RefreshGridView(int currentPage, int pageSize)
    {
        try
        {
            var pageList = await _proOrderService.Page(new ProOrderPageInput
            {
                Current = currentPage,
                Size = pageSize
            });
            gridControl1.DataSource = null;
            gridControl1.DataSource = _proOrderList = pageList.Data;
            paginationControl1.SetPage(pageList.CurrentPage, pageList.TotalCount);
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    #region GridView
    private ProOrder _proOrder = null;
    /// <summary>
    /// 选项发送变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
    {
        var srs = gridView1.GetSelectedRows();
        if (srs.Length > 0)
        {
            //获得选中的行，如果是单选模式，则直接取第一个
            int selectRow = srs[0];
            //从绑定的行数据直接取数据
            _proOrder = _proOrderList[selectRow];
        }
        else
        {
            _proOrder = null;
        }
    }
    /// <summary>
    /// 鼠标松开
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gridView1_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None)
        {
            Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
            var hitInfo = gridView1.CalcHitInfo(e.Location);
            if (hitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu1.ShowPopup(p);
            }
        }
    }
    /// <summary>
    /// 行样式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
    {
        //更多可以通过逻辑筛选
        //e.RowHandle  等于当前行时变红
        if (false)
        {
            //设置字体样式
            //e.Appearance.ForeColor = Color.GreenYellow;
        }
    }
    /// <summary>
    /// 单元格样式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
    {
        //更多可以通过逻辑筛选
        //列名为 gridColumn2的单元列
        if (e.Column.FieldName == "Status")
        {
            var v = e.CellValue;
            if (v != null)
            {//当单元列名称为  gridColumn2  值大于 standard时，单元列背景颜色变红
                if (v.ToString() == "AWAIT")
                {
                    e.Appearance.BackColor = Color.Gray;
                }
                else if (v.ToString() == "READY")
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
                else if (v.ToString() == "RUN")
                {
                    e.Appearance.BackColor = Color.Green;
                }
                else if (v.ToString() == "CLEAR")
                {
                    e.Appearance.BackColor = Color.Orange;
                }
                else if (v.ToString() == "STOP")
                {
                    e.Appearance.BackColor = Color.Red;
                }
                else if (v.ToString() == "FINISHED")
                {
                    e.Appearance.BackColor = Color.Black;
                }
                else
                {
                    e.Appearance.BackColor = Color.Black;
                }
            }
        }
        if (e.Column.FieldName == "OrderType")
        {
            var v = e.CellValue;
            if (v != null)
            {//当单元列名称为  gridColumn2  值大于 standard时，单元列背景颜色变红
                if (v.ToString() == "Normal")
                {
                    e.Appearance.BackColor = Color.White;
                }
                else if (v.ToString() == "Repair")
                {
                    e.Appearance.BackColor = Color.Red;
                }
                else
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
            }
        }
    }
    #endregion

    #region 菜单
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var editForm = Native.CreateInstance<OrderEditForm>(new ProOrder());
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            //刷新
            await RefreshGridView(1, paginationControl1.PageSize);
        }
    }
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (_proOrder == null)
        {
            XtraMessageBox.Show("请选择需要编辑行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        //只有等待的工单可以编辑
        if (_proOrder.Status != "AWAIT")
        {
            XtraMessageBox.Show("只能编辑等待状态下的工单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var editForm = Native.CreateInstance<OrderEditForm>(_proOrder);
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            //刷新
            await RefreshGridView(1, paginationControl1.PageSize);
        }
    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (_proOrder == null)
        {
            XtraMessageBox.Show("请选择需要删除行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        //只有等待的工单可以删除
        if (_proOrder.Status != "AWAIT")
        {
            XtraMessageBox.Show("只能删除等待状态下的工单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        if (XtraMessageBox.Show($"确定删除【{_proOrder.Sono}】？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
        {
            await _proOrderService.Delete(new List<BaseIdInput> { new BaseIdInput() { Id = _proOrder.Id } });
            //刷新
            await RefreshGridView(1, paginationControl1.PageSize);
        }
    }
    /// <summary>
    /// 刷新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        //刷新
        await RefreshGridView(1, paginationControl1.PageSize);
    }
    #endregion

    #region 右键菜单
    /// <summary>
    /// 右键编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var srs = gridView1.GetSelectedRows();
        if (srs.Length == 1)
        {
            var id = gridView1.GetRowCellValue(srs[0], "Id").ToLong();
            var order = _proOrderList.Where(it => it.Id == id).FirstOrDefault();
            if (order != null)
            {
                //只有等待的工单可以编辑
                if (order.Status != "AWAIT")
                {
                    XtraMessageBox.Show("只能编辑等待状态下的工单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var editForm = Native.CreateInstance<OrderEditForm>(order);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    //刷新
                    await RefreshGridView(1, paginationControl1.PageSize);
                }
            }
        }
    }
    /// <summary>
    /// 右键删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var srs = gridView1.GetSelectedRows();
        if (srs.Length == 1)
        {
            var id = gridView1.GetRowCellValue(srs[0], "Id").ToLong();
            var order = _proOrderList.Where(it => it.Id == id).FirstOrDefault();
            if (order != null)
            {
                //只有等待的工单可以删除
                if (order.Status != "AWAIT")
                {
                    XtraMessageBox.Show("只能删除等待状态下的工单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (XtraMessageBox.Show($"确定删除【{order.Sono}】？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    await _proOrderService.Delete(new List<BaseIdInput> { new BaseIdInput() { Id = order.Id } });
                    //刷新
                    await RefreshGridView(1, paginationControl1.PageSize);
                }
            }
        }
    }
    /// <summary>
    /// 生成Ready工单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var srs = gridView1.GetSelectedRows();
        if (srs.Length == 1)
        {
            var id = gridView1.GetRowCellValue(srs[0], "Id").ToLong();
            var order = _proOrderList.Where(it => it.Id == id).FirstOrDefault();
            if (order != null)
            {
                try
                {
                    //下发工单需要生成对应数据
                    //await _proProcessService.ReadyOrder(new BaseIdInput { Id = order.Id });
                    await _proOrderService.ReadyOrder(new BaseIdInput { Id = order.Id });

                    await RefreshGridView(1, paginationControl1.PageSize);

                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"{ex.Message}", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
    /// <summary>
    /// 右键开始
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var srs = gridView1.GetSelectedRows();
        if (srs.Length == 1)
        {
            var id = gridView1.GetRowCellValue(srs[0], "Id").ToLong();
            var order = _proOrderList.Where(it => it.Id == id).FirstOrDefault();
            if (order != null)
            {
                var workingOrder = await _proOrderService.GetWorkingOrderNoSelf(id);//获取除自己以外的正在加工的工单
                if (workingOrder != null)
                {
                    XtraMessageBox.Show($"有正在执行的工单{workingOrder.Sono}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //只有READY CLEAR STOP状态的工单才能开始
                List<string> listStatus = new List<string>() { "READY", "CLEAR", "STOP" };
                if (!listStatus.Contains(order.Status))
                {
                    XtraMessageBox.Show($"只有READY,CLEAR,STOP状态的工单才能开始", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                try
                {
                    await _proOrderService.SetStatus(new BaseIdInput { Id = order.Id }, "RUN");
                    //
                    OrderInfo.OrderSono = order.Sono;
                    var flow = await _macFlowService.GetMacFlowById(order.FlowId);
                    var listParam = await _macModelParamService.GetListByModelId(flow.ModelId);
                    var command1 = listParam.Where(it => it.Code == "Command01").FirstOrDefault();
                    OrderInfo.Model = Convert.ToInt16(command1.ParamValue);
                    await RefreshGridView(1, paginationControl1.PageSize);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"{ex.Message}", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
    /// <summary>
    /// 右键清料
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var srs = gridView1.GetSelectedRows();
        if (srs.Length == 1)
        {
            var id = gridView1.GetRowCellValue(srs[0], "Id").ToLong();
            var order = _proOrderList.Where(it => it.Id == id).FirstOrDefault();
            if (order != null)
            {
                try
                {
                    //只有正在清料的工单才能人工确认清料完成
                    if (order.Status != "RUN")
                    {
                        XtraMessageBox.Show($"只有正在[生产]工单才能进行[清理]，当前工单[{order.Status}]", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var workingOrder = await _proOrderService.GetWorkingOrderNoSelf(id);//获取除自己以外的正在加工的工单
                    if (workingOrder != null)
                    {
                        XtraMessageBox.Show($"有正在执行的工单{workingOrder.Sono}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    await _proOrderService.SetStatus(new BaseIdInput { Id = order.Id }, "CLEAR");
                    await RefreshGridView(1, paginationControl1.PageSize);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"{ex.Message}", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
    /// <summary>
    /// 人工确认清料完成
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var srs = gridView1.GetSelectedRows();
        if (srs.Length == 1)
        {
            var id = gridView1.GetRowCellValue(srs[0], "Id").ToLong();
            var order = _proOrderList.Where(it => it.Id == id).FirstOrDefault();
            if (order != null)
            {
                try
                {
                    //只有正在清料的工单才能人工确认清料完成
                    if (order.Status != "CLEAR")
                    {
                        XtraMessageBox.Show($"只有正在清料的工单才能人工确认清料完成，当前工单[{order.Status}]", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    //强制清理该工单中的过程状态和数据

                    await _proOrderService.SetStatus(new BaseIdInput { Id = order.Id }, "STOP");
                    await RefreshGridView(1, paginationControl1.PageSize);

                    OrderInfo.OrderSono = "";
                    OrderInfo.Model = 0;
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"{ex.Message}", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
    #endregion
}