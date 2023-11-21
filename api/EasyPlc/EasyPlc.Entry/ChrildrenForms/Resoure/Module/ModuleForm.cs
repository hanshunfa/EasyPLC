using DevExpress.XtraEditors;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class ModuleForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IModuleService _moduleService;

    public ModuleForm(
        IModuleService moduleService
        )
    {
        _moduleService = moduleService;

        InitializeComponent();
    }

    private async void ModuleForm_Load(object sender, EventArgs e)
    {
        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
        gridView1.SetConfigGridView();
        await RefreshGridView(1, paginationControl1.PageSize);
        //分页
        paginationControl1.Method = OnPage;
    }
    private async Task OnPage(int page = 1)
    {
        await RefreshGridView(page, paginationControl1.PageSize);
    }

    private async Task RefreshGridView(int currentPage, int pageSize)
    {
        try
        {
            var pageList = await _moduleService.Page(new ModulePageInput
            {
                Current = currentPage,
                Size = pageSize
            });
            gridControl1.DataSource = null;
            gridControl1.DataSource = pageList.Records;
            paginationControl1.SetPage(pageList.Current, pageList.Total);
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    #region 菜单
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    /// <summary>
    /// 刷新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    #endregion
}