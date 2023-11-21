using DevExpress.XtraEditors;
using Mapster;
using EasyPlc.System;
using static EasyPlc.Entry.ChrildrenForms.Role.RoleForm;

namespace EasyPlc.Entry.ChrildrenForms.Role;

public partial class RoleEditForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IRoleService _roleService;
    private readonly List<EditNode> _orgNodes;
    private readonly SysRole _sysRole;
    public RoleEditForm(
        IRoleService roleService,
        List<EditNode> orgNodes,
        SysRole sysRole
        )
    {
        InitializeComponent();

        _roleService = roleService;
        _orgNodes = orgNodes;
        _sysRole = sysRole;
        //修改标题
        Text = $"角色管理" + "-" + (_sysRole.Id == 0 ? "新增" : "编辑");

        InitEditForm();
        InitData();
    }
    private List<RadioEditValue> _radioEditValues = new List<RadioEditValue>();
    private void InitEditForm()
    {
        treeListLookUpEdit1.Properties.DataSource = _orgNodes;

        _radioEditValues.Clear();
        _radioEditValues.Add(new RadioEditValue()
        {
            Level = 5,
            Title = "全部",
            ScopeCategory = "SCOPE_ALL"
        });
        _radioEditValues.Add(new RadioEditValue()
        {
            Level = 1,
            Title = "仅自己",
            ScopeCategory = "SCOPE_SELF"
        });
        _radioEditValues.Add(new RadioEditValue()
        {
            Level = 2,
            Title = "所属组织",
            ScopeCategory = "SCOPE_ORG"
        });
        _radioEditValues.Add(new RadioEditValue()
        {
            Level = 4,
            Title = "所属组织及以下",
            ScopeCategory = "SCOPE_ORG_CHILD"
        });
    }
    private void InitData()
    {
        if (_sysRole.Id != 0)
        {
            textEdit1.Text = _sysRole.Name;
            comboBoxEdit1.Text = _sysRole.Category;
            treeListLookUpEdit1.EditValue = _sysRole.OrgId;
            radioGroup1.EditValue = _radioEditValues.First(it => it.Level == _sysRole.DefaultDataScope.Level).Title;
            trackBarControl1.Value = _sysRole.SortCode ?? 99;
        }
    }
    private bool _isG = true;
    /// <summary>
    /// 类型发生变化 若是机构类型 需要输入组织
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboBoxEdit1.SelectedIndex == 0)
        {
            //全局
            labelControl5.Visible = false;
            treeListLookUpEdit1.Visible = false;
            _isG = true;
        }
        else
        {
            //机构
            labelControl5.Visible = true;
            treeListLookUpEdit1.Visible = true;
            _isG = false;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void btnOk_Click(object sender, EventArgs e)
    {
        _sysRole.Name = textEdit1.Text;
        _sysRole.Category = comboBoxEdit1.Text == "全局" ? "GLOBAL" : "ORG";
        _sysRole.DefaultDataScope = _radioEditValues.First(it => it.Title == radioGroup1.EditValue.ToString()).Adapt<DefaultDataScope>();
        _sysRole.SortCode = trackBarControl1.Value;
        if (!_isG)//不是全局需要指定组织
        {
            if (treeListLookUpEdit1.EditValue.ToLong() == 0)
            {
                XtraMessageBox.Show("请选择组织", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var treeId = treeListLookUpEdit1.EditValue.ToLong();
            _sysRole.OrgId = treeId;
        }
        else
        {
            _sysRole.OrgId = null;
        }
        //上报
        try
        {
            if (_sysRole.Id == 0)
            {
                await _roleService.Add(_sysRole.Adapt<RoleAddInput>());
            }
            else
            {
                await _roleService.Edit(_sysRole.Adapt<RoleEditInput>());
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