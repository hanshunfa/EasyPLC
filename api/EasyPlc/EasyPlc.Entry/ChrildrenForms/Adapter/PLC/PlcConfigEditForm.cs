using DevExpress.XtraEditors;
using Mapster;
using Masuit.Tools;

namespace EasyPlc.Entry.ChrildrenForms;

public partial class PlcConfigEditForm : DevExpress.XtraEditors.XtraForm
{
    private readonly IPlcConfigService _configService;
    private readonly List<PlcConfig> _plcConfigs;

    public PlcConfigEditForm(
        IPlcConfigService configService,
        List<PlcConfig> plcConfigs
        )
    {
        InitializeComponent();

        _configService = configService;
        _plcConfigs = plcConfigs;
        InitEditForm();
        InitData();
    }
    private void InitEditForm()
    {

    }
    private void InitData()
    {
        if (_plcConfigs.Count == 0)
        {

        }
        else
        {
            textEdit1.Text = _plcConfigs[0].Name;
            spinEdit1.Value = _plcConfigs[0].SortCode ?? 99;
            //扩展信息
            var plcExtJson = _plcConfigs[0].ExtJson.ToObject<PlcExtJson>();
            comboBoxEdit1.Text = plcExtJson.Type;
            comboBoxEdit2.Text = plcExtJson.Version;
            textEdit3.Text = plcExtJson.Ip;
            textEdit2.Text = plcExtJson.Port.ToString();
            textEdit4.Text = plcExtJson.Rack.ToString();
            textEdit5.Text = plcExtJson.Slot.ToString();

            var addrExtJsonCR = _plcConfigs[1].ExtJson.ToObject<AddrExtJson>();
            textEdit6.Text = addrExtJsonCR.StartAddr;

            var addrExtJsonCW = _plcConfigs[2].ExtJson.ToObject<AddrExtJson>();
            textEdit11.Text = addrExtJsonCW.StartAddr;

            var addrExtJsonPR = _plcConfigs[3].ExtJson.ToObject<AddrExtJson>();
            textEdit7.Text = addrExtJsonPR.StartAddr;

            var addrExtJsonPW = _plcConfigs[4].ExtJson.ToObject<AddrExtJson>();
            textEdit8.Text = addrExtJsonPW.StartAddr;

            var addrExtJsonER = _plcConfigs[5].ExtJson.ToObject<AddrExtJson>();
            textEdit9.Text = addrExtJsonER.StartAddr;

            var addrExtJsonEW = _plcConfigs[6].ExtJson.ToObject<AddrExtJson>();
            textEdit10.Text = addrExtJsonEW.StartAddr;
        }
    }
    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void simpleButton1_Click(object sender, EventArgs e)
    {
        try
        {
            //创建扩展信息
            var plcExtJson = new PlcExtJson();
            plcExtJson.Type = comboBoxEdit1.Text;
            plcExtJson.Version = comboBoxEdit2.Text;
            plcExtJson.Ip = textEdit3.Text;
            plcExtJson.Port = textEdit2.Text.ToInt();
            plcExtJson.Rack = textEdit4.Text.ToInt();
            plcExtJson.Slot = textEdit5.Text.ToInt();

            var addrExtJsonCR = new AddrExtJson();
            addrExtJsonCR.StartAddr = textEdit6.Text.Trim();

            var addrExtJsonCW = new AddrExtJson();
            addrExtJsonCW.StartAddr = textEdit11.Text.Trim();

            var addrExtJsonPR = new AddrExtJson();
            addrExtJsonPR.StartAddr = textEdit7.Text.Trim();

            var addrExtJsonPW = new AddrExtJson();
            addrExtJsonPW.StartAddr = textEdit8.Text.Trim();

            var addrExtJsonER = new AddrExtJson();
            addrExtJsonER.StartAddr = textEdit9.Text.Trim();

            var addrExtJsonEW = new AddrExtJson();
            addrExtJsonEW.StartAddr = textEdit10.Text.Trim();

            if (_plcConfigs.Count == 0)
            {
                //新增

                // 1 PLC
                var plc = new PlcConfig();
                //获取信息
                plc.ParentId = 0;
                plc.Name = textEdit1.Text.Trim();
                plc.SortCode = spinEdit1.Value.ToInt();
                plc.Category = "PLC";
                plc.ExtJson = plcExtJson.ToJsonString();

                long plcId = await _configService.AddReturnSnowflakeId(plc.Adapt<PlcConfigAddInput>());
                //创建-自定义区
                var customr = new PlcConfig();
                customr.ParentId = plcId;
                customr.Name = "自定义区-读";
                customr.Category = "CUSTOM-R";
                customr.SortCode = 1;
                customr.ExtJson = addrExtJsonCR.ToJsonString();
                await _configService.Add(customr.Adapt<PlcConfigAddInput>());

                var customw = new PlcConfig();
                customw.ParentId = plcId;
                customw.Name = "自定义区-写";
                customw.Category = "CUSTOM-W";
                customw.SortCode = 2;
                customw.ExtJson = addrExtJsonCW.ToJsonString();
                await _configService.Add(customw.Adapt<PlcConfigAddInput>());
                //创建-公共区
                var ggr = new PlcConfig();
                ggr.ParentId = plcId;
                ggr.Name = "公共区-读";
                ggr.Category = "GGQ-R";
                ggr.SortCode = 3;
                ggr.ExtJson = addrExtJsonPR.ToJsonString();
                await _configService.Add(ggr.Adapt<PlcConfigAddInput>());

                var ggw = new PlcConfig();
                ggw.ParentId = plcId;
                ggw.Name = "公共区-写";
                ggw.Category = "GGQ-W";
                ggw.SortCode = 4;
                ggw.ExtJson = addrExtJsonPW.ToJsonString();
                await _configService.Add(ggw.Adapt<PlcConfigAddInput>());
                //创建-事件区
                var sjr = new PlcConfig();
                sjr.ParentId = plcId;
                sjr.Name = "事件区-读";
                sjr.Category = "SJQ-R";
                sjr.SortCode = 5;
                sjr.ExtJson = addrExtJsonER.ToJsonString();
                await _configService.Add(sjr.Adapt<PlcConfigAddInput>());

                var sjw = new PlcConfig();
                sjw.ParentId = plcId;
                sjw.Name = "事件区-写";
                sjw.Category = "SJQ-W";
                sjw.SortCode = 6;
                sjw.ExtJson = addrExtJsonEW.ToJsonString();
                await _configService.Add(sjw.Adapt<PlcConfigAddInput>());
            }
            else
            {
                //编辑

                //获取信息
                _plcConfigs[0].Name = textEdit1.Text.Trim();
                _plcConfigs[0].SortCode = spinEdit1.Value.ToInt();
                _plcConfigs[0].Category = "PLC";
                _plcConfigs[0].ExtJson = plcExtJson.ToJsonString();
                await _configService.Edit(_plcConfigs[0].Adapt<PlcConfigEditInput>());

                _plcConfigs[1].Category = "CUSTOM-R";
                _plcConfigs[1].ExtJson = addrExtJsonCR.ToJsonString();
                await _configService.Edit(_plcConfigs[1].Adapt<PlcConfigEditInput>());

                _plcConfigs[2].Category = "CUSTOM-W";
                _plcConfigs[2].ExtJson = addrExtJsonCW.ToJsonString();
                await _configService.Edit(_plcConfigs[2].Adapt<PlcConfigEditInput>());

                _plcConfigs[3].Category = "GGQ-R";
                _plcConfigs[3].ExtJson = addrExtJsonPR.ToJsonString();
                await _configService.Edit(_plcConfigs[3].Adapt<PlcConfigEditInput>());

                _plcConfigs[4].Category = "GGQ-W";
                _plcConfigs[4].ExtJson = addrExtJsonPW.ToJsonString();
                await _configService.Edit(_plcConfigs[4].Adapt<PlcConfigEditInput>());

                _plcConfigs[5].Category = "SJQ-R";
                _plcConfigs[5].ExtJson = addrExtJsonER.ToJsonString();
                await _configService.Edit(_plcConfigs[5].Adapt<PlcConfigEditInput>());

                _plcConfigs[6].Category = "SJQ-W";
                _plcConfigs[6].ExtJson = addrExtJsonEW.ToJsonString();
                await _configService.Edit(_plcConfigs[6].Adapt<PlcConfigEditInput>());
            }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        DialogResult = DialogResult.OK;
    }
    /// <summary>
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void simpleButton2_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}