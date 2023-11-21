using DevExpress.XtraEditors;
using Furion;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Threading.Tasks;
using EasyPlc.Core.Utils;

namespace EasyPlc.Entry.ChrildrenForms
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly IAuthService _authService;

        public LoginForm(
            IAuthService authService
            )
        {
            InitializeComponent();

            _authService = authService;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUser.Properties.NullValuePrompt = "请输入用户";
            txtPwd.Properties.NullValuePrompt = "请输入密码";
            txtPwd.Properties.PasswordChar = '*';

            if (Debugger.IsAttached)
            {
                txtUser.EditValue = "superAdmin";
                txtPwd.EditValue = "123456";
            }
        }
        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void okBtn_Click(object sender, EventArgs e)
        {
            lbMsg.Text = string.Empty;
            string rlt = await CheckUserPwd();
            if (rlt == "Succeed")
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                lbMsg.Text = rlt;
            }
        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        //密码按下时发生
        private async void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //调用确认
                lbMsg.Text = string.Empty;
                string rlt = await CheckUserPwd();
                if (rlt == "Succeed")
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    lbMsg.Text = rlt;
                }
            }
        }
        private async Task<string> CheckUserPwd()
        {
            string user = txtUser.Text.Trim();
            string pwd = txtPwd.Text.Trim();
            try
            {
                await _authService.Login(new LoginInput { Account = user, Password = CryptogramUtil.Sm2Encrypt(pwd) }, LoginClientTypeEnum.C);
                return "Succeed";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}