using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using System.Threading.Tasks;
using EasyPlc.System;
using DevExpress.Mvvm.Native;
using EasyPlc.Entry.ChrildrenForms.Org;
using DevExpress.XtraSplashScreen;
namespace EasyPlc.Entry.ChrildrenForms.Role;

public partial class RoleForm : DevExpress.XtraEditors.XtraForm
{
    private readonly ISysOrgService _sysOrgService;
    private readonly IRoleService _roleService;

    public RoleForm(
        ISysOrgService sysOrgService,
        IRoleService roleService
        )
    {
        InitializeComponent();

        _sysOrgService = sysOrgService;
        _roleService = roleService;
    }

    private async void RoleForm_Load(object sender, EventArgs e)
    {

        treeList1.SetConfigTreeList();
        gridView1.SetConfigGridView();
        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
        await RefreshTreeList();
        //分页控件
        paginationControl1.Method = LoadData;
    }
    private async Task LoadData(int page = 1)
    {
        await RefreshGrid(page, paginationControl1.PageSize);
    }
    List<SysRole> _sysRoles = new List<SysRole>();
    private async Task RefreshGrid(int currentPage, int pageSize)
    {
        try
        {
            var pageList = await _roleService.Page(new RolePageInput()
            {
                OrgId = _sysOrg.Id,
                Category = _sysOrg.Category,
                Current = currentPage,
                Size = pageSize
            });
            //自定义变换
            pageList.Records.ForEach(it =>
            {
                it.Category = it.Category == "GLOBAL" ? "全局" : "机构";
            });
            gridControl1.DataSource = null;
            gridControl1.DataSource = _sysRoles = pageList.Records.ToList();
            paginationControl1.SetPage(pageList.Current, pageList.Total);
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

    private SysOrg _sysOrg;
    private async void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
    {
        TreeList tree = sender as TreeList;
        TreeListNode node = tree.FocusedNode;
        if (null != node)
        {
            var id = node.GetDisplayText("Id").ToLong();
            if (id == 19900522)
            {
                //查询所有
                _sysOrg = new SysOrg();
            }
            else
            {
                if (id != -1)
                {
                    _sysOrg = _sysOrgs.Where(it => it.Id == id).FirstOrDefault();
                    _sysOrg.Category = "ORG";
                }
                else //全局
                    _sysOrg = new SysOrg() { Id = -1, ParentId = -1, Category = "GLOBAL" };
            }
            //刷新
            await RefreshGrid(1, paginationControl1.PageSize);
        }
    }
    private SysRole _sysRole;
    /// <summary>
    /// 行变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
    {
        var srs = gridView1.GetSelectedRows();
        if (srs.Length > 0)
        {
            //获得选中的行，如果是单选模式，则直接取第一个
            int selectRow = srs[0];
            //从绑定的行数据直接取数据
            _sysRole = _sysRoles[selectRow];
        }
        else
        {
            _sysRole = null;
        }
    }
    #region 方法

    /// <summary>
    /// 创建一个包含全局的组织
    /// </summary>
    /// <returns></returns>
    private List<EditNode> CreateEditTreeAll()
    {
        List<EditNode> editNodes = new List<EditNode>
        {
            new EditNode() { Id = 19900522, ParentId = 0, Name = "顶级" },//顶级 全部的
            new EditNode() { Id = -1, ParentId = 19900522, Name = "全局" }
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
    /// <summary>
    /// 创建一个原有组织
    /// </summary>
    /// <returns></returns>
    private List<EditNode> CreateOrgEditTree()
    {
        List<EditNode> editNodes = new List<EditNode>();
        _sysOrgs.ForEach(it =>
        {
            editNodes.Add(new EditNode() { Id = it.Id, ParentId = it.ParentId, Name = it.Name });
        });
        return editNodes;
    }

    public class RadioEditValue
    {
        public int Level { get; set; }
        public string Title { get; set; }
        public string ScopeCategory { get; set; }
    }

    #endregion

    #region 操作项
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var orgTree = CreateOrgEditTree();
        var editForm = Native.CreateInstance<RoleEditForm>(orgTree, new SysRole());
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            //刷新
            await RefreshGrid(1, paginationControl1.PageSize);
        }
    }
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (_sysRole != null)
        {
            var orgTree = CreateOrgEditTree();
            var editForm = Native.CreateInstance<RoleEditForm>(orgTree, _sysRole);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                //刷新
                await RefreshGrid(1, paginationControl1.PageSize);
            }
        }
        else
        {
            XtraMessageBox.Show("请选择需要编辑行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (_sysRole != null)
        {
            if (XtraMessageBox.Show($"确定删除【{_sysRole.Name}】？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
            {
                try
                {
                    await _roleService.Delete(new List<BaseIdInput> { new BaseIdInput { Id = _sysRole.Id } });
                    await RefreshGrid(1, paginationControl1.PageSize);//刷新右边grid
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
    /// 刷新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        SplashScreenManager.ShowForm(typeof(WaitForm));

        await RefreshGrid(1, paginationControl1.PageSize);//刷新右边grid

        SplashScreenManager.CloseForm();
    }
    /// <summary>
    /// 授权用户
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    /// <summary>
    /// 授权资源
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    #endregion

    /// <summary>
    /// 鼠标松开
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gridView1_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None)
        {
            Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
            var hitInfo = gridView1.CalcHitInfo(e.Location);
            if (hitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu1.ShowPopup(p);
            }
        }
    }

    #region 右键菜单

    /// <summary>
    /// 授权资源
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    /// <summary>
    /// 授权用户
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var grantUserForm = Native.CreateInstance<RoleGrantUserForm>();
        grantUserForm.ShowDialog();
    }
    #endregion
}