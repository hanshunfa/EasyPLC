using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraEditors;

namespace EasyPlc.Entry.ChrildrenForms.Org;

public partial class OrgForm : DevExpress.XtraEditors.XtraForm
{
    private readonly ISysOrgService _sysOrgService;
    private Dictionary<string, string> _dicCategory = new Dictionary<string, string>();
    public OrgForm(
        ISysOrgService sysOrgService
        )
    {
        InitializeComponent();
        _sysOrgService = sysOrgService;
    }

    private void OrgForm_Load(object sender, EventArgs e)
    {
        _dicCategory.Clear();
        _dicCategory.Add("COMPANY", "公司");
        _dicCategory.Add("DEPT", "部门");
        InitOrg();
        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
    }

    private void InitOrg()
    {
        treeList1.SetConfigTreeList();
        SetDataSource();
    }
    private List<SysOrg> _sysOrgs = new List<SysOrg>();
    private void SetDataSource()
    {
        _sysOrgs = GetList();
        //枚举转换
        _sysOrgs.ForEach(x =>
        {
            string value = "未知";
            _dicCategory.TryGetValue(x.Category, out value);
            x.Category = value;
        });
        treeList1.DataSource = _sysOrgs;

        treeList1.RefreshDataSource();//刷新reeList1
        treeList1.ExpandAll();//展开所有节点
    }

    private List<SysOrg> GetList()
    {
        return _sysOrgService.GetListAsync().Result;
    }

    #region 节点
    /// <summary>
    /// 节点选中前事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void treeList1_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
    {
        //e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
    }

    /// <summary>
    /// 节点选中后事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void treeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
    {
        //SetCheckedChildNodes(e.Node, e.Node.CheckState);
        //SetCheckedParentNodes(e.Node, e.Node.CheckState);
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

    #region 增删改查
    private SysOrg _sysOrg;

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var editTree = CreateEditTree();
        var editForm = Native.CreateInstance<OrgEditForm>(new SysOrg(), editTree);
        if(editForm.ShowDialog() == DialogResult.OK)
        {
            SetDataSource();
        }
    }
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var editTree = CreateEditTree();
        var editForm = Native.CreateInstance<OrgEditForm>(_sysOrg, editTree);
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            SetDataSource();
        }
    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (XtraMessageBox.Show($"确定删除【{_sysOrg.Name}】？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
        {
            try
            {
                await _sysOrgService.Delete(new List<BaseIdInput> { new BaseIdInput { Id = _sysOrg.Id } });
                SetDataSource();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    /// <summary>
    /// 刷新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        SplashScreenManager.ShowForm(typeof(WaitForm));

        SetDataSource();

        SplashScreenManager.CloseForm();
    }
    #endregion
    /// <summary>
    /// 光标节点变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
    {
        TreeList workSpaceTree = sender as TreeList;
        TreeListNode node = workSpaceTree.FocusedNode;
        if (null != node)
        {
            var id = node.GetDisplayText("Id").ToLong();
            _sysOrg = _sysOrgs.Where(it => it.Id == id).FirstOrDefault();
        }
    }
   

    #region 方法
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private List<EditNode> CreateEditTree()
    {
        List<EditNode> editNodes = new List<EditNode>
        {
            new EditNode() { Id = 19900522, ParentId = 0, Name = "顶级" }
        };
        //找到ParentId = 0 的
        _sysOrgs.ForEach(it =>
        {
            if (it.ParentId == 0)
                editNodes.Add(new EditNode() { Id = it.Id, ParentId = 19900522, Name = it.Name });
            else
                editNodes.Add(new EditNode() { Id = it.Id, ParentId = it.ParentId, Name = it.Name });
        });
        return editNodes;
    }
    #endregion
}