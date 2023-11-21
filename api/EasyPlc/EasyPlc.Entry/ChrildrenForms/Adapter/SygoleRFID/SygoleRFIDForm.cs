
using Castle.Core.Logging;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraEditors;
using Microsoft.Extensions.Logging;
using EasyPlc.Plugin.SygoleRFID;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class SygoleRFIDForm : DevExpress.XtraEditors.XtraForm
{
    private readonly ILogger<SygoleRFIDForm> _logger;
    private readonly ISygoleRfidService _sygoleRfidService;
    private readonly ISygoleFactoryService _sygoleFactoryService;

    public SygoleRFIDForm(
        ILogger<SygoleRFIDForm> logger,
        ISygoleRfidService sygoleRfidService,
        ISygoleFactoryService sygoleFactoryService
        )
    {
        _logger = logger;
        _sygoleRfidService = sygoleRfidService;
        _sygoleFactoryService = sygoleFactoryService;

        InitializeComponent();
    }

    private async void SygoleRFIDForm_Load(object sender, EventArgs e)
    {
        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
        //初始化RFID工厂
        await _sygoleFactoryService.InitFactory();//第二次自动跳过
        RefreshGridControl();
        timer1.Start();
    }

    private List<SygoleRfid> rfidSygoleList = new List<SygoleRfid>();
    /// <summary>
    /// 刷新
    /// </summary>
    /// <returns></returns>
    private void RefreshGridControl()
    {
        var conns = _sygoleFactoryService.GetConnections();
        rfidSygoleList = conns.Select(it => it.RfidSygole).ToList();
        //rfidSygoleList = await _sygoleRfidService.GetListAsync();
        gridControl1.DataSource = rfidSygoleList;
    }

    #region 操作
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var editForm = Native.CreateInstance<SygoleRFIDEditForm>(new SygoleRfid());
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            await _sygoleFactoryService.InitFactory(true);
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
        var rfid = rfidSygoleList.Where(it => it.Ip == m_rfidIp).FirstOrDefault();
        var editForm = Native.CreateInstance<SygoleRFIDEditForm>(rfid);
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            await _sygoleFactoryService.InitFactory(true);
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
        var rfid = rfidSygoleList.Where(it => it.Ip == m_rfidIp).FirstOrDefault();
        if (XtraMessageBox.Show($"确定删除RFID{rfid.Name} {rfid.Ip}?", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            await _sygoleRfidService.Delete(new List<BaseIdInput> { new BaseIdInput { Id = rfid.Id } });

            await _sygoleFactoryService.InitFactory(true);

            RefreshGridControl();
        }

    }
    /// <summary>
    /// 读RFID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var connList = _sygoleFactoryService.GetConnections();
        var conn = connList.Where(it => it.RfidSygole.Ip == m_rfidIp).First();
        string resStr = string.Empty;
        if (!_sygoleFactoryService.ReadRFID(conn, 6, ref resStr))
        {
            XtraMessageBox.Show($"RFID读取失败:{resStr}");
        }
        //调用日志
        memoEdit1.Text = _sygoleFactoryService.GetRFIDLog(conn);
    }
    /// <summary>
    /// 写RFID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var connList = _sygoleFactoryService.GetConnections();
        var conn = connList.Where(it => it.RfidSygole.Ip == m_rfidIp).First();

        string writeStr = "";
        var writeForm = Native.CreateInstance<SygoleRFIDWriteForm>();
        if (writeForm.ShowDialog() == DialogResult.OK)
        {
            writeStr = writeForm.CarrierSn;
            string resStr = string.Empty;
            if (!_sygoleFactoryService.WriteRFID(conn, writeStr, ref resStr))
            {
                XtraMessageBox.Show($"RFID写入失败:{resStr}");
            }
            //调用日志
            memoEdit1.Text = _sygoleFactoryService.GetRFIDLog(conn);
        }
    }
    /// <summary>
    /// 查看日志
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var connList = _sygoleFactoryService.GetConnections();
        var conn = connList.Where(it => it.RfidSygole.Ip == m_rfidIp).First();
        //调用日志
        memoEdit1.Text = _sygoleFactoryService.GetRFIDLog(conn);
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