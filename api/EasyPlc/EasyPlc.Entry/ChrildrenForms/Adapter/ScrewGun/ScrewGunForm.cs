
using DevExpress.XtraEditors;
using Microsoft.Extensions.Logging;
using EasyPlc.Plugin.ScrewGun;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class ScrewGunForm : DevExpress.XtraEditors.XtraForm
{
    private readonly ILogger _logger;
    private readonly IKwScrewGunService _kwScrewGunService;
    private readonly IKwScrewGunFactoryService _kwScrewGunFactoryService;
    public ScrewGunForm(
        ILogger<ScrewGunForm> logger,
        IKwScrewGunService kwScrewGunService,
        IKwScrewGunFactoryService kwScrewGunFactoryService
        )
    {
        _logger = logger;
        _kwScrewGunService = kwScrewGunService;
        _kwScrewGunFactoryService = kwScrewGunFactoryService;

        InitializeComponent();
    }
    private List<KwScrewGunInfo> KwScrewGunInfoList = new List<KwScrewGunInfo>();
    private async void ScrewGunForm_Load(object sender, EventArgs e)
    {
        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
        await _kwScrewGunFactoryService.StartTcpServer();//启动螺丝枪服务器
        await RefreshGridControl();

        timer1.Start();//启动定时器
    }
    private List<KwScrewGun> kwScrewGunList = new List<KwScrewGun>();
    private async Task RefreshGridControl()
    {
        KwScrewGunInfoList = _kwScrewGunFactoryService.GetKwScrewGuns();
        gridControl1.DataSource = KwScrewGunInfoList;

        kwScrewGunList = await _kwScrewGunService.GetListAsync();
    }

    #region 右键菜单
    /// <summary>
    /// 编辑螺丝枪
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var kw = kwScrewGunList.Where(it => it.Ip == m_screwGunIp).FirstOrDefault();
        if (kw != null)
        {
            var editForm = Native.CreateInstance<ScrewGunEditForm>(kw);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                //刷新
                await RefreshGridControl();
            }
        }
    }

    /// <summary>
    /// 删除螺丝枪
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var kw = kwScrewGunList.Where(it => it.Ip == m_screwGunIp).FirstOrDefault();
        if (kw != null)
        {
            if (XtraMessageBox.Show($"确定删除螺丝枪{kw.Name} {kw.Ip}?", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                await _kwScrewGunService.Delete(new List<BaseIdInput> { new BaseIdInput { Id = kw.Id } });
                await RefreshGridControl();
            }
        }
    }
    /// <summary>
    /// 切换螺丝枪程序号
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    /// <summary>
    /// 查看日志
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var kwInfo = KwScrewGunInfoList.Where(it => it.Ip == m_screwGunIp).FirstOrDefault();
        memoEdit1.Text = kwInfo.Log;
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
        var editForm = Native.CreateInstance<ScrewGunEditForm>(new KwScrewGun());
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            //刷新
            await RefreshGridControl();
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
        await RefreshGridControl();
    }
    #endregion

    #region TiltView操作
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
    private string m_screwGunIp = string.Empty;
    private void tileView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
    {
        var srs = tileView1.GetSelectedRows();
        if (srs.Length == 1)
        {
            m_screwGunIp = tileView1.GetRowCellValue(srs[0], "Ip").ToString();
        }
        else
        {
            m_screwGunIp = string.Empty;
        }
    }
    #endregion

    private void tileView1_ItemCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs e)
    {
        if (e.Item["IsConn"].Text == "True")
        {
            e.Item["IsConn"].ImageOptions.Image = imageCollection1.Images[0];
        }
        if (e.Item["IsConn"].Text == "False")
        {
            e.Item["IsConn"].ImageOptions.Image = imageCollection1.Images[1];
        }
    }

    #region 定时器
    /// <summary>
    /// 500ms定时器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void timer1_Tick(object sender, EventArgs e)
    {
        gridControl1.RefreshDataSource();
    }
    #endregion
}