using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using Mapster;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class ScrewGunEditForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IKwScrewGunService _kwScrewGunService;
    private readonly KwScrewGun _kwScrewGun;

    public ScrewGunEditForm(
        IKwScrewGunService kwScrewGunService,
        KwScrewGun kwScrewGun
        )
    {
        _kwScrewGunService = kwScrewGunService;
        _kwScrewGun = kwScrewGun;

        InitializeComponent();

    }
    private void ScrewGunEditForm_Load(object sender, EventArgs e)
    {
        this.Text = _kwScrewGun.Id == 0 ? "新增螺丝枪" : "编辑螺丝枪";

        if (_kwScrewGun.Id == 0)
        {
            //新增
        }
        else
        {
            //编辑
            textEdit1.Text = _kwScrewGun.Name;
            textEdit2.Text = _kwScrewGun.Ip;
            spinEdit3.Value = _kwScrewGun.SortCode ?? 99;
        }
    }


    /// <summary>
    /// 确定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void simpleButton1_Click(object sender, EventArgs e)
    {
        _kwScrewGun.Name = textEdit1.Text.Trim();
        _kwScrewGun.Ip = textEdit2.Text.Trim();
        _kwScrewGun.SortCode = spinEdit3.Value.ToInt();

        try
        {
            if (_kwScrewGun.Id == 0)
            {
                await _kwScrewGunService.Add(_kwScrewGun.Adapt<KwScrewGunAddInput>());
            }
            else
            {
                await _kwScrewGunService.Edit(_kwScrewGun.Adapt<KwScrewGunEditInput>());
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