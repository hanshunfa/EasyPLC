

using DevExpress.XtraEditors;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class ModelCopyForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IMacModelParamService _macModelParamService;
    private readonly List<EditNode> _treeNodes;
    private readonly MacModel _macModel;

    public ModelCopyForm(
        IMacModelParamService macModelParamService,
        List<EditNode> treeNodes,
        MacModel macModel
        )
    {
        _macModelParamService = macModelParamService;
        _treeNodes = treeNodes;
        _macModel = macModel;

        InitializeComponent();
    }

    private void ModelCopyForm_Load(object sender, EventArgs e)
    {

        treeListLookUpEdit1.Properties.DataSource = _treeNodes;
    }
    /// <summary>
    /// 确定按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void simpleButton1_Click(object sender, EventArgs e)
    {
        var treeId = treeListLookUpEdit1.EditValue.ToLong() == 19900522 ? 0 : treeListLookUpEdit1.EditValue.ToLong();

        if (treeId == 0)
        {
            XtraMessageBox.Show("不能选择顶级", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        if (treeId == _macModel.Id)
        {
            XtraMessageBox.Show("不能选择自己", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        try
        {
            //复制
            await _macModelParamService.Copy(_macModel.Id, treeId);
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        DialogResult = DialogResult.OK;
    }
    /// <summary>
    /// 取消按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void simpleButton2_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}