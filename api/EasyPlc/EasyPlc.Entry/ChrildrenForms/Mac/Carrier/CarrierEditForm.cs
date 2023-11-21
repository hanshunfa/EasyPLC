using DevExpress.XtraEditors;
using Mapster;
using EasyPlc.System;

namespace EasyPlc.Entry.ChrildrenForms.Mac.Carrier;

public partial class CarrierEditForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IMacCarrierService _macCarrierService;
    private readonly List<MacModel> _modelTree;
    private readonly MacCarrier _macCarrier;

    public CarrierEditForm(
        IMacCarrierService macCarrierService,
        List<MacModel> modelTree,
        MacCarrier macCarrier
        )
    {
        InitializeComponent();

        _macCarrierService = macCarrierService;
        _modelTree = modelTree;
        _macCarrier = macCarrier;

        //修改标题
        Text = $"载具管理" + "-" + (_macCarrier.Id == 0 ? "新增" : "编辑");

        InitEditForm();
        InitData();
    }
    private void InitEditForm()
    {
        treeListLookUpEdit1.Properties.DataSource = _modelTree;
    }
    private void InitData()
    {
        if (_macCarrier.Id == 0)
        {

        }
        else
        {
            treeListLookUpEdit1.EditValue = _macCarrier.ModelId;
            textEdit1.Text = _macCarrier.Name;
            textEdit2.Text = _macCarrier.Code;
            comboBoxEdit1.Text = _macCarrier.CarrierStatus;
            spinEdit1.Value = _macCarrier.NumberOfPosition;
            trackBarControl1.Value = _macCarrier.SortCode ?? 99;

        }
    }

    private async void btnOk_Click(object sender, EventArgs e)
    {
        //提取数据
        _macCarrier.ModelId = treeListLookUpEdit1.EditValue.ToLong();
        _macCarrier.Name = textEdit1.Text.Trim();
        _macCarrier.Code = textEdit2.Text.Trim();
        _macCarrier.CarrierStatus = comboBoxEdit1.Text == "启用" ? "ENABLE" : "DISABLED";
        _macCarrier.NumberOfPosition = spinEdit1.Value.ToInt();
        _macCarrier.SortCode = trackBarControl1.Value;

        //分类默认
        _macCarrier.Category = "CARRIER_LINE";
        try
        {
            if (_macCarrier.Id == 0)
            {
                //新增
                await _macCarrierService.Add(_macCarrier.Adapt<CarrierAddInput>());
            }
            else
            {
                //编辑
                await _macCarrierService.Edit(_macCarrier.Adapt<CarrierEditInput>());
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