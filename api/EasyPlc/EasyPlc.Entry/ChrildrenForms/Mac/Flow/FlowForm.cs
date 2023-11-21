
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using EasyPlc.System;
using DevExpress.XtraEditors;

namespace EasyPlc.Entry.ChrildrenForms.Mac;

public partial class FlowForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IMacFlowService _macFlowService;
    private readonly IMacEquipmentService _macEquipmentService;

    public FlowForm(
        IMacFlowService macFlowService,
        IMacEquipmentService macEquipmentService
        )
    {
        InitializeComponent();

        _macFlowService = macFlowService;
        _macEquipmentService = macEquipmentService;
    }
    private async void FlowForm_Load(object sender, EventArgs e)
    {
        _dicCategory.Clear();
        _dicCategory.Add("FLOW_MORMAL", "正常流程");
        _dicCategory.Add("FLOW_REPAIR", "返修流程");
        //流程
        treeListFlow.SetConfigTreeList();
        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
        await RefreshTreeListFlow();
        //流程关系
        ConfigTrrlistFlowRelation();
        await RefreshFlowRelation();
    }

    #region 流程

    private Dictionary<string, string> _dicCategory = new Dictionary<string, string>();
    private List<MacFlow> _macFlows = new List<MacFlow>();
    private async Task RefreshTreeListFlow()
    {
        _macFlows = await _macFlowService.GetListBySortCodeAsync();
        //替换
        _macFlows.ForEach(x =>
        {
            //分类
            string value = "未知";
            _dicCategory.TryGetValue(x.Category, out value);
            x.Category = value;
        });
        treeListFlow.DataSource = null;
        treeListFlow.DataSource = _macFlows;
        //treeListFlow.ExpandAll();//展开所有节点
    }
    private MacFlow _macFlow = null;
    private async void treeListFlow_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
    {
        TreeList tree = sender as TreeList;
        TreeListNode node = tree.FocusedNode;
        if (null != node)
        {
            var id = node.GetDisplayText("Id").ToLong();
            _macFlow = _macFlows.Where(it => it.Id == id).FirstOrDefault();
        }
        //跟新右侧
        await RefreshFlowRelation();
    }
    #endregion

    #region 流程关系
    private async void ConfigTrrlistFlowRelation()
    {
        treeListFlowRelation.OptionsBehavior.Editable = false;
        //treeList1.ExpandAll();//展开所有节点
        treeListFlowRelation.KeyFieldName = "Id";
        treeListFlowRelation.ParentFieldName = "ParentId";

        treeListFlowRelation.OptionsView.ShowCheckBoxes = true;// //是否显示CheckBox
        treeListFlowRelation.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
        treeListFlowRelation.OptionsBehavior.AllowIndeterminateCheckState = true;// //设置节点是否有中间状态，即一部分子节点选中，一部分子节点没有选中

        var _equipmentList = await _macEquipmentService.GetListBySortCodeAsync();
        treeListFlowRelation.DataSource = _equipmentList;
        treeListFlowRelation.ExpandAll();
    }
    private async Task RefreshFlowRelation()
    {
        if (_macFlow != null)
        {
            //设置已选择工位
            var ids = (await _macEquipmentService.GetEquipmentListByFlowId(_macFlow.Id)).Select(it => it.Id).ToList();
            SetCheckNodes(treeListFlowRelation, ids);
        }
    }

    #region 节点
    /// <summary>
    /// 节点选中前事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void treeListFlowParam_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
    {
        e.CanCheck = false;//屏蔽
        e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
    }
    /// <summary>
    /// 节点选中后事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void treeListFlowParam_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
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

    #endregion

    #region 操作

    /// <summary>
    /// 新增流程
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var treeAll = CreateEditTreeAll();
        var editForm = Native.CreateInstance<FlowEditForm>(treeAll, new MacFlow());
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            //刷新
            await RefreshTreeListFlow();
        }
    }
    /// <summary>
    /// 编辑流程
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (_macFlow != null)
        {
            var treeAll = CreateEditTreeAll();
            var editForm = Native.CreateInstance<FlowEditForm>(treeAll, _macFlow);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                //刷新
                await RefreshTreeListFlow();
            }
        }
        else
        {
            XtraMessageBox.Show("请选择需要编辑行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    /// <summary>
    /// 删除流程
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (_macFlow != null)
        {
            if (XtraMessageBox.Show($"确定删除【{_macFlow.Names}】？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
            {
                try
                {
                    await _macFlowService.Delete(new List<BaseIdInput> { new BaseIdInput { Id = _macFlow.Id } });
                    await RefreshTreeListFlow();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        else
        {
            XtraMessageBox.Show("请选择需要删除行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    /// <summary>
    /// 刷新流程
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        await RefreshTreeListFlow();
    }


    /// <summary>
    /// 编辑参数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    /// <summary>
    /// 保存参数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    /// <summary>
    /// 刷新参数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }

    #endregion

    private List<EditNode> CreateEditTreeAll()
    {
        List<EditNode> editNodes = new List<EditNode>
        {
            new EditNode() { Id = 19900522, ParentId = 0, Name = "顶级" }
        };
        //找到ParentId = 0 的
        _macFlows.ForEach(it =>
        {
            if (it.ParentId == 0)
                editNodes.Add(new EditNode() { Id = it.Id, ParentId = 19900522, Name = it.Name });
            else
                editNodes.Add(new EditNode() { Id = it.Id, ParentId = it.ParentId, Name = it.Name });
        });
        return editNodes;
    }


}