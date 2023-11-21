

using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class DataForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IProOrderService _proOrderService;
    private readonly IProDataService _proDataService;
    private readonly IProWorkingStepService _proWorkingStepService;
    public DataForm(
        IProOrderService proOrderService,
        IProDataService proDataService,
        IProWorkingStepService proWorkingStepService
        )
    {
        _proOrderService = proOrderService;
        _proDataService = proDataService;
        _proWorkingStepService = proWorkingStepService;

        InitializeComponent();
    }

    private async void DataForm_Load(object sender, EventArgs e)
    {
        gridView1.SetConfigGridView();//设置自定义配置
        gridView1.BestFitColumns();//列自适应

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
            await RefreshDataByOrderId(page, paginationControl1.PageSize, currentOrder.Id);
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
    private List<ProData> ProDataList = new List<ProData>();
    private async Task RefreshDataByOrderId(int currentPage, int pageSize, long orderId = 0)
    {
        try
        {
            var pageList = await _proDataService.PageByOrderId(
                new ProDataPageInput
                {
                    Current = currentPage,
                    Size = pageSize
                }, orderId);
            gridControl1.DataSource = ProDataList = pageList.Records.ToList();
            paginationControl1.SetPage(pageList.Current, pageList.Total);
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
        else if (e.Item["ProductStatus"].Text == "repair")
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
        await RefreshDataByOrderId(1, paginationControl1.PageSize, order.Id);
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
        await RefreshDataByOrderId(1, paginationControl1.PageSize, order.Id);
    }
    #endregion

    /// <summary>
    /// 单元列样式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
    {
        //更多可以通过逻辑筛选
        //列名为 gridColumn2的单元列
        if (e.Column.FieldName == "ProductStatus")
        {
            var v = e.CellValue;
            if (v != null)
            {//当单元列名称为  gridColumn2  值大于 standard时，单元列背景颜色变红
                if (v.ToString() == "ok")
                {
                    e.Appearance.BackColor = Color.Green;
                }
                else if (v.ToString() == "repair")
                {
                    e.Appearance.BackColor = Color.Orange;
                }
                else if (v.ToString() == "scrap")
                {
                    e.Appearance.BackColor = Color.Red;
                }
                else
                {
                    e.Appearance.BackColor = Color.Black;
                }
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
        if (e.RowHandle == this.gridView1.FocusedRowHandle)
        {
            //设置字体样式
            //e.Appearance.ForeColor = Color.GreenYellow;
        }
    }

    private void gridView1_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
    {
        string s = string.Empty;
        ColumnView view = sender as ColumnView;
        BindingSource dataSource = view.DataSource as BindingSource;

        if (dataSource == null || dataSource.Count == 0)
        {
            s = "没有数据";
        }
        else
        {
            s = "没有符合条件的数据";
        }
        Font font = new Font("Tahoma", 10, FontStyle.Bold);
        Rectangle r = new Rectangle(e.Bounds.Left + 5, e.Bounds.Top + 5, e.Bounds.Width - 5, e.Bounds.Height - 5);
        e.Graphics.DrawString(s, font, Brushes.Black, r);
    }
}