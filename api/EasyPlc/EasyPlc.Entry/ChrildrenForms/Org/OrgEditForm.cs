using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Mapster;
using System.Threading.Tasks;
using static EasyPlc.Entry.ChrildrenForms.Org.OrgForm;

namespace EasyPlc.Entry.ChrildrenForms.Org;

public partial class OrgEditForm : DevExpress.XtraEditors.XtraForm
{
    private readonly ISysOrgService _sysOrgService;
    private readonly IDictService _dictService;
    private readonly SysOrg _sysOrg;
    private readonly List<EditNode> _editNodes;

    public OrgEditForm(
        ISysOrgService sysOrgService,
        IDictService dictService,
        SysOrg sysOrg,
        List<EditNode> editNodes
        )
    {
        InitializeComponent();

        _sysOrgService = sysOrgService;
        _dictService = dictService;
        _sysOrg = sysOrg;
        _editNodes = editNodes;
        //修改标题
        Text = $"组织管理" + "-" + (_sysOrg.Id == 0 ? "新增" : "编辑");
        InitControl();
        InitData();
    }
    private void InitControl()
    {
        treeListLookUpEdit1.Properties.DataSource = _editNodes;
    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    private void InitData()
    {
        if (_sysOrg.Id != 0)
        {
            treeListLookUpEdit1.EditValue = _sysOrg.ParentId == 0 ? 19900522 : _sysOrg.ParentId;
            textEdit1.Text = _sysOrg.Name;
            comboBoxEdit1.Text = _sysOrg.Category == "COMPANY" ? "公司" : "部门";
            trackBarControl1.Value = _sysOrg.SortCode ?? 0;
        }
    }

    private async void btnOk_Click(object sender, EventArgs e)
    {
        var treeId = treeListLookUpEdit1.EditValue.ToLong() == 19900522 ? 0 : treeListLookUpEdit1.EditValue.ToLong();
        var orgName = textEdit1.Text.Trim();
        var category = comboBoxEdit1.Text == "公司" ? "COMPANY" : "DEPT";
        var sortCode = trackBarControl1.Value.ToInt();
        //数据验证 后续补充...
        if (_sysOrg.Id == 0)
        {
            //新增
            try
            {
                await _sysOrgService.Add(new SysOrgAddInput()
                {
                    ParentId = treeId,
                    Name = orgName,
                    Category = category,
                    SortCode = sortCode
                });
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        else
        {
            _sysOrg.ParentId = treeId;
            _sysOrg.Name = orgName;
            _sysOrg.Category = category;
            _sysOrg.SortCode = sortCode;
            var editInput = _sysOrg.Adapt<SysOrgEditInput>();
            //编辑
            try
            {
                await _sysOrgService.Edit(editInput);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        DialogResult = DialogResult.OK;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}