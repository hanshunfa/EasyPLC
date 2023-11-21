
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using EasyPlc.Entry.ChrildrenForms.Org;
using EasyPlc.System;
using DevExpress.XtraBars;
using System.Windows.Media;
using EasyPlc.Entry.ChrildrenForms.Adapter.PLC;
using HslCommunication.Core;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class CustomDataForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IPlcResourceService _plcResourceService;
    private readonly IBaseDataService _baseDataService;
    private readonly IStructDataService _structDataService;
    private readonly IArrDataService _arrDataService;

    public CustomDataForm(
        IPlcResourceService plcResourceService,
        IBaseDataService baseDataService,
        IStructDataService structDataService,
        IArrDataService arrDataService
        )
    {
        InitializeComponent();

        _plcResourceService = plcResourceService;
        _baseDataService = baseDataService;
        _structDataService = structDataService;
        _arrDataService = arrDataService;
    }
    private Dictionary<string, string> _dicCategory = new Dictionary<string, string>();

    private async void CustomDataForm_Load(object sender, EventArgs e)
    {
        _dicCategory.Clear();
        _dicCategory.Add("BASEDATA", "基本类型");
        _dicCategory.Add("STRUCTDATA", "结构类型");
        _dicCategory.Add("ARRDATA", "数组类型");

        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
        treeList1.SetConfigTreeList(true);
        gridView1.SetConfigGridView();
        await RefreshTreeList();
        //分页控件
        paginationControl1.Method = LoadData;
        //按钮
        //RefreshButtonBySelect();
    }
    private async Task LoadData(int page = 1)
    {
        await RefreshGrid(page, paginationControl1.PageSize);
    }

    private List<PlcResource> _treeList = new List<PlcResource>();
    /// <summary>
    /// 刷新左边树
    /// </summary>
    /// <returns></returns>
    private async Task RefreshTreeList()
    {
        _treeList = await _plcResourceService.GetListBySortCodeAsync();
        var eidtNode = CreateEditTreeAll();//转换成有顶级节点的tree
        treeList1.DataSource = null;
        treeList1.DataSource = eidtNode;
        // treeList1.ExpandAll();//展开所有节点
        treeList1.ExpandToLevel(0);
    }
    private List<PlcResource> _plcResources = new List<PlcResource>();
    private async Task RefreshGrid(int currentPage, int pageSize)
    {
        try
        {
            var pageList = await _plcResourceService.Page(new PlcResourcePageInput()
            {
                ParentId = _resourceTree.Id,
                Current = currentPage,
                Size = pageSize
            });
            //自定义变换
            pageList.Records.ForEach(x =>
            {
                string value = "未知";
                _dicCategory.TryGetValue(x.Category, out value);
                x.Category = value;
            });
            gridControl1.DataSource = null;
            gridControl1.DataSource = _plcResources = pageList.Records.ToList();
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

    #region 方法

    /// <summary>
    /// 创建一个包含最上层所有节点
    /// </summary>
    /// <returns></returns>
    private List<EditNode> CreateEditTreeAll()
    {
        List<EditNode> editNodes = new List<EditNode>
        {
            new EditNode() { Id = 19900522, ParentId = 0, Name = "顶级" }
        };
        //找到ParentId = 0 的
        _treeList.ForEach(it =>
        {
            string arr = string.Empty;
            if (it.Category == "ARRDATA")
            {
                arr = $" [{it.ValueLength}]";
            }
            if (it.ParentId == 0)
                editNodes.Add(new EditNode() { Id = it.Id, ParentId = 19900522, Name = $"{it.Title}({it.Code}){arr}" });
            else
                editNodes.Add(new EditNode() { Id = it.Id, ParentId = it.ParentId ?? 0, Name = $"{it.Title}({it.Code}){arr}" });
        });
        return editNodes;
    }
    /// <summary>
    /// 创建一个包含最上层所有节点-去除所有基础数据类型和自己
    /// </summary>
    /// <returns></returns>
    private List<EditNode> CreateEditTreeRemoveBaseDataAndSelf(long selfId = 0)
    {
        List<EditNode> editNodes = new List<EditNode>
        {
            new EditNode() { Id = 19900522, ParentId = 0, Name = "顶级" }
        };
        //找到ParentId = 0 的
        _treeList.ForEach(it =>
        {
            if (it.Category != "BASEDATA" && it.Id != selfId)
            {
                if (it.ParentId == 0)
                    editNodes.Add(new EditNode() { Id = it.Id, ParentId = 19900522, Name = $"{it.Title}({it.Code})" });
                else
                {
                    editNodes.Add(new EditNode() { Id = it.Id, ParentId = it.ParentId ?? 0, Name = $"{it.Title}({it.Code})" });
                }
            }
        });
        return editNodes;
    }
    /// <summary>
    /// 创建一个原有
    /// </summary>
    /// <returns></returns>
    private List<EditNode> CreateEditTree()
    {
        List<EditNode> editNodes = new List<EditNode>
        {
        };
        //找到ParentId = 0 的
        _treeList.ForEach(it =>
        {
            string arr = string.Empty;
            if (it.Category == "ARRDATA")
            {
                arr = $" [{it.ValueLength}]";
            }
            editNodes.Add(new EditNode() { Id = it.Id, ParentId = it.ParentId ?? 0, Name = $"{it.Title}({it.Code}){arr}" });
        });
        return editNodes;
    }

    #endregion

    private PlcResource _resourceTree = null;
    /// <summary>
    /// 结构数据发生变化
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
                _resourceTree = _treeList.Where(it => it.Id == id).FirstOrDefault();
            else
                _resourceTree = new PlcResource { Id = 0 };//查询所有
            //刷新
            await RefreshGrid(1, paginationControl1.PageSize);
        }
    }
    /// <summary>
    /// 显示行号
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
    {
        if (e.Info.IsRowIndicator && e.RowHandle >= 0)
        {
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
    private PlcResource _plcResource = null;
    private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
    {
        var srs = gridView1.GetSelectedRows();
        if (srs.Length > 0)
        {
            //获得选中的行，如果是单选模式，则直接取第一个
            int selectRow = srs[0];
            //从绑定的行数据直接取数据
            _plcResource = _plcResources[selectRow];
        }
        else
        {
            _plcResource = null;
        }
    }

    #region 操作

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var eidtNode = CreateEditTreeRemoveBaseDataAndSelf();//转换成有顶级节点的tree
        var editForm = Native.CreateInstance<CustomDataEditForm>(eidtNode, new PlcResource());
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            //刷新
            await RefreshTreeList();
        }
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (_plcResource != null)
        {
            var edittNode = CreateEditTreeRemoveBaseDataAndSelf(_plcResource.Id);//转换成有顶级节点的tree
            var editForm = Native.CreateInstance<CustomDataEditForm>(edittNode, _plcResource);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                //刷新
                await RefreshTreeList();
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
        if (_plcResource != null)
        {
            if (XtraMessageBox.Show($"确定删除【{_plcResource.Title}-{_plcResource.Code}】？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
            {
                try
                {
                    if (_plcResource.Category == "基本类型")
                        await _baseDataService.Delete(new List<BaseIdInput> { new BaseIdInput { Id = _plcResource.Id } });
                    if (_plcResource.Category == "结构类型")
                        await _structDataService.Delete(new List<BaseIdInput> { new BaseIdInput { Id = _plcResource.Id } });
                    if (_plcResource.Category == "数组类型")
                        await _arrDataService.Delete(new List<BaseIdInput> { new BaseIdInput { Id = _plcResource.Id } });
                    await RefreshTreeList();
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
        await RefreshTreeList();
    }

    #endregion



    #region 按钮管理

    #region 权限相关
    #endregion

    #region 选择相关

    private void RefreshButtonBySelect()
    {
        var buttons = GetControlByForm.FindControls<BarButtonItem>(this);
        foreach (var button in buttons)
        {
            if (button.Tag.ToString() == "")
            {

            }
        }
    }

    #endregion

    #endregion

    #region treeList图标
    private void treeList1_CustomDrawNodeImages(object sender, CustomDrawNodeImagesEventArgs e)
    {
        var id = e.Node.GetDisplayText("Id").ToLong();
        var t = _treeList.FirstOrDefault(it => it.Id == id);

        if (t != null)
        {
            if (t.Id == 19900522)
                e.SelectImageIndex = 0;
            else if (t.Category == "STRUCTDATA")
                e.SelectImageIndex = 1;
            else if (t.Category == "ARRDATA")
                e.SelectImageIndex = 2;
            if (t.Category == "BASEDATA")
            {
                if (t.ValueType == "Bool")
                    e.SelectImageIndex = 3;
                else if (t.ValueType == "Int16")
                    e.SelectImageIndex = 4;
                else if (t.ValueType == "Int32")
                    e.SelectImageIndex = 5;
                else if (t.ValueType == "Float")
                    e.SelectImageIndex = 6;
                else if (t.ValueType == "String")
                    e.SelectImageIndex = 7;
                else if (t.ValueType == "WString")
                    e.SelectImageIndex = 8;
            }
        }
    }
    #endregion

    #region 拖拽操作
    /// <summary>
    /// 拖拽松开
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void treeList1_DragDrop(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.None;
        //业务逻辑
        //1 弹出编辑框，输入结构对应
        if (_dragEnterId == 0)
        {
            XtraMessageBox.Show("没有被拖拽对象", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        if (_dragEnterId == 19900522)
        {
            XtraMessageBox.Show("顶级结构不允许拖拽", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        long dragDropId = 0;

        TreeList treeList = sender as TreeList;
        Point p = new Point(MousePosition.X, MousePosition.Y);
        Point npt = treeList.PointToClient(p);
        TreeListNode node = treeList.CalcHitInfo(npt).Node;
        if (null != node)
        {
            var id = node.GetDisplayText("Id").ToLong();
            //通过Id查找整个对象结构
            dragDropId = id == 19900522 ? 0 : id;
        }
        else
        {
            //没有拷贝的目标
            return;
        }
        try
        {
            //弹窗询问剪切还是复制
            var copyForm = Native.CreateInstance<CopyForm>();
            if (copyForm.ShowDialog() == DialogResult.OK)
            {
                if (copyForm.Rlt == 1)
                {
                    //移动复制
                    await _plcResourceService.Copy(new PlcResourceCopyInput
                    {
                        TargetId = dragDropId,
                        Ids = new List<long> { _dragEnterId },
                        ContainsChild = true,
                    });
                    //刷新
                    await RefreshTreeList();
                }
                if (copyForm.Rlt == 2)
                {
                    //重命名复制
                    //移动复制
                    await _plcResourceService.ChangedNameCopy(new PlcResourceCopyInput
                    {
                        TargetId = dragDropId,
                        Ids = new List<long> { _dragEnterId },
                        ContainsChild = true,
                    });
                    //刷新
                    await RefreshTreeList();
                }
                if (copyForm.Rlt == 3)
                {
                    //剪切
                    await _plcResourceService.Cut(new PlcResourceCopyInput
                    {
                        TargetId = dragDropId,
                        Ids = new List<long> { _dragEnterId },
                        ContainsChild = true,
                    });
                    //刷新
                    await RefreshTreeList();
                }
            }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message, "拖拽异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        _dragEnterId = 0;//置位
    }

    private long _dragEnterId = 0;
    /// <summary>
    /// 拖拽按下
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void treeList1_DragEnter(object sender, DragEventArgs e)
    {
        _dragEnterId = 0;//置位

        TreeList treeList = sender as TreeList;
        TreeListNode node = treeList.FocusedNode;
        if (null != node)
        {
            var id = node.GetDisplayText("Id").ToLong();
            //通过Id查找整个对象结构
            _dragEnterId = id;
        }
    }
    /// <summary>
    /// 拖拽移动中
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void treeList1_DragOver(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.Move;
    }

    #endregion
}