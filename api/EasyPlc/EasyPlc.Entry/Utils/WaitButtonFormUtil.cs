

using DevExpress.XtraSplashScreen;

namespace EasyPlc.Entry.Utils
{
    internal class WaitButtonFormUtil
    {
        /// <summary>
        /// 设置等待窗口的标题
        /// </summary>
        /// <returns></returns>
        public static void SetCaption(string caption)
        {
            if (SplashScreenManager.Default != null && !string.IsNullOrEmpty(caption))
            {
                SplashScreenManager.Default.SetWaitFormCaption(caption);
            }
        }

        /// <summary>
        /// 设置等待窗口的描述文字
        /// </summary>
        /// <returns></returns>
        public static void SetDescription(string description)
        {
            if (SplashScreenManager.Default != null && !string.IsNullOrEmpty(description))
            {
                SplashScreenManager.Default.SetWaitFormDescription(description);
            }
        }

        /// <summary>
        /// 显示等待窗口
        /// </summary>
        /// <returns></returns>
        public static void ShowSplashScreen<TForm>() where TForm : WaitForm
        {
            CloseSplashScreen();
            SplashScreenManager.ShowForm(null, typeof(TForm), false, false, true);
        }

        /// <summary>
        /// 关闭等待窗口
        /// </summary>
        /// <returns></returns>
        public static void CloseSplashScreen()
        {
            if (SplashScreenManager.Default != null)
            {
                SplashScreenManager.CloseForm(true);
            }
        }
    }
}
