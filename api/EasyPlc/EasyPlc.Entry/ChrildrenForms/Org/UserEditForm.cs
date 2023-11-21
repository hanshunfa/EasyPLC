using DevExpress.Mvvm.Native;
using DevExpress.XtraEditors;
using Mapster;
using System.Threading.Tasks;
using EasyPlc.System;
using static EasyPlc.Entry.ChrildrenForms.Org.UserForm;

namespace EasyPlc.Entry.ChrildrenForms.Org;

public partial class UserEditForm : DevExpress.XtraEditors.XtraForm
{
    private readonly ISysUserService _sysUserService;
    private readonly ISysOrgService _sysOrgService;
    private readonly ISysPositionService _positionService;
    private readonly List<EditNode> _orgNodes;
    private readonly SysUser _sysUser;

    public UserEditForm(
        ISysUserService sysUserService,
        ISysOrgService sysOrgService,
        ISysPositionService positionService,
        List<EditNode> orgNodes,
        SysUser sysUser
        )
    {
        InitializeComponent();

        _sysUserService = sysUserService;
        _sysOrgService = sysOrgService;
        _positionService = positionService;
        _orgNodes = orgNodes;
        _sysUser = sysUser;

        //修改标题
        Text = $"用户管理" + "-" + (_sysUser.Id == 0 ? "新增" : "编辑");
        InitEditForm();
        InitData();
    }
    private void InitEditForm()
    {
        treeListLookUpEdit1.Properties.DataSource = _orgNodes;
    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    private void InitData()
    {
        if (_sysUser.Id == 0)
        {
            //新增
        }
        else
        {
            //编辑
            textEdit1.Text = _sysUser.Account;
            textEdit2.Text = _sysUser.Name;
            comboBoxEdit1.Text = _sysUser.Gender;
            textEdit3.Text = _sysUser.Phone;
            textEdit4.Text = _sysUser.Email;
            dateEdit1.Text = _sysUser.Birthday;
            //组织
            treeListLookUpEdit1.EditValue = _sysUser.OrgId;

            textEdit5.Text = _sysUser.EmpNo;
            trackBarControl1.Value = _sysUser.SortCode ?? 99;
            comboBoxEdit2.Text = _sysUser.UserStatus;
        }
    }
    private List<SysPosition> _sysPositions;
    private async Task SetPositionByOrg(long orgId, long positionId = 0)
    {
        if (orgId == 0)
        {
            //新增
        }
        else
        {
            //编辑
            //获取当前用户组织下的所有对象
            _sysPositions = await _positionService.PositionSelector(new PositionSelectorInput { OrgId = orgId });
            //控件设置
            comboBoxEdit3.Properties.Items.Clear();
            comboBoxEdit3.Properties.Items.AddRange(_sysPositions.Select(it => it.Name).ToList());
            //设置当前选项
            var p = _sysPositions.FirstOrDefault(it => it.Id == positionId);
            comboBoxEdit3.EditValue = p?.Name;
        }
    }
    /// <summary>
    /// 选择组织变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void treeListLookUpEdit1_EditValueChanged(object sender, EventArgs e)
    {
        //职位-需要根据组织变化而变化
        SetPositionByOrg(treeListLookUpEdit1.EditValue.ToLong(), _sysUser.PositionId).GetAwaiter();
        //除了第一次，后续都需要手动指定职位
        _sysUser.PositionId = 0;
    }
    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void btnOk_Click(object sender, EventArgs e)
    {
        //提取数据
        _sysUser.Account = textEdit1.Text.Trim();
        _sysUser.Name = textEdit2.Text.Trim();
        _sysUser.Gender = comboBoxEdit1.Text.Trim();
        _sysUser.Phone = textEdit3.Text.Trim();
        _sysUser.Email = textEdit4.Text.Trim();
        _sysUser.Birthday = dateEdit1.Text.Trim();
        _sysUser.OrgId = treeListLookUpEdit1.EditValue.ToLong();
        if (comboBoxEdit3.EditValue != null) _sysUser.PositionId = _sysPositions.First(it => it.Name == comboBoxEdit3.EditValue.ToString()).Id;
        _sysUser.EmpNo = textEdit5.Text.Trim();
        _sysUser.SortCode = trackBarControl1.Value;
        _sysUser.UserStatus = comboBoxEdit2.Text == "启用" ? "ENABLE" : "DISABLED";

        try
        {
            if (_sysUser.Id == 0)
            {
                //新增
                await _sysUserService.Add(_sysUser.Adapt<UserAddInput>());
            }
            else
            {
                await _sysUserService.Edit(_sysUser.Adapt<UserEditInput>());
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