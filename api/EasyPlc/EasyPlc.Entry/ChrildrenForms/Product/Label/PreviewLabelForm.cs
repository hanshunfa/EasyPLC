
using DevExpress.XtraEditors;
using Furion.RemoteRequest.Extensions;
using NewLife.Serialization;
using System.Net.Sockets;
using System.Net;
using EasyPlc.System;
using static EasyPlc.Web.Core.WorkingProcess;
using Furion;
using System.IO;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class PreviewLabelForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IProOrderService _proOrderService;
    private readonly IProLabelService _proLabelService;

    public PreviewLabelForm(
        IProOrderService proOrderService,
        IProLabelService proLabelService
        )
    {
        _proOrderService = proOrderService;
        _proLabelService = proLabelService;

        InitializeComponent();
    }
    private async void PreviewLabelForm_Load(object sender, EventArgs e)
    {
        await Init();
    }

    List<ProOrder> _proOrderList = new List<ProOrder>();
    private async Task Init()
    {
        _proOrderList = await _proOrderService.GetListByStatusDes();
        var sonos = _proOrderList.Select(it => it.Sono + "|" + it.Status).ToList();
        comboBoxEdit1.Properties.Items.AddRange(sonos);
    }
    private ProLabel label = null;
    private async void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectItem = comboBoxEdit1.SelectedItem.ToString();
        //通过label服务生产预览
        var sono = selectItem.Split("|")[0];
        var order = _proOrderList.Where(it => it.Sono == sono).FirstOrDefault();
        label = await _proLabelService.GetCurrentPreview(order.Id);

        memoEdit1.Text = label.PreviewJson.ConvertJsonString();
    }

    /// <summary>
    /// 手动触发打标
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void simpleButton1_Click(object sender, EventArgs e)
    {
        if (label == null)
        {
            XtraMessageBox.Show("请选择指定工单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        WaitButtonFormUtil.ShowSplashScreen<WaitButtonForm>();

        if (label.PreviewJson == "[]")
        {
            string rlt = string.Empty;
            //打标
            string IP = "192.168.99.101";
            int port = 9100;
            Socket client;
            IPAddress ip = IPAddress.Parse(IP);  //将IP地址字符串转换成IPAddress实例
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//使用指定的地址簇协议、套接字类型和通信协议
            client.SendTimeout = 100;
            client.ReceiveTimeout = 1000;
            IPEndPoint endPoint = new IPEndPoint(ip, port); // 用指定的ip和端口号初始化IPEndPoint实例
            try
            {
                client.Connect(endPoint);  //与远程主机建立连接
                client.Send(Encoding.ASCII.GetBytes("^XA\n^PH\n^XZ"));
                rlt = "打标成功";
            }
            catch (global::System.Exception ex)
            {
                rlt = "异常:" + ex.Message;
            }
            finally
            {
                client.Close();
                client.Dispose();
            }
            XtraMessageBox.Show(rlt, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            //转换成打标服务能够识别的Json类型
            LabelSend labelSend = new LabelSend();
            labelSend.Path = label.Path;
            labelSend.DriveName = label.DriveName;
            var extJsonObj = label.PreviewJson.ToObject<List<LabelParam>>();
            extJsonObj.ForEach(it =>
            {
                labelSend.Params.Add(new LabelSendParam { Key = it.Name, Value = it.Value });
            });

            var labelResult = await "http://192.168.99.8:44324/api/Label/LabelPrint"
                       .OnClientCreating(client =>
                       {
                           client.Timeout = TimeSpan.FromSeconds(20000); // 设置超时时间 20s超时
                       })
                       .SetBody(labelSend.ToJson(), "application/json")
                       .PostAsAsync<LabelRecive>();
            XtraMessageBox.Show(labelResult.ResultText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        WaitButtonFormUtil.CloseSplashScreen();
    }

    public class LabelRecive
    {
        public bool Result { get; set; }
        public string ResultText { get; set; }
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

        label = await _proLabelService.GetCurrentPreview(order.Id);
        memoEdit1.Text = label.PreviewJson.ConvertJsonString();
    }
}