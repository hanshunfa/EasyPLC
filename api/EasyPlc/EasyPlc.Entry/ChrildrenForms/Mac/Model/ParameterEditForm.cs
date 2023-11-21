using DevExpress.XtraEditors;
using Mapster;
using EasyPlc.System;
using static EasyPlc.Entry.ChrildrenForms.Mac.ModelForm;

namespace EasyPlc.Entry.ChrildrenForms.Mac;

public partial class ParameterEditForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IMacModelParamService _parameterService;
    private readonly List<EditNode> _treeNodes;
    private readonly MacModelParam _macParameter;

    public ParameterEditForm(
        IMacModelParamService parameterService,
        List<EditNode> treeNodes,
        MacModelParam macParameter
        )
    {
        InitializeComponent();

        _parameterService = parameterService;
        _treeNodes = treeNodes;
        _macParameter = macParameter;

        //修改标题
        Text = $"参数管理" + "-" + (_macParameter.Id == 0 ? "新增" : "编辑");

        InitEditForm();
        InitData();
    }
    private void InitEditForm()
    {
        treeListLookUpEdit1.Properties.DataSource = _treeNodes;
    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    private void InitData()
    {
        if (_macParameter.Id == 0)
        {
            //新增
        }
        else
        {
            //编辑
            treeListLookUpEdit1.EditValue = _macParameter.ModelId;
            textEdit1.Text = _macParameter.Name;
            textEdit3.Text = _macParameter.Code;
            comboBoxEdit1.Text = _macParameter.Category;
            comboBoxEdit2.Text = _macParameter.ParamType;
            textEdit2.Text = _macParameter.ParamValue;
            textEdit4.Text = _macParameter.ParamUnit;
            trackBarControl1.Value = _macParameter.SortCode ?? 99;
        }
    }

    private void ParameterEditForm_Load(object sender, EventArgs e)
    {

    }

    private async void btnOk_Click(object sender, EventArgs e)
    {
        _macParameter.ModelId = treeListLookUpEdit1.EditValue.ToLong();
        _macParameter.Name = textEdit1.Text.Trim();
        _macParameter.Code = textEdit3.Text.Trim();
        _macParameter.Category = comboBoxEdit1.Text == "参数" ? "PARAMETER" : "NULL";
        string pt = "";
        switch (comboBoxEdit2.Text.Trim())
        {
            case "string":
                pt = "PARM_STRING";
                break;
            case "int16":
                pt = "PARM_INT16";
                break;
            case "int32":
                pt = "PARM_INT32";
                break;
            case "float":
                pt = "PARM_FLOAT";
                break;
            default:
                pt = "UNLL";
                break;
        }
        _macParameter.ParamType = pt;
        _macParameter.ParamValue = textEdit2.Text.Trim();
        _macParameter.ParamUnit = textEdit4.Text.Trim();
        _macParameter.SortCode = trackBarControl1.Value;

        try
        {
            if (_macParameter.Id == 0)
            {
                //新增
                await _parameterService.Add(_macParameter.Adapt<ParameterAddInput>());
            }
            else
            {
                //编辑
                await _parameterService.Edit(_macParameter.Adapt<ParameterEditInput>());
            }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        DialogResult = DialogResult.OK;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}