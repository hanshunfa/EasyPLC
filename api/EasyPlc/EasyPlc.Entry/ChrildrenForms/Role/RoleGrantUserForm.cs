
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using EasyPlc.System;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class RoleGrantUserForm : DevExpress.XtraEditors.XtraForm
{
    private readonly ISysOrgService _sysOrgService;
    public RoleGrantUserForm(
        ISysOrgService sysOrgService
        )
    {
        InitializeComponent();

        _sysOrgService = sysOrgService;
    }

    private async void RoleGrantUserForm_Load(object sender, EventArgs e)
    {
        treeList1.SetConfigTreeList();
        gridView1.SetConfigGridView();
        gridView2.SetConfigGridView();
        await RefreshTreeList();//刷新数据
        //用户分页控件
        paginationControl1.Method = LoadUsers;
    }

    private async Task LoadUsers(int page = 1)
    {
        await RefreshGrid(page, paginationControl1.PageSize);
    }
    private List<SysUser> _sysUsers = new List<SysUser>();
    private async Task RefreshGrid(int currentPage, int pageSize)
    {
        try
        {

        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
        }
    }

    private List<SysOrg> _sysOrgs = new List<SysOrg>();
    private async Task RefreshTreeList()
    {
        _sysOrgs = await _sysOrgService.GetListAsync();
        var eidtNode = CreateEditTreeAll();//转换成有顶级节点的tree
        treeList1.DataSource = eidtNode;
        treeList1.RefreshDataSource();//刷新reeList1
        treeList1.ExpandAll();//展开所有节点
    }

    /// <summary>
    /// 创建一个包含最上层所有组织节点
    /// </summary>
    /// <returns></returns>
    private List<EditNode> CreateEditTreeAll()
    {
        List<EditNode> editNodes = new List<EditNode>
        {
            new EditNode() { Id = 19900522, ParentId = 0, Name = "全部组织" }
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

    private SysOrg _sysOrg;
    /// <summary>
    /// 组织发生变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
    {
        TreeList workSpaceTree = sender as TreeList;
        TreeListNode node = workSpaceTree.FocusedNode;
        if (null != node)
        {
            var id = node.GetDisplayText("Id").ToLong();
            if (id != 19900522)
                _sysOrg = _sysOrgs.Where(it => it.Id == id).FirstOrDefault();
            else
                _sysOrg = new SysOrg() { Id = 0 };//查询所有
            //刷新
            await RefreshGrid(1, paginationControl1.PageSize);
        }
    }
}