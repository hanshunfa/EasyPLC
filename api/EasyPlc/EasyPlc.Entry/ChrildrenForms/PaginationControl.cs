using DevExpress.XtraBars;
using System.Threading.Tasks;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class PaginationControl : DevExpress.XtraEditors.XtraUserControl
{
    public PaginationControl()
    {
        InitializeComponent();
    }

    private int _pageSize = 50, _page = 1, _totalPage; //每页记录数、当前页码、总页数
    private bool _isChangeByUser = true;    //是否由用户改变每页记录数或页码，注意何时使用该变量，以及何时赋值
    public delegate Task LoadDataDelegate(int page);    //委托
    private LoadDataDelegate _method;   //加载数据的方法
    /// <summary>
    /// 每页记录数
    /// </summary>
    [Description("每页记录数"), Category("自定义"), DefaultValue(50)]
    public int PageSize
    {
        get { return _pageSize; }
        set
        {
            _pageSize = value;
            _isChangeByUser = false;
            cmbPageSize.EditValue = _pageSize;
            _isChangeByUser = true;
        }
    }
    /// <summary>
    /// 加载数据的方法名称
    /// </summary>
    [Description("加载数据的方法名称"), Category("自定义")]
    public LoadDataDelegate Method { set { _method = value; } }
    //第一页，btnFirst的ItemClick事件
    private void btnFirst_ItemClick(object sender, ItemClickEventArgs e)
    {
        _method(1);
    }
    //上一页，btnPrevious的ItemClick事件
    private void btnPrevious_ItemClick(object sender, ItemClickEventArgs e)
    {
        _method(_page - 1);
    }
    //下一页，btnNext的ItemClick事件
    private void btnNext_ItemClick(object sender, ItemClickEventArgs e)
    {
        _method(_page + 1);
    }
    //最后一页，btnLast的ItemClick事件
    private void btnLast_ItemClick(object sender, ItemClickEventArgs e)
    {
        _method(_totalPage);
    }
    //页码改变，txtPage的EditValueChanged事件
    private void txtPage_EditValueChanged(object sender, EventArgs e)
    {
        if (!_isChangeByUser)
        {
            return;
        }
        if (txtPage.EditValue.ToString() == "")
        {
            _method(1);
            return;
        }
        int page = Convert.ToInt32(txtPage.EditValue);
        if (page < 1)
        {
            _method(1);
        }
        else if (page > _totalPage)
        {
            _method(_totalPage);
        }
        else
        {
            _method(page);
        }
    }
    //每页记录数改变，cmbPageSize的EditValueChanged事件
    private void cmbPageSize_EditValueChanged(object sender, EventArgs e)
    {
        if (_isChangeByUser)
        {
            _pageSize = Convert.ToInt32(cmbPageSize.EditValue);
            _method(1);
        }
    }
    /// <summary>
    /// 设置分页组件内部各控件状态，以及相关文字信息
    /// </summary>
    /// <param name="page">当前页码</param>
    /// <param name="total">总记录数</param>
    public void SetPage(int page, int total)
    {
        _totalPage = (int)Math.Ceiling((double)total / _pageSize);
        if (_totalPage <= 1)
        {
            btnFirst.Enabled = false;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
            txtPage.Enabled = false;
        }
        else if (page == 1)
        {
            btnFirst.Enabled = false;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            txtPage.Enabled = true;
        }
        else if (page == _totalPage)
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
            txtPage.Enabled = true;
        }
        else
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            txtPage.Enabled = true;
        }
        _page = page;
        _isChangeByUser = false;
        txtPage.EditValue = page;
        _isChangeByUser = true;
        if (total == 0)
        {
            lblSummary.Caption = "没有记录";
        }
        else
        {
            lblSummary.Caption = $"共 {_totalPage:N0} 页　第 {(_pageSize * (page - 1) + 1):N0} 到 {(page == _totalPage ? total : _pageSize * page):N0} 条　共 {total:N0} 条";
        }
    }
}
