using DevExpress.XtraEditors;
using Mapster;
using SimpleTool;
using EasyPlc.SqlSugar;
using EasyPlc.System;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class OrderEditForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IProOrderService _proOrderService;
    private readonly IMacModelService _macModelService;
    private readonly IMacFlowService _macFlowService;
    private readonly ProOrder _proOrder;

    public OrderEditForm(
        IProOrderService proOrderService,
        IMacModelService macModelService,
        IMacFlowService macFlowService,
        ProOrder proOrder
        )
    {
        _proOrderService = proOrderService;
        _macModelService = macModelService;
        _macFlowService = macFlowService;
        _proOrder = proOrder;

        InitializeComponent();

        layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;//工单类型
        layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
    }
    private async void OrderEditForm_Load(object sender, EventArgs e)
    {
        await InitFlow();

        Text = (_proOrder.Id == 0 ? "新增" : "编辑") + "工单";

        if (_proOrder.Id == 0)
        {
            //新增
        }
        else
        {
            if (_proOrder.OrderType == "Normal")
            {
                layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;//工单类型
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //编辑
                textEdit1.Text = _proOrder.Sono;
                var flow = macFlows.Where(it => it.Id == _proOrder.FlowId).FirstOrDefault();
                if (flow != null)
                {
                    comboBoxEdit2.Text = flow.Name;
                }
                spinEdit1.Value = _proOrder.PlanQty;
                textEdit2.Text = _proOrder.Batch;
            }
            else
            {
                layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;//工单类型
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                var flow = macFlows.Where(it => it.Id == _proOrder.FlowId).FirstOrDefault();
                if (flow != null)
                {
                    comboBoxEdit2.Text = flow.Name;
                }
            }
        }
    }
    private List<MacModel> macModels = new List<MacModel>();
    private List<MacFlow> macFlows = new List<MacFlow>();
    /// <summary>
    /// 初始化型号和工艺路线
    /// </summary>
    private async Task InitFlow()
    {
        comboBoxEdit2.Properties.Items.Clear();
        var flowList = await _macFlowService.GetListBySortCodeAsync();
        macFlows = flowList.Where(it => it.Category == CateGoryConst.Mac_FLOW_MORMAL).ToList();
        macFlows.ForEach(it =>
        {
            comboBoxEdit2.Properties.Items.Add(it.Name);
        });

    }

    /// <summary>
    /// 确定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void simpleButton1_Click(object sender, EventArgs e)
    {
        if (comboBoxEdit1.Text == "正常工单")
        {

            //防呆
            if (string.IsNullOrEmpty(textEdit1.Text.Trim()))
            {
                XtraMessageBox.Show("请输入车间订单号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxEdit2.SelectedIndex == -1)
            {
                XtraMessageBox.Show("请选择工艺路线", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textEdit2.Text))
            {
                XtraMessageBox.Show("请输入批次号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _proOrder.OrderType = "Normal";
            _proOrder.Sono = textEdit1.Text.Trim();
            var flow = macFlows.Where(it => it.Name == comboBoxEdit2.Text).FirstOrDefault();
            _proOrder.FlowId = flow.Id;
            _proOrder.FlowName = flow.Name;
            _proOrder.PlanQty = spinEdit1.Value.ToInt();
            _proOrder.Batch = textEdit2.Text.Trim();

            _proOrder.Status = "AWAIT";

            try
            {
                if (_proOrder.Id == 0)
                {
                    await _proOrderService.Add(_proOrder.Adapt<ProOrderAddInput>());
                }
                else
                {
                    await _proOrderService.Edit(_proOrder.Adapt<ProOrderEditInput>());
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"{ex.Message}", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else//返修工单
        {
            if (comboBoxEdit2.SelectedIndex == -1)
            {
                XtraMessageBox.Show("请选择工艺路线", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _proOrder.OrderType = "Repair";
            _proOrder.Sono = RandomHelper.CreateRandomString(20);
            var flow = macFlows.Where(it => it.Name == comboBoxEdit2.Text).FirstOrDefault();
            _proOrder.FlowId = flow.Id;
            _proOrder.FlowName = flow.Name;
            _proOrder.PlanQty = 99999;
            _proOrder.Batch = "";
            _proOrder.Status = "READY";
            try
            {
                if (_proOrder.Id == 0)
                {
                    await _proOrderService.Add(_proOrder.Adapt<ProOrderAddInput>());
                }
                else
                {
                    await _proOrderService.Edit(_proOrder.Adapt<ProOrderEditInput>());
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"{ex.Message}", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    /// 获取MES信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void simpleButton3_Click(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// 选择工单模式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboBoxEdit1.Text == "正常工单")
        {
            layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
        else
        {
            layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
    }
}