using DevExpress.XtraEditors;
using Mapster;
using EasyPlc.System;
using static EasyPlc.Entry.ChrildrenForms.Resoure.Menu.MenuForm;

namespace EasyPlc.Entry.ChrildrenForms.Resoure.Menu;

public partial class MenuEditForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IMenuService _menuService;
    private readonly List<EditNode> _menuNodes;
    private readonly SysResource _menu;

    public MenuEditForm(
        IMenuService menuService,
        List<EditNode> menuNodes,
        SysResource menu
        )
    {
        InitializeComponent();

        _menuService = menuService;
        _menuNodes = menuNodes;
        _menu = menu;

        //修改标题
        Text = $"菜单管理" + "-" + (_menu.Id == 0 ? "新增" : "编辑");
        InitEditForm();
        InitData();
    }
    private void InitEditForm()
    {
        treeListLookUpEdit1.Properties.DataSource = _menuNodes;
    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    private void InitData()
    {
        if (_menu.Id == 0)
        {
            //新增
        }
        else
        {
            //编辑
            textEdit1.Text = _menu.Title;
            radioGroup1.EditValue = _menu.MenuType;
            //上级菜单
            treeListLookUpEdit1.EditValue = _menu.ParentId;
            trackBarControl1.Value = _menu.SortCode ?? 99;
        }
    }
    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void btnOk_Click(object sender, EventArgs e)
    {
        //获取数据
        _menu.Title = textEdit1.Text.Trim();
        _menu.MenuType = radioGroup1.EditValue.ToString() == "目录" ? "CATALOG" : "MENU";
        _menu.ParentId = treeListLookUpEdit1.EditValue.ToLong();
        _menu.SortCode = trackBarControl1.Value;

        if (_menu.Id == 0)
        {
            //当前项目不需要，但是系统需要传入的数据
            //1 模块
            _menu.Module = 212725263003721;
            //2 路径
            _menu.Path = "/kstopa";
            //3 图标
            _menu.Icon = "kstopa";
        }
        try
        {
            if (_menu.Id == 0)
            {
                //新增
                await _menuService.Add(_menu.Adapt<MenuAddInput>());
            }
            else
            {
                //编辑
                await _menuService.Edit(_menu.Adapt<MenuEditInput>());
            }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        DialogResult = DialogResult.OK;
    }
    /// <summary>
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}