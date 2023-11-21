using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using EasyPlc.System;
using System.Collections;
using DevExpress.XtraDiagram.Base;
using Mapster;
using DevExpress.Charts.Native;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class FlowEditForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IMacModelService _macModelService;
    private readonly IMacEquipmentService _macEquipmentService;
    private readonly IMacFlowService _macFlowService;
    private readonly List<EditNode> _treeNodes;
    private readonly MacFlow _macFlow;

    private Dictionary<string, string> _dicCategory = new Dictionary<string, string>();
    public FlowEditForm(
        IMacModelService macModelService,
        IMacEquipmentService macEquipmentService,
        IMacFlowService macFlowService,
        List<EditNode> treeNodes,
        MacFlow macFlow
        )
    {
        InitializeComponent();

        _macModelService = macModelService;
        _macEquipmentService = macEquipmentService;
        _macFlowService = macFlowService;
        _treeNodes = treeNodes;
        _macFlow = macFlow;
    }
    private async void FlowEditForm_Load(object sender, EventArgs e)
    {
        _dicCategory.Clear();
        _dicCategory.Add("正常流程", "FLOW_MORMAL");
        _dicCategory.Add("返修流程", "FLOW_REPAIR");

        await ConfigTrrlist();

        await InitEditForm();
        await InitData();
    }
    private async Task InitEditForm()
    {
        treeListLookUpEdit1.Properties.DataSource = _treeNodes;

        //型号列表
        comboBoxEdit2.Properties.Items.Clear();
        var modelList = (await _macModelService.GetListBySortCodeAsync()).Where(it => it.Category == "MODEL_MODEL").ToList();
        modelList.ForEach(model =>
        {
            comboBoxEdit2.Properties.Items.Add(model.Name);
        });

    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    private async Task InitData()
    {
        if (_macFlow.Id == 0)
        {
            //新增
        }
        else
        {
            //编辑
            treeListLookUpEdit1.EditValue = _macFlow.ParentId == 0 ? 19900522 : _macFlow.ParentId; ;
            textEdit1.Text = _macFlow.Name;
            comboBoxEdit1.Text = _macFlow.Category;
            spinEdit1.Value = _macFlow.SortCode ?? 99;

            //设置已选择工位
            var ids = (await _macEquipmentService.GetEquipmentListByFlowId(_macFlow.Id)).Select(it => it.Id).ToList();
            SetCheckNodes(treeList1, ids);
            //判断流程分类
            if (_macFlow.Category == "FLOW_MORMAL")
            {
                var model = await _macModelService.GetMacModelById(_macFlow.ModelId);
                comboBoxEdit2.Text = model.Name;
            }
            else
            {
                //返修流程 隐藏
                layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }
    }
    /// <summary>
    /// 所属流程发生变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void treeListLookUpEdit1_EditValueChanged(object sender, EventArgs e)
    {

    }

    #region 右边树配置
    private List<MacEquipment> _equipmentList = new List<MacEquipment>();
    private async Task ConfigTrrlist()
    {
        treeList1.OptionsBehavior.Editable = false;
        //treeList1.ExpandAll();//展开所有节点
        treeList1.KeyFieldName = "Id";
        treeList1.ParentFieldName = "ParentId";

        treeList1.OptionsView.ShowCheckBoxes = true;// //是否显示CheckBox
        treeList1.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
        this.treeList1.OptionsBehavior.AllowIndeterminateCheckState = true;// //设置节点是否有中间状态，即一部分子节点选中，一部分子节点没有选中
        //显示可以选择的工位
        _equipmentList = await _macEquipmentService.GetListBySortCodeAsync();
        treeList1.DataSource = _equipmentList;
        treeList1.ExpandAll();
    }
    private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
    {
        TreeList tree = sender as TreeList;
        TreeListNode node = tree.FocusedNode;
        if (null != node)
        {
            var id = node.GetDisplayText("Id").ToLong();
        }
        else
        {

        }
    }

    #region 节点
    /// <summary>
    /// 节点选中前事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void treeList1_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
    {
        e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
    }
    /// <summary>
    /// 节点选中后事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void treeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
    {
        SetCheckedChildNodes(e.Node, e.Node.CheckState);
        SetCheckedParentNodes(e.Node, e.Node.CheckState);
    }
    /// <summary>
    /// 设置子节点的状态
    /// </summary>
    /// <param name="node"></param>
    /// <param name="check"></param>
    private void SetCheckedChildNodes(DevExpress.XtraTreeList.Nodes.TreeListNode node, CheckState check)
    {
        for (int i = 0; i < node.Nodes.Count; i++)
        {
            node.Nodes[i].CheckState = check;
            SetCheckedChildNodes(node.Nodes[i], check);
        }
    }

    /// <summary>
    /// 设置父节点的状态
    /// </summary>
    /// <param name="node"></param>
    /// <param name="check"></param>
    private void SetCheckedParentNodes(DevExpress.XtraTreeList.Nodes.TreeListNode node, CheckState check)
    {
        if (node.ParentNode != null)
        {
            bool b = false;
            CheckState state;
            for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
            {
                state = (CheckState)node.ParentNode.Nodes[i].CheckState;
                if (!check.Equals(state))
                {
                    b = !b;
                    break;
                }
            }
            node.ParentNode.CheckState = b ? CheckState.Indeterminate : check;
            SetCheckedParentNodes(node.ParentNode, check);
        }
    }
    #endregion

    #endregion

    /// <summary>
    /// 流程分类发生变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void comboBoxEdit1_SelectedValueChanged(object sender, EventArgs e)
    {
        var category = comboBoxEdit1.SelectedItem.ToString();
        if (category == "正常流程")
        {
            layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

        }
        else
        {
            layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
    }
    /// <summary>
    /// 所属型号发生变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void comboBoxEdit2_SelectedValueChanged(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 确定按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void simpleButton1_Click(object sender, EventArgs e)
    {
        var treeId = treeListLookUpEdit1.EditValue.ToLong() == 19900522 ? 0 : treeListLookUpEdit1.EditValue.ToLong();
        _macFlow.ParentId = treeId;
        _macFlow.Name = textEdit1.Text.Trim();
        string value = "未知";
        _dicCategory.TryGetValue(comboBoxEdit1.Text, out value);
        _macFlow.Category = value;
        _macFlow.SortCode = spinEdit1.Value.ToInt();

        var modelName = comboBoxEdit2.SelectedItem?.ToString();
        //通过型号名称获取型号
        var model = await _macModelService.GetMacModelByName(modelName);
        if (model != null)
        {
            _macFlow.ModelId = model.Id;//ModelId赋值
        }
        //工序选择
        var idList = GetCheckNodes(treeList1);
        //过滤 提取工位Id
        var ids = _equipmentList.Where(it => idList.Contains(it.Id) && it.Category == "STATION").Select(it => it.Id).ToList();

        long flowId = _macFlow.Id;
        if (_macFlow.Id == 0)
        {
            flowId = await _macFlowService.AddReturnSnowflakeId(_macFlow.Adapt<MacFlowAddInput>());
        }
        else
        {
            await _macFlowService.Edit(_macFlow.Adapt<MacFlowEditInput>());
        }
        await _macFlowService.GrantEuipment(new FlowGrantEquipmentInput { Id = flowId, EquipmentIdList = ids });

        //生成返修流程
        if (toggleSwitch1.IsOn)
        {
            //生成默认返修流程 返修流程是根据正常流程里面的每个工位异常进行返修，即返修流程个数==正常流程工位树
            for (int i = 0; i < ids.Count; i++)
            {
                var equipment = await _macEquipmentService.GetEquipmentById(ids[i]);
                if (equipment != null)
                {
                    //创建对象
                    var fid = await _macFlowService.AddReturnSnowflakeId(new MacFlowAddInput
                    {
                        ParentId = flowId,
                        ModelId = model.Id,
                        EquipmentId = equipment.Id,
                        Name = $"{equipment.Code}",
                        Category = "FLOW_REPAIR",
                        SortCode = i + 1,
                    });
                    //指定对应的返修工位
                    var idsList = ids.Skip(i).ToList();
                    //每个返修工位都需要添加首站
                    if(!idsList.Contains(ids[0])) idsList.Insert(0, ids[0]);
                    await _macFlowService.GrantEuipment(new FlowGrantEquipmentInput { Id = fid, EquipmentIdList = idsList });
                }
            };
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

    #region 方法
    /// <summary>
    /// 枚举树中所有结点。
    /// </summary>
    /// <param name="tree"></param>
    /// <returns></returns>
    public List<long> GetCheckNodes(TreeList tree)
    {
        List<long> ids = new List<long>();
        foreach (TreeListNode n in tree.Nodes)
        {
            if (n.Checked)
            {
                ids.Add(n.GetValue("Id").ToLong());
            }
            if (n.Nodes.Count > 0)
                this.DoGetCheckNodes(ids, n);
        }
        return ids;
    }

    /// <summary>
    /// 剃归，扫描所有结点。
    /// </summary>
    /// <param name="list"></param>
    /// <param name="parentNode"></param>
    private void DoGetCheckNodes(List<long> Ids, TreeListNode parentNode)
    {
        //枚举当前结点的所有子结点
        foreach (TreeListNode n in parentNode.Nodes)
        {
            if (n.Checked)
            {
                Ids.Add(n.GetValue("Id").ToLong());

            }
            if (n.Nodes.Count > 0)
                this.DoGetCheckNodes(Ids, n);
        }
    }

    /// <summary>
    /// 根据Id设置勾选。
    /// </summary>
    /// <param name="tree"></param>
    /// <returns></returns>
    public void SetCheckNodes(TreeList tree, List<long> ids)
    {
        foreach (TreeListNode n in tree.Nodes)
        {
            var id = n.GetValue("Id").ToLong();
            n.Checked = ids.Contains(id);
            SetCheckedChildNodes(n, n.CheckState);
            SetCheckedParentNodes(n, n.CheckState);
            if (n.Nodes.Count > 0)
                this.DoSetCheckNodes(ids, n);
        }
    }

    /// <summary>
    /// 剃归，扫描所有结点。
    /// </summary>
    /// <param name="list"></param>
    /// <param name="parentNode"></param>
    private void DoSetCheckNodes(List<long> ids, TreeListNode parentNode)
    {
        //枚举当前结点的所有子结点
        foreach (TreeListNode n in parentNode.Nodes)
        {
            var id = n.GetValue("Id").ToLong();
            n.Checked = ids.Contains(id);
            SetCheckedChildNodes(n, n.CheckState);
            SetCheckedParentNodes(n, n.CheckState);
            if (n.Nodes.Count > 0)
                this.DoSetCheckNodes(ids, n);
        }
    }
    #endregion
}