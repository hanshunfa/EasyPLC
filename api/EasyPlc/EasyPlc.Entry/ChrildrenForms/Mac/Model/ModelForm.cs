using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.XtraEditors;
using EasyPlc.System;

namespace EasyPlc.Entry.ChrildrenForms.Mac;

public partial class ModelForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IMacModelService _macModelService;
    private readonly IMacModelParamService _macParameterService;

    public ModelForm(
        IMacModelService macModelService,
        IMacModelParamService macParameterService
        )
    {
        InitializeComponent();

        _macModelService = macModelService;
        _macParameterService = macParameterService;
    }

    private async void ModelForm_Load(object sender, EventArgs e)
    {
        treeList1.SetConfigTreeList();
        await RefreshTreeList();
        gridView1.SetConfigGridView();
        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
        //分页控件
        paginationControl1.Method = LoadData;
    }
    private async Task LoadData(int page = 1)
    {
        await RefreshGrid(page, paginationControl1.PageSize);
    }
 
  
    private List<MacModel> _macModels = new List<MacModel>();
    private async Task RefreshTreeList()
    {
        _macModels = await _macModelService.GetListBySortCodeAsync();
        //替换
        _macModels.ForEach(x =>
        {
            x.Category = x.Category == "MODEL_MODEL" ? "型号" : "分类";
        });
        treeList1.DataSource = null;
        treeList1.DataSource = _macModels;
        treeList1.ExpandAll();//展开所有节点
    }
    private List<MacModelParam> _macParameters = new List<MacModelParam>();
    private async Task RefreshGrid(int currentPage, int pageSize)
    {
        try
        {
            if (_macModel == null)
            {
                gridControl1.DataSource = null;
                paginationControl1.SetPage(1, 0);
                return;
            }
            //只有具体的型号才有参数
            if (_macModel.Category == "型号")
            {
                var pageList = await _macParameterService.Page(new ParameterPageInput()
                {
                    ModelId = _macModel.Id,
                    Current = currentPage,
                    Size = pageSize
                });
                //自定义变换
                pageList.Records.ForEach(it =>
                {
                    it.Category = it.Category == "PARAMETER" ? "参数" : "未定义";
                    it.ParamType = it.ParamType == "PARM_STRING" ? "string" : (it.ParamType == "PARM_INT16" ? "int16" : (it.ParamType == "PARM_INT32" ? "int32" : "float"));
                });
                gridControl1.DataSource = null;
                gridControl1.DataSource = _macParameters = pageList.Records.ToList();
                paginationControl1.SetPage(pageList.Current, pageList.Total);
            }
            else
            {
                _macParameters.Clear();
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

    private MacModel _macModel = null;
    private async void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
    {
        TreeList tree = sender as TreeList;
        TreeListNode node = tree.FocusedNode;
        if (null != node)
        {
            var id = node.GetDisplayText("Id").ToLong();
            _macModel = _macModels.Where(it => it.Id == id).FirstOrDefault();
        }
        //跟新右侧按钮
        await RefreshGrid(1, paginationControl1.PageSize);

    }
    private MacModelParam _macParameter = null;
    private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
    {
        var srs = gridView1.GetSelectedRows();
        if (srs.Length > 0)
        {
            //获得选中的行，如果是单选模式，则直接取第一个
            int selectRow = srs[0];
            //从绑定的行数据直接取数据
            _macParameter = _macParameters[selectRow];
        }
        else
        {
            _macParameter = null;
        }
    }
    private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
    {
        if (e.Info.IsRowIndicator && e.RowHandle >= 0)
        {
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
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
            new EditNode() { Id = 19900522, ParentId = 0, Name = "顶级" }
        };
        //找到ParentId = 0 的
        _macModels.ForEach(it =>
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
        _macModels.ForEach(it =>
        {
            editNodes.Add(new EditNode() { Id = it.Id, ParentId = it.ParentId, Name = it.Name });
        });
        return editNodes;
    }
    #endregion

    #region 操作

    #region 型号

    /// <summary>
    /// 型号-新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var treeAll = CreateEditTreeAll();
        var editForm = Native.CreateInstance<ModelEditForm>(treeAll, new MacModel());
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            //刷新
            await RefreshTreeList();
        }
    }
    /// <summary>
    /// 型号-编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (_macModel != null)
        {
            var treeAll = CreateEditTreeAll();
            var editForm = Native.CreateInstance<ModelEditForm>(treeAll, _macModel);
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
    /// 型号-删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (_macModel != null)
        {
            if (XtraMessageBox.Show($"确定删除【{_macModel.Names}】？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
            {
                try
                {
                    await _macModelService.Delete(new List<BaseIdInput> { new BaseIdInput { Id = _macModel.Id } });
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
    /// 型号-刷新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        await RefreshTreeList();
    }
    #endregion


    #region 参数

    /// <summary>
    /// 参数-新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var treeAll = CreateEditTree();
        var editForm = Native.CreateInstance<ParameterEditForm>(treeAll, new MacModelParam());
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            //刷新
            await RefreshGrid(1, paginationControl1.PageSize);
        }
    }
    /// <summary>
    /// 参数-编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var treeAll = CreateEditTree();
        var editForm = Native.CreateInstance<ParameterEditForm>(treeAll, _macParameter);
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            //刷新
            await RefreshGrid(1, paginationControl1.PageSize);
        }
    }
    /// <summary>
    /// 参数-删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (_macParameter != null)
        {
            if (XtraMessageBox.Show($"确定删除【{_macParameter.Name}】？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
            {
                try
                {
                    await _macParameterService.Delete(new List<BaseIdInput> { new BaseIdInput { Id = _macParameter.Id } });
                    await RefreshGrid(1, paginationControl1.PageSize);
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
    /// 参数-刷新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        await RefreshGrid(1, paginationControl1.PageSize);
    }

    /// <summary>
    /// 参数批量复制
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (_macModel != null)
        {
            var treeAll = CreateEditTreeAll();
            var editForm = Native.CreateInstance<ModelCopyForm>(treeAll, _macModel);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                //刷新
                await RefreshGrid(1, paginationControl1.PageSize);
            }
        }
        else
        {
            XtraMessageBox.Show("请选择需要拷贝行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    #endregion

    #endregion


}