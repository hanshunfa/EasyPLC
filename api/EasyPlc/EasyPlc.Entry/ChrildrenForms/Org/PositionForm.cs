using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyPlc.SqlSugar;
using EasyPlc.System;
using DevExpress.Mvvm.Native;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraEditors.Controls;

namespace EasyPlc.Entry.ChrildrenForms.Org;

public partial class PositionForm : DevExpress.XtraEditors.XtraForm
{
    private Dictionary<string, string> _dicCategory = new Dictionary<string, string>();

    private readonly ISysOrgService _sysOrgService;
    private readonly ISysPositionService _sysPositionService;

    public PositionForm(
         ISysOrgService sysOrgService,
        ISysPositionService sysPositionService
        )
    {
        InitializeComponent();

        _sysOrgService = sysOrgService;
        _sysPositionService = sysPositionService;
    }

    private async void PositionForm_Load(object sender, EventArgs e)
    {
        _dicCategory.Clear();
        _dicCategory.Add("HIGH", "高层");
        _dicCategory.Add("MIDDLE", "中层");
        _dicCategory.Add("LOW", "低层");
        treeList1.SetConfigTreeList();
        gridView1.SetConfigGridView();
        await RefreshTreeList();//刷新数据
        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
        //分页控件
        paginationControl1.Method = LoadData;
    }
    private async Task LoadData(int page = 1)
    {
        var pagePositionList = await RefreshGrid(page, paginationControl1.PageSize);
        gridControl1.DataSource = pagePositionList.Records;

        paginationControl1.SetPage(page, pagePositionList.Total);
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
    /// 编辑和删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public async void ItemBtnClick(object sender, ButtonPressedEventArgs e)
    {
        if (e.Button.Caption == "编辑")
        {
            //编辑
            if (_sysPosition != null)
            {
                var editTree = CreateEditTree();
                var editForm = Native.CreateInstance<PositionEditForm>(_sysPosition, editTree);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    await RefreshGridView();//刷新右边grid
                }
            }
            else
            {
                XtraMessageBox.Show("请选择需要编辑行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        else
        {
            //删除
            if (_sysPosition != null)
            {
                if (XtraMessageBox.Show($"确定删除【{_sysPosition.Name}】？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
                {
                    try
                    {
                        await _sysPositionService.Delete(new List<BaseIdInput> { new BaseIdInput { Id = _sysPosition.Id } });
                        await RefreshGridView();//刷新右边grid
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
    }

    //显示行号
    private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
    {
        if (e.Info.IsRowIndicator && e.RowHandle >= 0)
        {
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
    /// <summary>
    /// 刷新右边gridview
    /// </summary>
    private async Task RefreshGridView()
    {
        var pagePositionList = await RefreshGrid(1, paginationControl1.PageSize);
        gridControl1.DataSource = pagePositionList.Records;
        paginationControl1.SetPage(1, pagePositionList.Total);
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
    private List<EditNode> CreateEditTree()
    {
        List<EditNode> editNodes = new List<EditNode>();
        _sysOrgs.ForEach(it =>
        {
            editNodes.Add(new EditNode() { Id = it.Id, ParentId = it.ParentId, Name = it.Name });
        });
        return editNodes;
    }

    #endregion

    private SysOrg _sysOrg;
    private SysPosition _sysPosition;
    private List<SysPosition> _sysPositions = new List<SysPosition>();
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
            var pagePositionList = await RefreshGrid(1, paginationControl1.PageSize);
            gridControl1.DataSource = _sysPositions = pagePositionList.Records.ToList();
            paginationControl1.SetPage(1, pagePositionList.Total);
        }
    }
    private async Task<SqlSugarPagedList<SysPosition>> RefreshGrid(int currentPage, int pageSize)
    {
        try
        {
            var pageList = await _sysPositionService.Page(new PositionPageInput()
            {
                OrgId = _sysOrg.Id,
                Current = currentPage,
                Size = pageSize
            });
            pageList.Records.ForEach(row =>
            {
                //分类
                string value = "未知";
                _dicCategory.TryGetValue(row.Category, out value);
                row.Category = value;
            });
            return pageList;
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
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
            _sysPosition = _sysPositions[selectRow];
        }
        else
        {
            _sysPosition = null;
        }
    }
    /// <summary>
    /// 焦点行变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
    {

    }

    #region 增删改查


    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var editTree = CreateEditTree();
        var editForm = Native.CreateInstance<PositionEditForm>(new SysPosition(), editTree);
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            await RefreshGridView();//刷新右边grid
        }
    }
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (_sysPosition != null)
        {
            var editTree = CreateEditTree();
            var editForm = Native.CreateInstance<PositionEditForm>(_sysPosition, editTree);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                await RefreshGridView();//刷新右边grid
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
        if (_sysPosition != null)
        {
            if (XtraMessageBox.Show($"确定删除【{_sysPosition.Name}】？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
            {
                try
                {
                    await _sysPositionService.Delete(new List<BaseIdInput> { new BaseIdInput { Id = _sysPosition.Id } });
                    await RefreshGridView();//刷新右边grid
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
        await RefreshGridView();//刷新右边grid
    }
    #endregion

}