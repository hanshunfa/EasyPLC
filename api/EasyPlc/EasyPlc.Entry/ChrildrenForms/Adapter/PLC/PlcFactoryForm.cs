
using Castle.Core.Logging;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using Mapster;
using Masuit.Tools;
using Microsoft.Extensions.Logging;
using NewLife.Log;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System.Net.Sockets;
using System.Net;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class PlcFactoryForm : DevExpress.XtraEditors.XtraForm
{
    private readonly ILogger<PlcFactoryForm> _log;
    private readonly ISiemensPlcFactoryService _siemensPlcFactoryService;

    public PlcFactoryForm(
        ILogger<PlcFactoryForm> log,
        ISiemensPlcFactoryService siemensPlcFactoryService
        )
    {
        _log = log;
        _siemensPlcFactoryService = siemensPlcFactoryService;

        InitializeComponent();
    }

    private void PlcFactoryForm_Load(object sender, EventArgs e)
    {
        InitGridView();
        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
        timer1.Start();
    }

    


    public List<PlcInfo> PlcInfoList = new List<PlcInfo>();
    private string InitGridView()
    {
        var initResult = _siemensPlcFactoryService.InitFactory();
        if (initResult != "成功" && initResult != "工厂已存在")
        {
            //表示没有PLC对象
            gridControl1.DataSource = null;
        }
        else
        {
            PlcInfoList = _siemensPlcFactoryService.GetConnectionSiemensPLCList().Select(it => it._plcInfo).ToList();
            gridControl1.DataSource = PlcInfoList;
        }
        return initResult;
    }
    /// <summary>
    /// 刷新
    /// </summary>
    private string RefreshGridView()
    {
        return InitGridView();
    }

    #region 操作
    /// <summary>
    /// 连接全部PLC
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        _siemensPlcFactoryService.StartPLC();
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
    /// <summary>
    /// 连接
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var plcInfo = GetPLC4OP();
        if (plcInfo != null)
        {
            var connPLC = _siemensPlcFactoryService.GetConnectionSiemensPLCList().Where(it => it._plcInfo == plcInfo).FirstOrDefault();
            var result = _siemensPlcFactoryService.StartPLC(connPLC);
            if (result != "OK")
            {
                XtraMessageBox.Show($"连接{result}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
    /// <summary>
    /// 断开
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var plcInfo = GetPLC4OP();
        if (plcInfo != null)
        {
            var connPLC = _siemensPlcFactoryService.GetConnectionSiemensPLCList().Where(it => it._plcInfo == plcInfo).FirstOrDefault();
            var result = _siemensPlcFactoryService.StopPLC(connPLC);
            XtraMessageBox.Show($"断开{result}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    /// <summary>
    /// 监控
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var plcInfo = GetPLC4OP();
        if (plcInfo != null)
        {
            var connPLC = _siemensPlcFactoryService.GetConnectionSiemensPLCList().Where(it => it._plcInfo == plcInfo).FirstOrDefault();
            memoEdit1.Text = JsonUtil.ConvertJsonString(connPLC._plcInfo.ToJsonString());
        }
    }
    string m_seletedPLC = string.Empty;
    private void tileView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
    {
        var srs = tileView1.GetSelectedRows();
        if (srs.Length == 1)
        {
            m_seletedPLC = tileView1.GetRowCellValue(srs[0], "OP").ToString();
        }
        else
        {
            m_seletedPLC = string.Empty;
        }
    }

    #region 方法
    /// <summary>
    /// 通过当前OP名称获取PLC
    /// </summary>
    /// <returns></returns>
    private PlcInfo GetPLC4OP()
    {
        return PlcInfoList.Where(it => it.OP == m_seletedPLC).FirstOrDefault();
    }
    #endregion

    #region 定时器
    /// <summary>
    /// 1s
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void timer1_Tick(object sender, EventArgs e)
    {
        gridControl1.RefreshDataSource();
    }
    #endregion
}