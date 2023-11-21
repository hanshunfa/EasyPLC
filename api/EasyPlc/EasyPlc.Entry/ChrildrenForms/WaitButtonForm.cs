
using ST =System.Threading;
using System.Threading;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class WaitButtonForm : WaitForm
{
    // 启动新线程用于计时
    ST.Timer myTimer;
    long TimeCount;

    delegate void SetValue();

    // 线程执行计时任务
    private void TimerUp(object state)
    {
        if (this.IsHandleCreated)
        {
            this.Invoke(new SetValue(ShowTime));
            TimeCount++;
        }
    }

    public void ShowTime()
    {
        TimeSpan t = new TimeSpan(0, 0, (int)TimeCount);
        labelControl1.Text = t.TotalSeconds + "秒";
        labelControl1.ForeColor = Color.Blue;
    }

    public WaitButtonForm()
    {
        InitializeComponent();
    }

    private void WaitButtonForm_Load(object sender, EventArgs e)
    {
        myTimer = new ST.Timer(new TimerCallback(TimerUp), null, Timeout.Infinite, 1000);

        // 计时开始
        TimeCount = 0;
        myTimer.Change(0, 1000);
    }

    // 计时结束
    private void WaitButtonForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        if (myTimer != null)
        {
            myTimer.Change(Timeout.Infinite, 1000);
            myTimer = null;
        }
    }
}