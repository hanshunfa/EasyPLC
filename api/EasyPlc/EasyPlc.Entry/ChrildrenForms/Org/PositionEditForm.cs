using DevExpress.XtraEditors;
using Mapster;

namespace EasyPlc.Entry.ChrildrenForms.Org;

public partial class PositionEditForm : DevExpress.XtraEditors.XtraForm
{
    private readonly ISysPositionService _sysPositionService;
    private readonly SysPosition _sysPosition;
    private readonly List<EditNode> _editNodes;
    public PositionEditForm(
         ISysPositionService sysPositionService,
         SysPosition sysPosition,
         List<EditNode> editNodes
        )
    {
        InitializeComponent();

        _sysPositionService = sysPositionService;
        _sysPosition = sysPosition;
        _editNodes = editNodes;

        //修改标题
        Text = $"职位管理" + "-" + (_sysPosition.Id == 0 ? "新增" : "编辑");
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
        if (_sysPosition.Id != 0)
        {
            treeListLookUpEdit1.EditValue = _sysPosition.OrgId;
            textEdit1.Text = _sysPosition.Name;
            comboBoxEdit1.Text = _sysPosition.Category;
            trackBarControl1.Value = _sysPosition.SortCode ?? 0;
        }
    }

    private async void btnOk_Click(object sender, EventArgs e)
    {
        var treeId = treeListLookUpEdit1.EditValue.ToLong();
        var orgName = textEdit1.Text.Trim();
        var category = comboBoxEdit1.Text == "高层" ? "HIGH" : (comboBoxEdit1.Text == "中层" ? "MIDDLE" : "LOW");
        var sortCode = trackBarControl1.Value.ToInt();
        //数据验证 后续补充...
        if (_sysPosition.Id == 0)
        {
            //新增
            try
            {
                await _sysPositionService.Add(new PositionAddInput()
                {
                    OrgId = treeId,
                    Name = orgName,
                    Category = category,
                    SortCode = sortCode
                });
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        else
        {
            _sysPosition.OrgId = treeId;
            _sysPosition.Name = orgName;
            _sysPosition.Category = category;
            _sysPosition.SortCode = sortCode;
            var editInput = _sysPosition.Adapt<PositionEditInput>();
            //编辑
            try
            {
                await _sysPositionService.Edit(editInput);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        DialogResult = DialogResult.OK;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}