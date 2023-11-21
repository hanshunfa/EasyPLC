using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraSplashForm;
using DevExpress.XtraSplashScreen;
using Furion.Logging;
using HslCommunication.Core;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using EasyPlc.Entry.ChrildrenForms;
using System.Globalization;

namespace EasyPlc.Entry
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            #region 设置中文
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
            #endregion

            #region 防止重复打开程序
            //单进程启动
            Process instance = ProcessHelper.RunningInstance();
            if (instance != null)
            {
                MessageBox.Show($"已存在正在运行的【{Application.ProductName}】应用程序，程序将自动切换到原有程序，并显示在桌面最前端。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //把程序放在桌面最前端
                ProcessHelper.HandleRunningInstance(instance);
                Environment.Exit(1);
            }
            #endregion 防止重复打开程序

            //加载动画iiiiiiiiiiiiiiii
            SplashScreenManager.ShowForm(typeof(SplashScreenForm));

            //Fursion
            Serve.RunNative(RunOptions.Default.ConfigureBuilder(builder =>
            {
                builder.WebHost.UseUrls(builder.Configuration["AppSettings:Urls"]);
            }), urls: Serve.IdleHost.Urls);

            #region 全局异常捕获
            //winform全局异常捕获
            ThreadExceptionHandler handler = new ThreadExceptionHandler();
            // 设置没有没捕获的异常在这里强制被捕获
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.Automatic);
            // 注册UI线程异常事件
            Application.ThreadException += handler.Form1_UIThreadException;
            // 注册非UI线程异常事件
            AppDomain.CurrentDomain.UnhandledException += handler.CurrentDomain_UnhandledException;
            #endregion 全局异常捕获

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //关闭加载动画
            SplashScreenManager.CloseForm();

            //LoginForm
            if (Native.CreateInstance<LoginForm>().ShowDialog() == DialogResult.OK)
            {
                //开始加载动画
                SplashScreenManager.ShowForm(typeof(SplashScreenForm));
                //加载主窗体
                Application.Run(Native.CreateInstance<MainForm>());
            }
        }
    }
    /// <summary>
    /// 全局异常处理
    /// </summary>
    internal class ThreadExceptionHandler
    {
        /// <summary>
        /// 捕获UI线程的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="t"></param>
        public void Form1_UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                result = ShowThreadExceptionDialog("Windows Forms UI错误", t.Exception);
            }
            catch
            {
                try
                {
                    MessageBox.Show("严重的错误", "Windows Forms UI错误", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }

            // 点中止时退出程序
            if (result == DialogResult.Abort)
                Application.Exit();
        }
        /// <summary>
        /// 捕获非UI线程的异常,
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                result = ShowThreadExceptionDialog("非UI线程错误", ex);
            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show("严重的非UI线程错误：" + exc.Message, "非UI线程错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
            // 点中止时退出程序
            if (result == DialogResult.Abort)
                Application.Exit();
        }

        /// <summary>
        /// 创建错误信息并显示
        /// </summary>
        /// <param name="title"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            string errorMsg = "应用程序错误，请联系管理员，" + "错误信息:";
            errorMsg = errorMsg + e.Message + ",Stack Trace:" + e.StackTrace;
            // 在这边记下日志，一般情况下我们可以自定义日志 TODO
            //return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);


            Log.Error(e.ToString());

            return DialogResult.OK;
        }
    }
    /// <summary>
    /// 防止重复启动问题，同一个目录下相同程序不允许启动多次，自动切换成已启动程序，并放置在桌面最前端。
    /// </summary>
    internal class ProcessHelper
    {
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //遍历与当前进程名称相同的进程列表  
            foreach (Process process in processes)
            {
                //如果实例已经存在则忽略当前进程  
                if (process.Id != current.Id)
                {
                    //保证要打开的进程同已经存在的进程来自同一文件路径
                    if (current.MainModule.FileName == process.MainModule.FileName)
                    {
                        //返回已经存在的进程
                        return process;
                    }
                }
            }
            return null;
        }

        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);
            SetForegroundWindow(instance.MainWindowHandle);
        }
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;
    }
}
