using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using EasyPlc.System;
using DevExpress.XtraEditors;

namespace EasyPlc.Entry.ChrildrenForms.Mac;

public partial class EquipmentForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IMacEquipmentService _macEquipmentService;

    public EquipmentForm(
        IMacEquipmentService macEquipmentService
        )
    {
        InitializeComponent();

        _macEquipmentService = macEquipmentService;
    }

    private async void EquipmentForm_Load(object sender, EventArgs e)
    {
        treeList1.SetConfigTreeList();
        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
        await RefreshTreeList();
    }
  
    private List<MacEquipment> _macEquipmentList = new List<MacEquipment>();
    private async Task RefreshTreeList()
    {
        _macEquipmentList = await _macEquipmentService.GetListBySortCodeAsync();
        //变量替换
        _macEquipmentList.ForEach(it =>
        {
            it.Category = (it.Category == "LINE" ? "产线" : (it.Category == "EQUIPMENT" ? "设备" : "工位"));
        });
        treeList1.DataSource = _macEquipmentList;

        treeList1.RefreshDataSource();//刷新reeList1
        treeList1.ExpandAll();//展开所有节点
    }

    private MacEquipment _macEquipment = null;
    /// <summary>
    /// 选项变化
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
            if (id != 19900522)
                _macEquipment = _macEquipmentList.Where(it => it.Id == id).FirstOrDefault();
            else
                _macEquipment = new MacEquipment() { Id = 0 };//查询所有
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
        _macEquipmentList.ForEach(it =>
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
        _macEquipmentList.ForEach(it =>
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
        var treeAll = CreateEditTreeAll();
        var editForm = Native.CreateInstance<EquipmentEditForm>(treeAll, new MacEquipment());
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
        if (_macEquipment != null)
        {
            var treeAll = CreateEditTreeAll();
            var editForm = Native.CreateInstance<EquipmentEditForm>(treeAll, _macEquipment);
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
        if (_macEquipment != null)
        {
            if (XtraMessageBox.Show($"确定删除【{_macEquipment.Names}】？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
            {
                try
                {
                    await _macEquipmentService.Delete(new List<BaseIdInput> { new BaseIdInput { Id = _macEquipment.Id } });
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
        await RefreshTreeList();
    }
    #endregion
}