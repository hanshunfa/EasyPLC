using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using EasyPlc.System;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using EasyPlc.Entry.ChrildrenForms.Org;

namespace EasyPlc.Entry.ChrildrenForms.Resoure.Menu;

public partial class MenuForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IMenuService _menuService;
    private readonly IButtonService _buttonService;

    public MenuForm(
        IMenuService menuService,
        IButtonService buttonService
        )
    {
        InitializeComponent();

        _menuService = menuService;
        _buttonService = buttonService;
    }
    private async void MenuForm_Load(object sender, EventArgs e)
    {
        ConfigTrrlist();
        gridView1.SetConfigGridView();
        await RefreshTree();
        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();

    }
    private void ConfigTrrlist()
    {
        //treeList1.OptionsBehavior.Editable = false;
        ////treeList1.ExpandAll();//展开所有节点
        //treeList1.KeyFieldName = "Id";
        //treeList1.ParentFieldName = "ParentId";

        //treeList1.OptionsView.ShowCheckBoxes = true;// //是否显示CheckBox
        //treeList1.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
        //this.treeList1.OptionsBehavior.AllowIndeterminateCheckState = true;// //设置节点是否有中间状态，即一部分子节点选中，一部分子节点没有选中

        treeList1.SetConfigTreeList();
    }

    private List<SysResource> _sysButtons = new List<SysResource>();
    private async Task RefreshGrid()
    {
        try
        {
            if (_sysMenu == null)
            {
                gridControl1.DataSource = null;
                paginationControl1.SetPage(1, 0);
                return;
            }
            //只有菜单才有按钮
            if (_sysMenu.MenuType == "菜单")
            {
                var pageList = await _buttonService.Page(new ButtonPageInput()
                {
                    ParentId = _sysMenu.Id
                });
                //自定义变换
                pageList.Records.ForEach(it =>
                {

                });
                gridControl1.DataSource = null;
                gridControl1.DataSource = _sysButtons = pageList.Records.ToList();
                paginationControl1.SetPage(pageList.Current, pageList.Total);
            }
            else
            {
                _sysButtons.Clear();
                gridControl1.DataSource = null;
                paginationControl1.SetPage(1, 0);
            }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
        }
    }
    private List<SysResource> _sysMenus = new List<SysResource>();
    private async Task RefreshTree()
    {
        _sysMenus = await _menuService.GetListAsync(new MenuTreeInput { Module = 212725263003721 });
        //替换
        _sysMenus.ForEach(x =>
        {
            x.MenuType = x.MenuType == "CATALOG" ? "目录" : "菜单";
        });
        treeList1.DataSource = null;
        treeList1.DataSource = _sysMenus;
        treeList1.RefreshDataSource();//刷新
        //treeList1.ExpandAll();//展开所有节点
    }
    private SysResource _sysMenu = null;
    private async void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
    {
        TreeList tree = sender as TreeList;
        TreeListNode node = tree.FocusedNode;
        if (null != node)
        {
            var id = node.GetDisplayText("Id").ToLong();
            _sysMenu = _sysMenus.Where(it => it.Id == id).FirstOrDefault();

            //跟新右侧按钮
            await RefreshGrid();

        }
        else
        {
            //跟新右侧按钮
            await RefreshGrid();
        }
    }
    private SysResource _sysButton = null;
    private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
    {
        var srs = gridView1.GetSelectedRows();
        if (srs.Length > 0)
        {
            //获得选中的行，如果是单选模式，则直接取第一个
            int selectRow = srs[0];
            //从绑定的行数据直接取数据
            _sysButton = _sysButtons[selectRow];
        }
        else
        {
            _sysButton = null;
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

    #region 方法

    /// <summary>
    /// 创建一个包含最上层所有组织节点
    /// </summary>
    /// <returns></returns>
    private List<EditNode> CreateEditTreeAll()
    {
        List<EditNode> editNodes = new List<EditNode>
        {
        new EditNode() { Id = 19900522, ParentId = 0, Name = "顶级" }
        };
        //找到ParentId = 0 的
        _sysMenus.ForEach(it =>
        {
            if (it.ParentId == 0)
                editNodes.Add(new EditNode() { Id = it.Id, ParentId = 19900522, Name = it.Title });
            else
                editNodes.Add(new EditNode() { Id = it.Id, ParentId = it.ParentId ?? 0, Name = it.Title });
        });
        return editNodes;
    }
    /// <summary>
    /// 创建一个原有组织
    /// </summary>
    /// <returns></returns>
    private List<EditNode> CreateEditTree()
    {
        List<EditNode> editNodes = new List<EditNode>();
        _sysMenus.ForEach(it =>
        {
            editNodes.Add(new EditNode() { Id = it.Id, ParentId = it.ParentId ?? 0, Name = it.Title });
        });
        return editNodes;
    }

    #endregion

    #region 菜单操作
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var menuTree = CreateEditTreeAll();
        var editForm = Native.CreateInstance<MenuEditForm>(menuTree, new SysResource());
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            //刷新
            await RefreshTree();

        }
    }
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (_sysMenu != null)
        {
            var menuTree = CreateEditTreeAll();
            var editForm = Native.CreateInstance<MenuEditForm>(menuTree, _sysMenu);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                //刷新
                await RefreshTree();
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
        if (_sysMenu != null)
        {
            if (XtraMessageBox.Show($"确定删除【{_sysMenu.Title}】？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
            {
                try
                {
                    await _menuService.Delete(new List<BaseIdInput> { new BaseIdInput { Id = _sysMenu.Id } });
                    //刷新
                    await RefreshTree();
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
        //刷新
        await RefreshTree();
    }

    #endregion

    #region 按钮操作项
    /// <summary>
    /// 新增按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    /// <summary>
    /// 编辑按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    /// <summary>
    /// 删除按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {

    }
    /// <summary>
    /// 刷新按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        await RefreshGrid();
    }
    #endregion
}