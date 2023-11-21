using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using Mapster;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class FixedScanEditForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IFixedScanService _fixedScanService;
    private readonly FixedScan _fixedScan;

    public FixedScanEditForm(
        IFixedScanService fixedScanService,
        FixedScan fixedScan
        )
    {
        _fixedScanService = fixedScanService;
        _fixedScan = fixedScan;

        InitializeComponent();

    }
    private void FixedScanEditForm_Load(object sender, EventArgs e)
    {
        this.Text = _fixedScan.Id == 0 ? "新增新大陆扫码器" : "编辑新大陆扫码器";

        if (_fixedScan.Id == 0)
        {
            //新增
        }
        else
        {
            //编辑
            textEdit1.Text = _fixedScan.Name;
            textEdit2.Text = _fixedScan.Ip;
            spinEdit1.Value = _fixedScan.Port;
            spinEdit3.Value = _fixedScan.SortCode ?? 99;
        }
    }


    /// <summary>
    /// 确定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void simpleButton1_Click(object sender, EventArgs e)
    {
        _fixedScan.Name = textEdit1.Text.Trim();
        _fixedScan.Ip = textEdit2.Text.Trim();
        _fixedScan.Port = spinEdit1.Value.ToInt();
        _fixedScan.SortCode = spinEdit3.Value.ToInt();

        try
        {
            if (_fixedScan.Id == 0)
            {
                await _fixedScanService.Add(_fixedScan.Adapt<FixedScanAddInput>());
            }
            else
            {
                await _fixedScanService.Edit(_fixedScan.Adapt<FixedScanEditInput>());
            }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        DialogResult = DialogResult.OK;
    }
    /// <summary>
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void simpleButton2_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}