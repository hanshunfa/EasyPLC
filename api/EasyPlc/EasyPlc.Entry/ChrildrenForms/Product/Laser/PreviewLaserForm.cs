
using DevExpress.XtraEditors;
using System.Net.Sockets;
using System.Net;
using DevExpress.XtraSplashScreen;
using System.Threading;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class PreviewLaserForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IProOrderService _proOrderService;
    private readonly IProLaserService _proLaserService;

    public PreviewLaserForm(
        IProOrderService proOrderService,
        IProLaserService proLaserService
        )
    {
        _proOrderService = proOrderService;
        _proLaserService = proLaserService;

        InitializeComponent();
    }
    private async void PreviewLaserForm_Load(object sender, EventArgs e)
    {
        await Init();
    }

    List<ProOrder> _proOrderList = new List<ProOrder>();
    private async Task Init()
    {
        comboBoxEdit1.Properties.Items.Clear();
        _proOrderList = await _proOrderService.GetListByStatusDes();
        var sonos = _proOrderList.Select(it => it.Sono + "|" + it.Status).ToList();
        comboBoxEdit1.Properties.Items.AddRange(sonos);
    }
    private ProLaser laser = null;
    private async void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectItem = comboBoxEdit1.SelectedItem.ToString();
        //通过laser服务生产预览
        var sono = selectItem.Split("|")[0];
        var order = _proOrderList.Where(it => it.Sono == sono).FirstOrDefault();
        laser = await _proLaserService.GetCurrentPreview(order.Id);

        memoEdit1.Text = laser.PreviewJson.ConvertJsonString();
    }

    /// <summary>
    /// 发送预览到镭射机
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void simpleButton1_Click(object sender, EventArgs e)
    {
        if (laser == null)
        {
            XtraMessageBox.Show("请选择指定工单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        WaitButtonFormUtil.ShowSplashScreen<WaitButtonForm>();

        LaserSend ls = new LaserSend();
        ls.Path = laser.Path;
        var laserParamList = laser.PreviewJson.ToObject<List<LaserParam>>();
        laserParamList.ForEach(it =>
        {
            ls.DataList.Add(it.Value);
        });

        string laserResult = string.Empty;
        //发送镭射指令到镭射机
        //激光打印
        string IP = "192.168.99.91";
        int port = 8888;
        Socket ClientSocket;
        IPAddress ip = IPAddress.Parse(IP);  //将IP地址字符串转换成IPAddress实例
        ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//使用指定的地址簇协议、套接字类型和通信协议
        ClientSocket.SendTimeout = 100;
        ClientSocket.ReceiveTimeout = 30000;//30s
        IPEndPoint endPoint = new IPEndPoint(ip, port); // 用指定的ip和端口号初始化IPEndPoint实例
        try
        {
            ClientSocket.Connect(endPoint);  //与远程主机建立连接

            string starCmd = Encoding.ASCII.GetString(new byte[] { 0x02 });
            string endCmd = Encoding.ASCII.GetString(new byte[] { 0x03 });
            starCmd = "";
            endCmd = "";
            string cmd = string.Empty;//指令
            byte[] send = null;//发送内容
            byte[] receive = new byte[1024];//接收内容
            int receiveLen = 0;
            string strRreceive = string.Empty;
            //切换模板指令
            cmd = "$Initialize_" + ls.Path;
            send = Encoding.ASCII.GetBytes(starCmd + cmd + endCmd);
            ClientSocket.Send(send);
            receiveLen = ClientSocket.Receive(receive);
            strRreceive = Encoding.ASCII.GetString(receive, 0, receiveLen).Trim();
            if (strRreceive.Contains("$Initialize_OK"))
            {
                //设定参数指令
                cmd = "$Data_";
                for (int i = 0; i < ls.DataList.Count; i++)
                {
                    cmd += ls.DataList[i];
                    if (i < ls.DataList.Count - 1) cmd += ",";
                }
                send = Encoding.ASCII.GetBytes(starCmd + cmd + endCmd);
                ClientSocket.Send(send);
                receiveLen = ClientSocket.Receive(receive);
                strRreceive = Encoding.ASCII.GetString(receive, 0, receiveLen).Trim();
                if (strRreceive.Contains("$Receive_OK"))
                {
                    //打标指令
                    cmd = "$MarkStart_";
                    send = Encoding.ASCII.GetBytes(starCmd + cmd + endCmd);
                    ClientSocket.Send(send);
                    receiveLen = ClientSocket.Receive(receive);
                    strRreceive = Encoding.ASCII.GetString(receive, 0, receiveLen).Trim();
                    if (strRreceive.Contains("$MarkStart_OK"))
                    {
                        //等待接收，打标完成指令
                        receiveLen = ClientSocket.Receive(receive);
                        strRreceive = Encoding.ASCII.GetString(receive, 0, receiveLen).Trim();
                        if (strRreceive.Contains("MarkEnd_OK"))
                        {
                            laserResult = $"镭射成功";
                        }
                        else
                        {
                            laserResult = $"{cmd}失败";
                        }
                    }
                    else
                    {
                        laserResult = $"{cmd}失败";
                    }
                }
                else
                {
                    laserResult = $"{cmd}失败";
                }
            }
            else
            {
                //切换模板失败
                laserResult = $"{cmd}失败";
            }
        }
        catch (global::System.Exception ex)
        {
            laserResult = $"激光镭射异常:{ex.Message}";


        }
        finally
        {
            ClientSocket.Close();
            ClientSocket.Dispose();
        }

        WaitButtonFormUtil.CloseSplashScreen();

        XtraMessageBox.Show(laserResult, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    /// 获取当前正在运行工单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void simpleButton2_Click(object sender, EventArgs e)
    {
        var order = await _proOrderService.GetWorkingOrder();
        if (order == null)
        {
            XtraMessageBox.Show("当前没有正在加工的工单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        laser = await _proLaserService.GetCurrentPreview(order.Id);
        memoEdit1.Text = laser.PreviewJson.ConvertJsonString();

    }
}