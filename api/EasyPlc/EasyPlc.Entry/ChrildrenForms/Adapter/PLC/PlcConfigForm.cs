using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using EasyPlc.System;
using EasyPlc.Entry.ChrildrenForms.Mac;
using DevExpress.XtraEditors;
using NewLife.Serialization;
using Newtonsoft.Json;
using System.IO;
using Mapster;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class PlcConfigForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IPlcConfigService _plcConfigService;
    public PlcConfigForm(
        IPlcConfigService plcConfigService
        )
    {
        InitializeComponent();

        _plcConfigService = plcConfigService;
    }
    private Dictionary<string, string> _dicCategory = new Dictionary<string, string>();

    private async void PlcConfigForm_Load(object sender, EventArgs e)
    {
        _dicCategory.Clear();
        _dicCategory.Add("PLC", "PLC");
        _dicCategory.Add("CUSTOM-R", "自定义区-读");
        _dicCategory.Add("CUSTOM-W", "自定义区-写");
        _dicCategory.Add("GGQ-R", "公共区-读");
        _dicCategory.Add("GGQ-W", "公共区-写");
        _dicCategory.Add("SJQ-R", "事件区-读");
        _dicCategory.Add("SJQ-W", "事件区-写");

        treeList1.SetConfigTreeList();
        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
        await RefreshTreeList();
    }

    private List<PlcConfig> _plcConfigList = new List<PlcConfig>(); 
    private async Task RefreshTreeList()
    {
        _plcConfigList = await _plcConfigService.GetListBySortCodeAsync();
        //变量替换
        _plcConfigList.ForEach(it =>
        {
            //分类
            string value = "未知";
            _dicCategory.TryGetValue(it.Category, out value);
            it.Category = value;
        });
        treeList1.DataSource = _plcConfigList;
        treeList1.RefreshDataSource();//刷新reeList1
        treeList1.ExpandAll();//展开所有节点
    }
    private List<PlcConfig> _plcConfigs = new List<PlcConfig>();
    private PlcConfig _plcConfig = null;
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
            var plc = _plcConfigList.Where(it => it.Id == id).FirstOrDefault();

            memoEdit1.Text = plc.ExtJson.ConvertJsonString();
            //根据选择内容所有改PLC的所有项
            if(plc.ParentId == 0)
            {
                _plcConfig = plc;
                //PLC
                _plcConfigs.Add(plc);
                _plcConfigs.AddRange(_plcConfigList.Where(it => it.ParentId == plc.Id).OrderBy(it=>it.SortCode).ToList());
            }
            else
            {
                var p = _plcConfigList.Where(it => it.Id == plc.ParentId).FirstOrDefault();
                _plcConfigs.Add(p);
                _plcConfigs.AddRange(_plcConfigList.Where(it => it.ParentId == p.Id).OrderBy(it => it.SortCode).ToList());

                _plcConfig = p;
            }
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
        var treeNodes = CreateEditTreeAll();
        var editForm = Native.CreateInstance<PlcConfigEditForm>(new List<PlcConfig>());
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
        if (_plcConfig != null)
        {
            //通过Id查找PLC
            long plcId = 0;
            if(_plcConfig.ParentId == 0)
            {
                plcId = _plcConfig.Id;
            }
            else
            {
                plcId = _plcConfig.ParentId;
            }
            //查找所有下级
            var plcList = new List<PlcConfig>();
            var plc1 = _plcConfigs.Where(it => it.Id == plcId).FirstOrDefault();
            plcList.Add(plc1);
            plcList.AddRange(_plcConfigs.Where(it => it.ParentId == plcId).ToList());
        
            var treeNodes = CreateEditTreeAll();
            var editForm = Native.CreateInstance<PlcConfigEditForm>(plcList);
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
        if (_plcConfig != null)
        {
            if (XtraMessageBox.Show($"确定删除【{_plcConfig.Names}】？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
            {
                try
                {
                    await _plcConfigService.Delete(new List<BaseIdInput> { new BaseIdInput { Id = _plcConfig.Id } });
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

    #region 方法

    private List<EditNode> CreateEditTreeAll()
    {
        List<EditNode> editNodes = new List<EditNode>
        {
            new EditNode() { Id = 19900522, ParentId = 0, Name = "顶级" }
        };
        //找到ParentId = 0 的
        _plcConfigList.ForEach(it =>
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