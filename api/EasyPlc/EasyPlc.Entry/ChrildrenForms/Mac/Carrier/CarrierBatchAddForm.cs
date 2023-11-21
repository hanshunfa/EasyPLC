using DevExpress.XtraEditors;
using Mapster;
using EasyPlc.System;

namespace EasyPlc.Entry.ChrildrenForms.Mac.Carrier;

public partial class CarrierBatchAddForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IMacCarrierService _macCarrierService;
    private readonly List<MacModel> _modelTree;

    public CarrierBatchAddForm(
        IMacCarrierService macCarrierService,
        List<MacModel> modelTree
        )
    {
        InitializeComponent();

        _macCarrierService = macCarrierService;
        _modelTree = modelTree;

        //修改标题
        Text = $"载具管理" + "-" + "批量新增";
        InitEditForm();
        InitData();
    }
    private void InitEditForm()
    {
        treeListLookUpEdit1.Properties.DataSource = _modelTree;
    }
    private void InitData()
    {
    }

    private async void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            var modelId = treeListLookUpEdit1.EditValue.ToLong();
            var baseName = textEdit1.Text.Trim();
            var baseCode = textEdit2.Text.Trim();
            var carrierStatus = "ENABLE";
            var numberOfPosition = spinEdit2.Value.ToInt();
            var category = "CARRIER_LINE";
            //批量新增数量
            int count = spinEdit1.Value.ToInt();
            for (int i = 0; i < count; i++)
            {
                var carrier = new MacCarrier
                {
                    ModelId = modelId,
                    Name = baseName + "-" + (i + 1).ToString().PadLeft(2, '0'),
                    Code = baseCode + "-" + (i + 1).ToString().PadLeft(2, '0'),
                    CarrierStatus = carrierStatus,
                    NumberOfPosition = numberOfPosition,
                    Category = category,
                    SortCode = i + 1
                };
                //新增
                await _macCarrierService.Add(carrier.Adapt<CarrierAddInput>());

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