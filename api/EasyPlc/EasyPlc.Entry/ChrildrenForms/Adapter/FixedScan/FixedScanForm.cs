
using Castle.Core.Logging;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraEditors;
using Microsoft.Extensions.Logging;
using EasyPlc.Plugin.Scan;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class FixedScanForm : DevExpress.XtraEditors.XtraForm
{
    private readonly ILogger<FixedScanForm> _logger;
    private readonly IFixedScanService _fixedScanService;
    private readonly IFixedScanFactoryService _fixedScanFactoryService;

    public FixedScanForm(
        ILogger<FixedScanForm> logger,
        IFixedScanService fixedScanService,
        IFixedScanFactoryService fixedScanFactoryService
        )
    {
        _logger = logger;
        _fixedScanService = fixedScanService;
        _fixedScanFactoryService = fixedScanFactoryService;

        InitializeComponent();
    }

    private async void FixedScanForm_Load(object sender, EventArgs e)
    {
        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
        //初始化RFID工厂
        await _fixedScanFactoryService.InitFactory();//第二次自动跳过
        RefreshGridControl();
        timer1.Start();
    }

    private List<FixedScan> fixedScanList = new List<FixedScan>();
    /// <summary>
    /// 刷新
    /// </summary>
    /// <returns></returns>
    private void RefreshGridControl()
    {
        var conns = _fixedScanFactoryService.GetConnections();
        fixedScanList = conns.Select(it => it.FixedScan).ToList();
        gridControl1.DataSource = fixedScanList;
    }

    #region 操作
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var editForm = Native.CreateInstance<FixedScanEditForm>(new FixedScan());
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            await _fixedScanFactoryService.InitFactory(true);
            //刷新
            RefreshGridControl();
        }
    }
    /// <summary>
    /// 刷新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        //刷新
        RefreshGridControl();
    }

    #endregion

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var rfid = fixedScanList.Where(it => it.Ip == m_rfidIp).FirstOrDefault();
        var editForm = Native.CreateInstance<FixedScanEditForm>(rfid);
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            await _fixedScanFactoryService.InitFactory(true);
            //刷新
            RefreshGridControl();
        }
    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var rfid = fixedScanList.Where(it => it.Ip == m_rfidIp).FirstOrDefault();
        if (XtraMessageBox.Show($"确定删除RFID{rfid.Name} {rfid.Ip}?", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            await _fixedScanService.Delete(new List<BaseIdInput> { new BaseIdInput { Id = rfid.Id } });

            await _fixedScanFactoryService.InitFactory(true);

            RefreshGridControl();
        }

    }
    /// <summary>
    /// 读扫码器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var connList = _fixedScanFactoryService.GetConnections();
        var conn = connList.Where(it => it.FixedScan.Ip == m_rfidIp).First();
        string resStr = string.Empty;
        if (!_fixedScanFactoryService.ReadScan(conn, ref resStr))
        {
            XtraMessageBox.Show($"RFID读取失败:{resStr}");
        }
        //调用日志
        memoEdit1.Text = _fixedScanFactoryService.GetScanLog(conn);
    }

    /// <summary>
    /// 查看日志
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var connList = _fixedScanFactoryService.GetConnections();
        var conn = connList.Where(it => it.FixedScan.Ip == m_rfidIp).First();
        //调用日志
        memoEdit1.Text = _fixedScanFactoryService.GetScanLog(conn);
    }

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
    private string m_rfidIp = string.Empty;
    private void tileView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
    {
        var srs = tileView1.GetSelectedRows();
        if (srs.Length == 1)
        {
            m_rfidIp = tileView1.GetRowCellValue(srs[0], "Ip").ToString();
        }
        else
        {
            m_rfidIp = string.Empty;
        }
    }

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

    private void timer1_Tick(object sender, EventArgs e)
    {
        gridControl1.RefreshDataSource();
    }
}