using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using Mapster;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class SygoleRFIDEditForm : DevExpress.XtraEditors.XtraForm
{
    private readonly ISygoleRfidService _sygoleRfidService;
    private readonly SygoleRfid _rfidSygole;

    public SygoleRFIDEditForm(
        ISygoleRfidService sygoleRfidService,
        SygoleRfid rfidSygole
        )
    {
        _sygoleRfidService = sygoleRfidService;
        _rfidSygole = rfidSygole;

        InitializeComponent();

    }
    private void SygoleRFIDEditForm_Load(object sender, EventArgs e)
    {
        this.Text = _rfidSygole.Id == 0 ? "新增思谷RFID" : "编辑思谷RFID";

        if (_rfidSygole.Id == 0)
        {
            //新增
        }
        else
        {
            //编辑
            textEdit1.Text = _rfidSygole.Name;
            textEdit2.Text = _rfidSygole.Ip;
            spinEdit1.Value = _rfidSygole.Port;
            spinEdit2.Value = _rfidSygole.ReaderId;
            spinEdit3.Value = _rfidSygole.SortCode ?? 99;
        }
    }


    /// <summary>
    /// 确定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void simpleButton1_Click(object sender, EventArgs e)
    {
        _rfidSygole.Name = textEdit1.Text.Trim();
        _rfidSygole.Ip = textEdit2.Text.Trim();
        _rfidSygole.Port = spinEdit1.Value.ToInt();
        _rfidSygole.ReaderId = spinEdit2.Value.ToInt();
        _rfidSygole.SortCode = spinEdit3.Value.ToInt();

        try
        {
            if (_rfidSygole.Id == 0)
            {
                await _sygoleRfidService.Add(_rfidSygole.Adapt<SygoleRfidAddInput>());
            }
            else
            {
                await _sygoleRfidService.Edit(_rfidSygole.Adapt<SygoleRfidEditInput>());
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