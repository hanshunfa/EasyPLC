using DevExpress.XtraEditors;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class SygoleRFIDWriteForm : DevExpress.XtraEditors.XtraForm
{
    public SygoleRFIDWriteForm()
    {
        InitializeComponent();
    }

    public string CarrierSn { get; set; }
    private void simpleButton1_Click(object sender, EventArgs e)
    {
        var text = textEdit1.Text.Trim();
        if (text.Length != 6)
        {
            XtraMessageBox.Show("长度必须为6");
            return;
        }
        CarrierSn = text;
        DialogResult = DialogResult.OK;
    }

    private void simpleButton2_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}