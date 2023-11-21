using DevExpress.Charts.Native;
using DevExpress.Mvvm.POCO;
using DevExpress.XtraEditors;
using Mapster;
using Masuit.Tools;
using EasyPlc.System;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class CustomDataEditForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IBaseDataService _baseDataService;
    private readonly IStructDataService _structDataService;
    private readonly IArrDataService _arrDataService;
    private readonly List<EditNode> _nodes;
    private readonly PlcResource _plcResource;

    public CustomDataEditForm(
        IBaseDataService baseDataService,
        IStructDataService structDataService,
        IArrDataService arrDataService,
        List<EditNode> nodes,
        PlcResource plcResource
        )
    {
        InitializeComponent();

        _baseDataService = baseDataService;
        _structDataService = structDataService;
        _arrDataService = arrDataService;
        _nodes = nodes;
        _plcResource = plcResource;
        InitEditForm();
        InitData();
    }
    private void InitEditForm()
    {
        //修改标题
        Text = $"数据结构配置" + "-" + (_plcResource.Id == 0 ? "新增" : "编辑");
        treeListLookUpEdit1.Properties.DataSource = _nodes;
    }
    private void InitData()
    {
        if (_plcResource.Id == 0)
        {

        }
        else
        {
            treeListLookUpEdit1.EditValue = _plcResource.ParentId == 0 ? 19900522 : _plcResource.ParentId;
            textEdit1.Text = _plcResource.Title;
            textEdit2.Text = _plcResource.Code;
            comboBoxEdit1.Text = _plcResource.ValueType;
            comboBoxEdit2.Text = _plcResource.Category;
            spinEdit2.Value = _plcResource.ValueLength;
            spinEdit1.Value = _plcResource.SortCode ?? 99;
        }
    }
    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void simpleButton1_Click(object sender, EventArgs e)
    {
        try
        {
            var treeId = treeListLookUpEdit1.EditValue.ToLong() == 19900522 ? 0 : treeListLookUpEdit1.EditValue.ToLong();
            _plcResource.ParentId = treeId;
            _plcResource.Title = textEdit1.Text.Trim();
            _plcResource.Code = textEdit2.Text.Trim();
            _plcResource.ValueType = comboBoxEdit1.Text.Trim();
            _plcResource.ValueLength = spinEdit2.Value.ToInt();
            _plcResource.SortCode = spinEdit1.Value.ToInt();

            var category = comboBoxEdit2.Text.Trim();
            if (category == "基本类型")
            {
                if (_plcResource.Id == 0)
                    await _baseDataService.Add(_plcResource.Adapt<BaseDataAddInput>());
                else
                    await _baseDataService.Edit(_plcResource.Adapt<BaseDataEditInput>());
            }
            else if (category == "结构类型")
            {
                _plcResource.ValueType = null;
                _plcResource.ValueLength = 1;
                if (_plcResource.Id == 0)
                    await _structDataService.Add(_plcResource.Adapt<StructDataAddInput>());
                else
                    await _structDataService.Edit(_plcResource.Adapt<StructDataEditInput>());
            }
            else
            {
                _plcResource.ValueType = null;
                if (_plcResource.Id == 0)
                    await _arrDataService.Add(_plcResource.Adapt<ArrDataAddInput>());
                else
                    await _arrDataService.Edit(_plcResource.Adapt<ArrDataEditInput>());
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
    /// <summary>
    /// 数据类型发生变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboBoxEdit1.Text == "String" || comboBoxEdit1.Text == "WString")
        {
            layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
        else
            layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
    }
    /// <summary>
    /// 数据分类发生变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboBoxEdit2.Text == "结构类型")
        {
            layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        else if (comboBoxEdit2.Text == "数组类型")
        {
            layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        else
        {
            layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
    }
}