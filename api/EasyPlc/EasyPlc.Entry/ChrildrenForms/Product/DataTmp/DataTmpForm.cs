

using DevExpress.XtraEditors;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class DataTmpForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IProOrderService _proOrderService;
    private readonly IProDataTmpService _proDataTmpService;
    private readonly IProWorkingStepService _proWorkingStepService;
    public DataTmpForm(
        IProOrderService proOrderService,
        IProDataTmpService proDataTmpService,
        IProWorkingStepService proWorkingStepService
        )
    {
        _proOrderService = proOrderService;
        _proDataTmpService = proDataTmpService;
        _proWorkingStepService = proWorkingStepService;

        InitializeComponent();
    }

    private async void DataTmpForm_Load(object sender, EventArgs e)
    {

        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
        await RefreshOrder();
        //分页
        paginationControl1.Method = LoadAsync;
    }
    private async Task LoadAsync(int page = 1)
    {
        if (currentOrder != null)
        {
            await RefreshDataTmpByOrderId(page, paginationControl1.PageSize, currentOrder.Id);
        }
    }
    private List<ProOrder> _proOrderList = new List<ProOrder>();
    private async Task RefreshOrder()
    {
        comboBoxEdit1.Properties.Items.Clear();
        _proOrderList = await _proOrderService.GetListByStatusDes();
        var sonos = _proOrderList.Select(it => it.Sono + "|" + it.Status).ToList();
        comboBoxEdit1.Properties.Items.AddRange(sonos);
    }
    private List<ProDataTmp> proDataTmpList = new List<ProDataTmp>();
    private async Task RefreshDataTmpByOrderId(int currentPage, int pageSize, long orderId = 0)
    {
        try
        {
            var pageList = await _proDataTmpService.PageByOrderId(
            new ProOrderPageInput
            {
                Current = currentPage,
                Size = pageSize
            }, orderId);
            gridControl1.DataSource = proDataTmpList = pageList.Data;
            paginationControl1.SetPage(pageList.CurrentPage, pageList.TotalCount);
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    #region TileView
    private void tileView1_ItemCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs e)
    {
        if (e.Item["ProductStatus"].Text == "ok")
        {
            e.Item["ProductStatus"].Appearance.Normal.BackColor = Color.Green;
        }
        else if (e.Item["ProductStatus"].Text == "repair"|| e.Item["ProductStatus"].Text == "ng")
        {
            e.Item["ProductStatus"].Appearance.Normal.BackColor = Color.Orange;
        }
        else if (e.Item["ProductStatus"].Text == "scrap")
        {
            e.Item["ProductStatus"].Appearance.Normal.BackColor = Color.Red;
        }
        else
        {
            e.Item["ProductStatus"].Appearance.Normal.BackColor = Color.White;
        }
    }
    /// <summary>
    /// 鼠标松开事件  TileView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void tileView1_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None)
        {
            Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
            var hitInfo = tileView1.CalcHitInfo(e.Location);
            if (hitInfo.HitTest == TileControlHitTest.Item)
            {
                popupMenu1.ShowPopup(p);
            }
        }
    }
    private long selectedId = 0;//当前选择项
    /// <summary>
    /// 选择指定项
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void tileView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
    {
        var srs = tileView1.GetSelectedRows();
        if (srs.Length == 1)
        {
            selectedId = tileView1.GetRowCellValue(srs[0], "Id").ToLong();
        }
        else
        {
            selectedId = 0;
        }
    }
    #endregion

    #region 查询

    private ProOrder currentOrder = null;
    /// <summary>
    /// 手动切换工单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectItem = comboBoxEdit1.SelectedItem.ToString();

        var sono = selectItem.Split("|")[0];
        var order = _proOrderList.Where(it => it.Sono == sono).FirstOrDefault();
        currentOrder = order;
        //通过工单查询其生产加工流程
        await RefreshDataTmpByOrderId(1, paginationControl1.PageSize, order.Id);
    }
    /// <summary>
    /// 通过产品SN查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void simpleButton2_Click(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// 获取当前正在加工工单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void simpleButton1_Click(object sender, EventArgs e)
    {
        currentOrder = null;
        var order = await _proOrderService.GetWorkingOrder();
        if (order == null)
        {
            XtraMessageBox.Show("当前没有正在加工的工单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        currentOrder = order;
        //通过工单查询其生产加工流程
        await RefreshDataTmpByOrderId(1, paginationControl1.PageSize, order.Id);
    }
    #endregion



    #region 右键菜单
    /// <summary>
    /// 强制返修
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (selectedId == 0)
        {
            XtraMessageBox.Show("请选择需要操作项", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

    }
    /// <summary>
    /// 强制报废
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (selectedId == 0)
        {
            XtraMessageBox.Show("请选择需要操作项", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
    }
    /// <summary>
    /// 强制删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (selectedId == 0)
        {
            XtraMessageBox.Show("请选择需要操作项", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
    }

    #endregion
}