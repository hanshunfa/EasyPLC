using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Furion.FriendlyException;
using System.Threading.Tasks;
using EasyPlc.Core.UserInfo;
using EasyPlc.Entry.ChrildrenForms.Mac;
using EasyPlc.Entry.ChrildrenForms.Mac.Carrier;
using EasyPlc.Entry.ChrildrenForms.Org;
using EasyPlc.Entry.ChrildrenForms.Resoure.Menu;
using EasyPlc.Entry.ChrildrenForms.Role;
using EasyPlc.Plugin.Scan;
using EasyPlc.Plugin.ScrewGun;
using EasyPlc.Plugin.SygoleRFID;

namespace EasyPlc.Entry
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region 初始化
        private readonly IAuthService _authService;
        private readonly ISygoleFactoryService _sygoleFactoryService;
        private readonly IKwScrewGunFactoryService _kwScrewGunFactoryService;
        private readonly IFixedScanFactoryService _fixedScanFactoryService;
        private readonly ISiemensPlcFactoryService _siemensPlcFactoryService;
        private readonly IProProcessService _proProcessService;

        //变量
        private LoginUserOutput _loginUser;

        public MainForm(
            IAuthService authService,
            ISygoleFactoryService sygoleFactoryService,
            IKwScrewGunFactoryService kwScrewGunFactoryService,
            IFixedScanFactoryService fixedScanFactoryService,
            ISiemensPlcFactoryService siemensPlcFactoryService,
            IProProcessService proProcessService
            )
        {
            //IOC构造
            _authService = authService;
            _sygoleFactoryService = sygoleFactoryService;
            _kwScrewGunFactoryService = kwScrewGunFactoryService;
            _fixedScanFactoryService = fixedScanFactoryService;
            _siemensPlcFactoryService = siemensPlcFactoryService;
            _proProcessService = proProcessService;

            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            GetLoginUser();

            //初始化启动服务
            //01 RFID
            //await _sygoleFactoryService.InitFactory();//该项目不使用
            //02 螺丝枪
            //await _kwScrewGunFactoryService.StartTcpServer();//启动螺丝枪服务器
            //03 新大陆扫码器
            //await _fixedScanFactoryService.InitFactory();
            //初始化工单数据
            await _proProcessService.GetWorkingOrderInfo();
            //最后PLC
            _siemensPlcFactoryService.InitFactory();//初始化工厂
            //_siemensPlcFactoryService.StartPLC();//连接所有PLC,测试时候先不用

            WorkingProcess workingProcess = new WorkingProcess();//创建过程处理对象
            workingProcess.Init();

            ribbonControl1.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Never;
            //关闭子窗体触发
            tabbedView1.DocumentClosing += DocumentCancelEventHandler;
            timer1.Start();

            //关闭加载动画 所有都加载完成
            SplashScreenManager.CloseForm();

            //加载初始化子窗体
            LoadInitChildrenForm();
        }
        #endregion

        #region 子窗体
        /// <summary>
        /// 关闭子窗体，欢迎界面不允许关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DocumentCancelEventHandler(object sender, DocumentCancelEventArgs e)
        {
            if (e.Document.Caption == "欢迎界面") { e.Cancel = true; } else { e.Cancel = false; }
        }

        private void LoadInitChildrenForm()
        {
            SetChildrenForm(Native.CreateInstance<WelcomeForm>());
            //第一次直接进入工单页面
            NavigateForm<OrderForm>();
        }

        #endregion

        #region 用户

        /// <summary>
        /// 获取登入用户
        /// </summary>
        /// <returns></returns>
        private void GetLoginUser()
        {
            _loginUser = _authService.GetLoginUser().Result;
            if (_loginUser != null)
            {
                bStaticILoginUser.Caption = _loginUser.Account;
            }
        }

        #endregion

        #region 用户操作
        /// <summary>
        /// 切换用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // throw Oops.Bah("aaaaa");
        }
        #endregion

        #region 菜单

        #region 系统菜单
        /// <summary>
        /// 组织菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bBtnIOrg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<OrgForm>();
        }
        /// <summary>
        /// 用户管理菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<UserForm>();
        }
        /// <summary>
        /// 职位管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<PositionForm>();
        }
        /// <summary>
        /// 角色管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<RoleForm>();
        }
        /// <summary>
        /// 模块管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem29_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<ModuleForm>();
        }
        /// <summary>
        /// 菜单管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<MenuForm>();
        }

        #endregion

        #region 设备管理菜单
        /// <summary>
        /// 设备管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<EquipmentForm>();
        }
        /// <summary>
        /// 载具管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<CarrierForm>();
        }
        /// <summary>
        /// 工装管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        /// <summary>
        /// 型号管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<ModelForm>();
        }
        /// <summary>
        /// 工艺路线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<FlowForm>();
        }



        #endregion

        #region 外设管理菜单

        /// <summary>
        /// PLC工厂
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<PlcFactoryForm>();
        }
        /// <summary>
        /// PLC-创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<PlcConfigForm>();
        }
        /// <summary>
        /// PLC-结构定义
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<CustomDataForm>();
        }
        /// <summary>
        /// PLC-地址配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<AddrConfigForm>();
        }

        /// <summary>
        /// RFID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<SygoleRFIDForm>();
        }
        /// <summary>
        /// 螺丝枪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<ScrewGunForm>();
        }
        /// <summary>
        /// 新大陆扫码器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<FixedScanForm>();
        }
        #endregion

        #region 工单

        /// <summary>
        /// 工单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<OrderForm>();
        }
        /// <summary>
        /// 激光镭射
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<PreviewLaserForm>();
        }
        /// <summary>
        /// 标签打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<PreviewLabelForm>();
        }
        /// <summary>
        /// 加工过程监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem25_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<WorkingStepForm>();
        }
        /// <summary>
        /// 加工中产品数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<DataTmpForm>();
        }
        /// <summary>
        /// 数据查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem27_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigateForm<DataForm>();
        }

        #endregion 

        #endregion

        #region 定时器 500ms
        private void timer1_Tick(object sender, EventArgs e)
        {
            barStaticItem2.Caption = DateTime.Now.ToString("G");
        }
        #endregion

        #region 退出
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (XtraMessageBox.Show("确定退出应用程序?", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region 方法
        private bool HasChildrenForm(string formName)
        {
            var cf = MdiChildren.FirstOrDefault(it => it.Name == formName);
            if (cf != null)
            {
                //切换到指定tab
                tabbedView1.ActivateDocument(cf);
                return true;
            }
            return false;
        }

        private void SetChildrenForm(XtraForm xtraForm)
        {
            xtraForm.MdiParent = this;
            xtraForm.Show();
        }
        


        /// <summary>
        /// 导航到指定类型窗体
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        private void NavigateForm<TWindow>() where TWindow : XtraForm
        {
            SplashScreenManager.ShowForm(typeof(WaitForm));
            //判断是否存在
            if (!HasChildrenForm(typeof(TWindow).Name)) SetChildrenForm(Native.CreateInstance<TWindow>());
            SplashScreenManager.CloseForm();
        }
        #endregion
    }
}
