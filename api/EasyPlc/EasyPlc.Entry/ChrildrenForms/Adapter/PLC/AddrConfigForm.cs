using DevExpress.XtraEditors;
using DevExpress.XtraScheduler.Native;
using DevExpress.XtraScheduler.UI;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using NewLife.Caching.Clusters;
using System.Collections;
using System.Diagnostics;
using EasyPlc.Entry.ChrildrenForms.Adapter.PLC;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class AddrConfigForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IPlcConfigService _plcConfigService;
    private readonly IPlcResourceService _plcResourceService;
    private readonly IAddressService _addressService;
    private readonly IGenbasicService _genbasicService;

    public AddrConfigForm(
        IPlcConfigService plcConfigService,
        IPlcResourceService plcResourceService,
        IAddressService addressService,
        IGenbasicService genbasicService
        )
    {
        InitializeComponent();

        _plcConfigService = plcConfigService;
        _plcResourceService = plcResourceService;
        _addressService = addressService;
        _genbasicService = genbasicService;
    }

    private async void AddrConfigForm_Load(object sender, EventArgs e)
    {
        //设置TreeList
        treeList1.SetConfigTreeList();
        treeList2.SetConfigTreeList(true, true);
        treeList3.SetConfigTreeList(true);
        treeList4.SetConfigTreeList();

        //初始化TreeList
        await RefreshTreeList1();
        await RefreshTreeList3();

        //初始化 ribbonControl
        ribbonControl1.SetControlStyle();
        if (!Debugger.IsAttached)
        {
            ribbonPageGroup2.Visible = false;
        }
    }
    private List<PlcConfig> _plcConfigList;
    /// <summary>
    /// 刷新PLC列表数据
    /// </summary>
    /// <returns></returns>
    private async Task RefreshTreeList1()
    {
        _plcConfigList = await _plcConfigService.GetListBySortCodeAsync();
        //变量替换
        _plcConfigList.ForEach(it =>
        {
            //名称显示更加完善
            if (it.Category == "PLC")
            {
                var jsonObj = it.ExtJson.ToObject<PlcExtJson>();
                it.Name = $"{it.Name} ({jsonObj.Type}-{jsonObj.Version}-{jsonObj.Ip})";
            }
            else
            {
                var jsonObj = it.ExtJson.ToObject<AddrExtJson>();
                it.Name = $"{it.Name} ({jsonObj.StartAddr})";
            }
        });
        treeList1.DataSource = _plcConfigList;

        treeList1.RefreshDataSource();//刷新reeList1
        treeList1.ExpandAll();//展开所有节点
    }

    //private 

    private List<PlcResource> _resourceList = new List<PlcResource>();
    private async Task RefreshTreeList3()
    {
        _resourceList = await _plcResourceService.GetListBySortCodeAsync();
        var nodeList = CreateEditTree(_resourceList);
        treeList3.DataSource = null;
        treeList3.DataSource = nodeList;
        //treeList3.ExpandAll();//展开所有节点
    }
    private void treeList2_CustomDrawNodeImages(object sender, CustomDrawNodeImagesEventArgs e)
    {
        var id = e.Node.GetDisplayText("Id").ToLong();
        var t = _targetResourceList.FirstOrDefault(it => it.Id == id);

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
    private void treeList3_CustomDrawNodeImages(object sender, DevExpress.XtraTreeList.CustomDrawNodeImagesEventArgs e)
    {
        var id = e.Node.GetDisplayText("Id").ToLong();
        var t = _resourceList.FirstOrDefault(it => it.Id == id);

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

    #region 方法
    /// <summary>
    /// 创建一个原有
    /// </summary>
    /// <returns></returns>
    private List<EditNode> CreateEditTree(List<PlcResource> resources)
    {
        List<EditNode> editNodes = new List<EditNode>
        {
        };
        //找到ParentId = 0 的
        resources.ForEach(it =>
        {
            string arr = string.Empty;
            if (it.Category == "ARRDATA")
            {
                arr = $" [{it.ValueLength}]";
            }
            editNodes.Add(new EditNode()
            {
                Id = it.Id,
                ParentId = it.ParentId ?? 0,
                Name = $"{it.Title}({it.Code}){arr}",
                Addr = it.StartAdrr
            });
        });
        return editNodes;
    }
    #endregion

    private PlcConfig _plcConfig = null;
    /// <summary>
    /// PLC选项发送变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
    {
        TreeList workSpaceTree = sender as TreeList;
        TreeListNode node = workSpaceTree.FocusedNode;
        if (null != node)
        {
            var id = node.GetDisplayText("Id").ToLong();
            _plcConfig = _plcConfigList.Where(it => it.Id == id).FirstOrDefault();
            //名称显示更加完善
            string addr = string.Empty;
            if (_plcConfig.Category == "PLC")
            {
                var jsonObj = _plcConfig.ExtJson.ToObject<PlcExtJson>();
                addr = $"{jsonObj.Type}-{jsonObj.Version}-{jsonObj.Ip}";
            }
            else
            {
                var jsonObj = _plcConfig.ExtJson.ToObject<AddrExtJson>();
                addr = jsonObj.StartAddr;
            }
            //标题
            labelControl1.Text = $"已选:{_plcConfig.Names} ({addr})";

            //地址配置树切换
            await RefreshTreeList2();
        }
    }

    List<PlcResource> _targetResourceList = new List<PlcResource>();
    /// <summary>
    /// 刷新事件树
    /// </summary>
    /// <returns></returns>
    private async Task RefreshTreeList2()
    {
        if (_plcConfig != null)
        {
            _targetResourceList = await _addressService.GetResourceListByPlcId(_plcConfig.Id);
            var nodes = CreateEditTree(_targetResourceList);

            treeList2.DataSource = null;
            treeList2.DataSource = nodes;
            //treeList2.ExpandAll();
        }
    }

    #region 拖拽结构数据到地址配置

    /// <summary>
    /// 结构数据拖拽到事件配置树中
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void treeList2_DragDrop(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.None;
        if (_parentResourceId != 0)
        {
            //业务逻辑
            //1 弹出编辑框，输入结构对应
            if (_parentResourceId == 0 && _plcConfig != null)
            {
                labelControl2.ForeColor = Color.Red;
                labelControl2.Text = "没有拖拽对象";
                return;
            }

            try
            {
                //新增
                await _addressService.Add(new AddressAddInput
                {
                    PlcId = _plcConfig.Id,
                    ResourceId = _parentResourceId,
                });

                await RefreshTreeList2();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "新增错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _parentResourceId = 0;//置位
        }
        if (_addrId != 0)
        {
            TreeList treeList = sender as TreeList;
            Point p = new Point(MousePosition.X, MousePosition.Y);
            Point npt = treeList.PointToClient(p);
            TreeListNode node = treeList.CalcHitInfo(npt).Node;
            if (null != node)
            {
                var id = node.GetDisplayText("Id").ToLong();
                var topId = GetTopaId(_targetResourceList, id);
                try
                {
                    var moveForm = Native.CreateInstance<MoveForm>();
                    if (moveForm.ShowDialog() == DialogResult.OK)
                    {
                        var addr1 = await _addressService.GetAddressById(_addrId);//拖拽对象的
                        var addr2 = await _addressService.GetAddressById(topId);//目标对象
                        if (moveForm.Rlt == 1)
                        {
                            //上方
                            await _addressService.Sort(new AddressSortInput
                            {
                                Columns = new List<SortColumn>() {
                                    new SortColumn()
                                    {
                                        PlcId = addr1.Id,
                                        Sort = addr2.SortCode.Value -1
                                    }
                                }
                            });
                        }
                        else if (moveForm.Rlt == 2)
                        {
                            //下方
                            await _addressService.Sort(new AddressSortInput
                            {
                                Columns = new List<SortColumn>() {
                                    new SortColumn()
                                    {
                                        PlcId = addr1.Id,
                                        Sort = addr2.SortCode.Value + 1
                                    }
                                }
                            });
                        }
                        else if (moveForm.Rlt == 3)
                        {
                            //对换
                            await _addressService.Sort(new AddressSortInput
                            {
                                Columns = new List<SortColumn>() {
                                    new SortColumn()
                                    {
                                        PlcId = addr1.Id,
                                        Sort = addr2.SortCode.Value
                                    },
                                    new SortColumn()
                                    {
                                        PlcId = addr2.Id,
                                        Sort = addr1.SortCode.Value
                                    }
                                }
                            });
                        }
                        else
                        {
                            //取消
                            return;
                        }
                    }

                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "拖拽异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            await RefreshTreeList2();
            _addrId = 0;//置位
        }
    }
    private long _addrId;
    private void treeList2_DragEnter(object sender, DragEventArgs e)
    {
        if (_parentResourceId == 0)
        {
            //没有外部移动对象情况下
            TreeList treeList = sender as TreeList;
            TreeListNode node = treeList.FocusedNode;
            if (null != node)
            {
                var id = node.GetDisplayText("Id").ToLong();
                _addrId = GetTopaId(_targetResourceList, id);
            }
        }
    }

    private void treeList2_DragOver(object sender, DragEventArgs e)
    {
        if (_parentResourceId != 0 || _addrId != 0)
            e.Effect = DragDropEffects.Move;
        else
            e.Effect = DragDropEffects.None;
    }

    private void treeList3_DragDrop(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.None;
        _parentResourceId = 0;//置位

        _addrId = 0;
    }

    private long _parentResourceId = 0;
    /// <summary>
    /// 拖拽起始对象
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void treeList3_DragEnter(object sender, DragEventArgs e)
    {
        _parentResourceId = 0;//置位

        TreeList treeList = sender as TreeList;
        TreeListNode node = treeList.FocusedNode;
        if (null != node)
        {
            var id = node.GetDisplayText("Id").ToLong();
            //通过Id查找整个对象结构
            _parentResourceId = GetTopaParentId(_resourceList, id);
        }
    }
    private void treeList3_DragOver(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.None;
    }

    #endregion

    #region 方法

    private long GetTopaParentId(List<PlcResource> resources, long id)
    {
        var pr = resources.Find(x => x.Id == id);
        if (pr != null)
        {
            if (pr.ParentId == 0)
            {
                return pr.Id;
            }
            else
            {
                return GetTopaParentId(resources, pr.ParentId.Value);
            }
        }
        else
            return id;
    }

    private long GetTopaId(List<PlcResource> resources, long id)
    {
        var pr = resources.Find(x => x.Id == id);
        if (pr != null)
        {
            return GetTopaId(resources, pr.ParentId.Value);
        }
        else
            return id;
    }

    #endregion

    private PlcAddress _plcAddress = null;
    private async void treeList2_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
    {
        TreeList workSpaceTree = sender as TreeList;
        TreeListNode node = workSpaceTree.FocusedNode;
        if (null != node)
        {
            var id = node.GetDisplayText("Id").ToLong();
            var topaId = GetTopaId(_targetResourceList, id);
            _plcAddress = await _addressService.GetAddressById(topaId);
            if (_plcAddress != null)
            {
                var resource = _targetResourceList.Where(it => it.Id == id).FirstOrDefault();
                //标题
                labelControl2.Text = $"已选:ID[{_plcAddress.Id}]地址-{resource.Title}({resource.Code})";
            }
        }
    }

    #region 操作
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (_plcAddress != null)
        {
            if (XtraMessageBox.Show($"确定删除【{_plcAddress.Id}】？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
            {
                try
                {
                    await _addressService.Delete(new List<BaseIdInput> {
                        new BaseIdInput { Id = _plcAddress.Id }
                    });
                    await RefreshTreeList2();
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
        //初始化TreeList
        await RefreshTreeList1();
        await RefreshTreeList3();
    }

    #endregion

    #region 代码生成
    private async void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        try
        {
            //生成实列对象
            await _genbasicService.ExecGenObjDefinedPro();
            //生成PLC关系
            await _genbasicService.ExecGenSiemensPlcInfoPro();
            XtraMessageBox.Show($"代码生成成功，重新编译生效", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show($"代码生成异常:{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    #endregion
}