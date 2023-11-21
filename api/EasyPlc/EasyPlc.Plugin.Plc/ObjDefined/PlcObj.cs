namespace EasyPlc.Plugin.Plc;

/// <summary>
///通用公共区PlcToEap
/// <summary>
public class General_PI_PlcToEap
{
    [KstopaStructProperty(8)]
    /// <summary>
    ///事件触发
    /// <summary>
    public bool[] EventTriggerBit {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///心跳
    /// <summary>
    public short HeartBeat {get; set; }
}
/// <summary>
///通用公共区EapToPlc
/// <summary>
public class General_PI_EapToPlc
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///心跳
    /// <summary>
    public short HearBeat {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///命令
    /// <summary>
    public short Command {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///型号1
    /// <summary>
    public short Model1 {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///型号2
    /// <summary>
    public short Model2 {get; set; }
}
/// <summary>
///Base_PlcToEap_IN
/// <summary>
public class Base_PlcToEap_IN
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
}
/// <summary>
///Base_EapToPlc_IN
/// <summary>
public class Base_EapToPlc_IN
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///ACK
    /// <summary>
    public short ACK {get; set; }
    [KstopaStructProperty(64, "UNICODE")]
    /// <summary>
    ///反馈信息
    /// <summary>
    public string Msg {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///编码
    /// <summary>
    public short ResultCode {get; set; }
    [KstopaStructProperty(42)]
    /// <summary>
    ///载具SN
    /// <summary>
    public string CarrierSN {get; set; }
    [KstopaStructProperty(102)]
    /// <summary>
    ///线束编码
    /// <summary>
    public string CableSN {get; set; }
}
/// <summary>
///Base_EapToPlc_OUT
/// <summary>
public class Base_EapToPlc_OUT
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///ACK
    /// <summary>
    public short ACK {get; set; }
    [KstopaStructProperty(64, "UNICODE")]
    /// <summary>
    ///反馈信息
    /// <summary>
    public string Msg {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///编码
    /// <summary>
    public short ResultCode {get; set; }
    [KstopaStructProperty(42)]
    /// <summary>
    ///载具编码
    /// <summary>
    public string CarrierSN {get; set; }
}
/// <summary>
///Base_PlcToEap_OUT
/// <summary>
public class Base_PlcToEap_OUT
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///编码
    /// <summary>
    public short ResultCode {get; set; }
}
/// <summary>
///Base_EapToPlc_ScrewGun
/// <summary>
public class Base_EapToPlc_ScrewGun
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///ACK
    /// <summary>
    public short ACK {get; set; }
    [KstopaStructProperty(64, "UNICODE")]
    /// <summary>
    ///反馈信息
    /// <summary>
    public string Msg {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///编码
    /// <summary>
    public short ResultCode {get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///螺丝枪下发结构
    /// <summary>
    public ScrewGun_Send ScrewGun_Send{get; set; }
}
/// <summary>
///Base_PlcToEap_ScrewGun
/// <summary>
public class Base_PlcToEap_ScrewGun
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///ResultCode
    /// <summary>
    public short ResultCode {get; set; }
    [KstopaStructProperty(42)]
    /// <summary>
    ///载具编码
    /// <summary>
    public string CarrierSN {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///Number
    /// <summary>
    public short Number {get; set; }
}
/// <summary>
///螺丝枪下发结构
/// <summary>
public class ScrewGun_Send
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///打螺丝结果(1ok,2ng)
    /// <summary>
    public short Result {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///扭矩设置(Nm)
    /// <summary>
    public float TorqueSet {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///扭矩实测值(Nm)
    /// <summary>
    public float Torque {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///角度设置(°)
    /// <summary>
    public float AngleSet {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///角度实测值(°)
    /// <summary>
    public float Angle {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///运行时间(s)
    /// <summary>
    public float RunTime {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///程序号
    /// <summary>
    public short ProNm {get; set; }
}
/// <summary>
///OP2001出站报工
/// <summary>
public class OP2001_Base_PlcToEap_OUT
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///编码
    /// <summary>
    public short ResultCode {get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///端子01
    /// <summary>
    public TerminalCrimping01_Recive TerminalCrimping01_Recive{get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///端子02
    /// <summary>
    public TerminalCrimping02_Recive TerminalCrimping02_Recive{get; set; }
}
/// <summary>
///端子压接上报结构
/// <summary>
public class TerminalCrimping_Recive
{
    [KstopaStructProperty(4)]
    /// <summary>
    ///压力
    /// <summary>
    public float Pressure {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///高度
    /// <summary>
    public float Height {get; set; }
}
/// <summary>
///端子01
/// <summary>
public class TerminalCrimping01_Recive
{
    [KstopaStructProperty(4)]
    /// <summary>
    ///压力
    /// <summary>
    public float Pressure {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///高度
    /// <summary>
    public float Height {get; set; }
}
/// <summary>
///端子02
/// <summary>
public class TerminalCrimping02_Recive
{
    [KstopaStructProperty(4)]
    /// <summary>
    ///压力
    /// <summary>
    public float Pressure {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///高度
    /// <summary>
    public float Height {get; set; }
}
/// <summary>
///飞丝检测上报结构
/// <summary>
public class CCD_Feisi_Recive
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///飞丝识别结果(1ok,2ng)
    /// <summary>
    public short Result {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///压接宽度结果
    /// <summary>
    public short WidthResult {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///端子压接位置(mm)
    /// <summary>
    public float Width {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///压接间距结果(1ok,2ng)
    /// <summary>
    public short SpaceResult {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///压接间距
    /// <summary>
    public float Space {get; set; }
}
/// <summary>
///螺丝枪上报结构
/// <summary>
public class ScrewGun_Recive
{
    [KstopaStructProperty(4)]
    /// <summary>
    ///扭力(Nm)
    /// <summary>
    public float Torque {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///角度(°)
    /// <summary>
    public float Angle {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///披头使用次数
    /// <summary>
    public int UseCount {get; set; }
}
/// <summary>
///热缩上报结构
/// <summary>
public class ThermalShrinkage_Recive
{
    [KstopaStructProperty(4)]
    /// <summary>
    ///热缩温度(°)
    /// <summary>
    public float Temperature {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///热缩时间(s)
    /// <summary>
    public float Time {get; set; }
}
/// <summary>
///OP3001出站报工
/// <summary>
public class OP3001_Base_PlcToEap_OUT
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///编码
    /// <summary>
    public short ResultCode {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///打螺钉实际个数
    /// <summary>
    public short Number {get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///螺丝枪上报结构01
    /// <summary>
    public ScrewGun_Recive_01 ScrewGun_Recive_01{get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///螺丝枪上报结构02
    /// <summary>
    public ScrewGun_Recive_02 ScrewGun_Recive_02{get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///螺丝枪上报结构03
    /// <summary>
    public ScrewGun_Recive_03 ScrewGun_Recive_03{get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///螺丝枪上报结构04
    /// <summary>
    public ScrewGun_Recive_04 ScrewGun_Recive_04{get; set; }
}
/// <summary>
///螺丝枪上报结构01
/// <summary>
public class ScrewGun_Recive_01
{
    [KstopaStructProperty(4)]
    /// <summary>
    ///扭力(Nm)
    /// <summary>
    public float Torque {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///角度(°)
    /// <summary>
    public float Angle {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///披头使用次数
    /// <summary>
    public int UseCount {get; set; }
}
/// <summary>
///螺丝枪上报结构02
/// <summary>
public class ScrewGun_Recive_02
{
    [KstopaStructProperty(4)]
    /// <summary>
    ///扭力(Nm)
    /// <summary>
    public float Torque {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///角度(°)
    /// <summary>
    public float Angle {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///披头使用次数
    /// <summary>
    public int UseCount {get; set; }
}
/// <summary>
///螺丝枪上报结构03
/// <summary>
public class ScrewGun_Recive_03
{
    [KstopaStructProperty(4)]
    /// <summary>
    ///扭力(Nm)
    /// <summary>
    public float Torque {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///角度(°)
    /// <summary>
    public float Angle {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///披头使用次数
    /// <summary>
    public int UseCount {get; set; }
}
/// <summary>
///螺丝枪上报结构04
/// <summary>
public class ScrewGun_Recive_04
{
    [KstopaStructProperty(4)]
    /// <summary>
    ///扭力(Nm)
    /// <summary>
    public float Torque {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///角度(°)
    /// <summary>
    public float Angle {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///披头使用次数
    /// <summary>
    public int UseCount {get; set; }
}
/// <summary>
///OP2002出站报工
/// <summary>
public class OP2002_Base_PlcToEap_OUT
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///编码
    /// <summary>
    public short ResultCode {get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///飞丝检测上报结构01
    /// <summary>
    public CCD_Feisi_Recive_01 CCD_Feisi_Recive_01{get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///飞丝检测上报结构02
    /// <summary>
    public CCD_Feisi_Recive_02 CCD_Feisi_Recive_02{get; set; }
}
/// <summary>
///OP4002出站报工
/// <summary>
public class OP4002_Base_PlcToEap_OUT
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///编码
    /// <summary>
    public short ResultCode {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///打螺钉实际个数
    /// <summary>
    public short Number {get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///螺丝枪上报结构01
    /// <summary>
    public ScrewGun_Recive_01 ScrewGun_Recive_01{get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///螺丝枪上报结构02
    /// <summary>
    public ScrewGun_Recive_02 ScrewGun_Recive_02{get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///螺丝枪上报结构03
    /// <summary>
    public ScrewGun_Recive_03 ScrewGun_Recive_03{get; set; }
}
/// <summary>
///OP3002出站报工
/// <summary>
public class OP3002_Base_PlcToEap_OUT
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///编码
    /// <summary>
    public short ResultCode {get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///热缩上报结构
    /// <summary>
    public ThermalShrinkage_Recive ThermalShrinkage_Recive{get; set; }
}
/// <summary>
///零部件检查结构
/// <summary>
public class CCD_Part_Recive
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///螺丝钉有无漏打(1ok,2ng)
    /// <summary>
    public short ScrewIsOk {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///胶圈有无漏装(1ok,2ng)
    /// <summary>
    public short RubberIsOk {get; set; }
}
/// <summary>
///OP5001出站报工
/// <summary>
public class OP5001_Base_PlcToEap_OUT
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///编码
    /// <summary>
    public short ResultCode {get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///零部件检查结构
    /// <summary>
    public CCD_Part_Recive CCD_Part_Recive{get; set; }
}
/// <summary>
///OP5002出站报工
/// <summary>
public class OP5002_Base_PlcToEap_OUT
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///编码
    /// <summary>
    public short ResultCode {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///打螺钉实际个数
    /// <summary>
    public short Number {get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///螺丝枪上报结构01
    /// <summary>
    public ScrewGun_Recive_01 ScrewGun_Recive_01{get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///螺丝枪上报结构02
    /// <summary>
    public ScrewGun_Recive_02 ScrewGun_Recive_02{get; set; }
}
/// <summary>
///OP3001螺丝枪PLC触发事件
/// <summary>
public class OP3001_Base_PlcToEap_ScrewGun
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///ResultCode
    /// <summary>
    public short ResultCode {get; set; }
    [KstopaStructProperty(42)]
    /// <summary>
    ///载具编码
    /// <summary>
    public string CarrierSN {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///Number
    /// <summary>
    public short Number {get; set; }
}
/// <summary>
///OP3001螺丝枪Eap事件回复
/// <summary>
public class OP3001_Base_EapToPlc_ScrewGun
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///ACK
    /// <summary>
    public short ACK {get; set; }
    [KstopaStructProperty(64, "UNICODE")]
    /// <summary>
    ///反馈信息
    /// <summary>
    public string Msg {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///编码
    /// <summary>
    public short ResultCode {get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///螺丝枪下发结构
    /// <summary>
    public ScrewGun_Send ScrewGun_Send{get; set; }
}
/// <summary>
///OP4002螺丝枪PLC触发事件
/// <summary>
public class OP4002_Base_PlcToEap_ScrewGun
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///ResultCode
    /// <summary>
    public short ResultCode {get; set; }
    [KstopaStructProperty(42)]
    /// <summary>
    ///载具编码
    /// <summary>
    public string CarrierSN {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///Number
    /// <summary>
    public short Number {get; set; }
}
/// <summary>
///OP4002螺丝枪Eap事件回复
/// <summary>
public class OP4002_Base_EapToPlc_ScrewGun
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///ACK
    /// <summary>
    public short ACK {get; set; }
    [KstopaStructProperty(64, "UNICODE")]
    /// <summary>
    ///反馈信息
    /// <summary>
    public string Msg {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///编码
    /// <summary>
    public short ResultCode {get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///螺丝枪下发结构
    /// <summary>
    public ScrewGun_Send ScrewGun_Send{get; set; }
}
/// <summary>
///OP5002螺丝枪PLC触发事件
/// <summary>
public class OP5002_Base_PlcToEap_ScrewGun
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///ResultCode
    /// <summary>
    public short ResultCode {get; set; }
    [KstopaStructProperty(42)]
    /// <summary>
    ///载具编码
    /// <summary>
    public string CarrierSN {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///Number
    /// <summary>
    public short Number {get; set; }
}
/// <summary>
///OP5002螺丝枪Eap事件回复
/// <summary>
public class OP5002_Base_EapToPlc_ScrewGun
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///SequenceID
    /// <summary>
    public short SequenceID {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///ACK
    /// <summary>
    public short ACK {get; set; }
    [KstopaStructProperty(64, "UNICODE")]
    /// <summary>
    ///反馈信息
    /// <summary>
    public string Msg {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///编码
    /// <summary>
    public short ResultCode {get; set; }
    [KstopaStruct(1)]
    /// <summary>
    ///螺丝枪下发结构
    /// <summary>
    public ScrewGun_Send ScrewGun_Send{get; set; }
}
/// <summary>
///飞丝检测上报结构01
/// <summary>
public class CCD_Feisi_Recive_01
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///飞丝识别结果(1ok,2ng)
    /// <summary>
    public short Result {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///压接宽度结果
    /// <summary>
    public short WidthResult {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///端子压接位置(mm)
    /// <summary>
    public float Width {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///压接间距结果(1ok,2ng)
    /// <summary>
    public short SpaceResult {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///压接间距
    /// <summary>
    public float Space {get; set; }
}
/// <summary>
///飞丝检测上报结构02
/// <summary>
public class CCD_Feisi_Recive_02
{
    [KstopaStructProperty(2)]
    /// <summary>
    ///飞丝识别结果(1ok,2ng)
    /// <summary>
    public short Result {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///压接宽度结果
    /// <summary>
    public short WidthResult {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///端子压接位置(mm)
    /// <summary>
    public float Width {get; set; }
    [KstopaStructProperty(2)]
    /// <summary>
    ///压接间距结果(1ok,2ng)
    /// <summary>
    public short SpaceResult {get; set; }
    [KstopaStructProperty(4)]
    /// <summary>
    ///压接间距
    /// <summary>
    public float Space {get; set; }
}
