using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace EasyPlc.Entry.ChrildrenForms.Org;

public partial class UserForm : DevExpress.XtraEditors.XtraForm
{
    private readonly ISysOrgService _sysOrgService;
    private readonly ISysUserService _sysUserService;

    public UserForm(
         ISysOrgService sysOrgService,
         ISysUserService sysUserService
        )
    {
        InitializeComponent();

        _sysOrgService = sysOrgService;
        _sysUserService = sysUserService;
    }
    private async void UserForm_Load(object sender, EventArgs e)
    {
        treeList1.SetConfigTreeList();
        gridView1.SetConfigGridView();
        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
        await RefreshTreeList();//刷新数据
        //分页控件
        paginationControl1.Method = LoadData;
    }
    private async Task LoadData(int page = 1)
    {
        await RefreshGrid(page, paginationControl1.PageSize);
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
    //显示行号
    private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
    {
        if (e.Info.IsRowIndicator && e.RowHandle >= 0)
        {
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
    private List<SysUser> _sysUsers = new List<SysUser>();
    private async Task RefreshGrid(int currentPage, int pageSize)
    {
        try
        {
            var pageList = await _sysUserService.Page(new UserPageInput()
            {
                OrgId = _sysOrg.Id,
                Current = currentPage,
                Size = pageSize
            });
            //自定义变换
            pageList.Records.ForEach(it =>
            {
                it.UserStatus = it.UserStatus == "ENABLE" ? "启用" : "禁用";
            });
            gridControl1.DataSource = null;
            gridControl1.DataSource = _sysUsers = pageList.Records.ToList();
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
    private SysUser _sysUser;
    /// <summary>
    /// girdview 选择行变化
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
            _sysUser = _sysUsers[selectRow];
        }
        else
        {
            _sysUser = null;
        }
    }

    #region 方法

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

    #endregion

    #region 操作

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var orgTree = CreateOrgEditTree();
        var editForm = Native.CreateInstance<UserEditForm>(orgTree, new SysUser());
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
        if (_sysUser != null)
        {
            var orgTree = CreateOrgEditTree();
            var editForm = Native.CreateInstance<UserEditForm>(orgTree, _sysUser);
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
        if (_sysUser != null)
        {
            if (XtraMessageBox.Show($"确定删除【{_sysUser.Account}】？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
            {
                try
                {
                    await _sysUserService.Delete(new List<BaseIdInput> { new BaseIdInput { Id = _sysUser.Id } });
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
    /// 修改密码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    /// <summary>
    /// 角色授权
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


}