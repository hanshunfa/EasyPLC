using DevExpress.XtraEditors;
using Mapster;
using EasyPlc.System;
using static EasyPlc.Entry.ChrildrenForms.Mac.ModelForm;

namespace EasyPlc.Entry.ChrildrenForms.Mac;

public partial class ModelEditForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IMacModelService _macModelService;
    private readonly List<EditNode> _treeNodes;
    private readonly MacModel _macModel;

    public ModelEditForm(
        IMacModelService macModelService,
        List<EditNode> treeNodes,
        MacModel macModel
        )
    {
        InitializeComponent();

        _macModelService = macModelService;
        _treeNodes = treeNodes;
        _macModel = macModel;

        //修改标题
        Text = $"型号管理" + "-" + (_macModel.Id == 0 ? "新增" : "编辑");
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
        if (_macModel.Id == 0)
        {
            //新增
        }
        else
        {
            //编辑
            treeListLookUpEdit1.EditValue = _macModel.ParentId == 0 ? 19900522 : _macModel.ParentId;
            textEdit1.Text = _macModel.Name;
            textEdit2.Text = _macModel.Code;
            comboBoxEdit1.Text = _macModel.Category;
            trackBarControl1.Value = _macModel.SortCode ?? 99;
        }
    }

    private async void btnOk_Click(object sender, EventArgs e)
    {
        var treeId = treeListLookUpEdit1.EditValue.ToLong() == 19900522 ? 0 : treeListLookUpEdit1.EditValue.ToLong();
        _macModel.ParentId = treeId;
        _macModel.Name = textEdit1.Text.Trim();
        _macModel.Code = textEdit2.Text.Trim();
        _macModel.Category = comboBoxEdit1.Text == "分类" ? "MODEL_CLASS" : "MODEL_MODEL";
        _macModel.SortCode = trackBarControl1.Value;

        try
        {
            if (_macModel.Id == 0)
            {
                //新增
                await _macModelService.Add(_macModel.Adapt<MacModelAddInput>());
            }
            else
            {
                //编辑
                await _macModelService.Edit(_macModel.Adapt<MacModelEditInput>());
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