using DevExpress.XtraEditors;
using Mapster;
using EasyPlc.System;
using static EasyPlc.Entry.ChrildrenForms.Mac.EquipmentForm;

namespace EasyPlc.Entry.ChrildrenForms.Mac;

public partial class EquipmentEditForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IMacEquipmentService _macEquipmentService;
    private readonly List<EditNode> _treeNodes;
    private readonly MacEquipment _macEquipment;

    public EquipmentEditForm(
        IMacEquipmentService macEquipmentService,
        List<EditNode> treeNodes,
        MacEquipment macEquipment

        )
    {
        InitializeComponent();

        _macEquipmentService = macEquipmentService;
        _treeNodes = treeNodes;
        _macEquipment = macEquipment;

        //修改标题
        Text = $"设备管理" + "-" + (_macEquipment.Id == 0 ? "新增" : "编辑");
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
        if (_macEquipment.Id == 0)
        {
            //新增
        }
        else
        {
            //编辑
            treeListLookUpEdit1.EditValue = _macEquipment.ParentId == 0 ? 19900522 : _macEquipment.ParentId; ;
            textEdit1.Text = _macEquipment.Name;
            textEdit2.Text = _macEquipment.Code;
            comboBoxEdit1.Text = _macEquipment.Category;
            trackBarControl1.Value = _macEquipment.SortCode ?? 99;
        }
    }

    private async void btnOk_Click(object sender, EventArgs e)
    {
        var treeId = treeListLookUpEdit1.EditValue.ToLong() == 19900522 ? 0 : treeListLookUpEdit1.EditValue.ToLong();
        _macEquipment.ParentId = treeId;
        _macEquipment.Name = textEdit1.Text.Trim();
        _macEquipment.Code = textEdit2.Text.Trim();
        _macEquipment.Category = comboBoxEdit1.Text == "产线" ? "LINE" : (comboBoxEdit1.Text == "设备" ? "EQUIPMENT" : "STATION");
        _macEquipment.SortCode = trackBarControl1.Value;

        try
        {
            if (_macEquipment.Id == 0)
            {
                //新增
                await _macEquipmentService.Add(_macEquipment.Adapt<MacEquipmentAddInput>());
            }
            else
            {
                //编辑
                await _macEquipmentService.Edit(_macEquipment.Adapt<MacEquipmentEditInput>());
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