
namespace EasyPlc.Plugin.ScrewGun;
/// <summary>
/// 康沃螺丝枪信息
/// </summary>
public class KwScrewGunOutput
{
    /// <summary>
    /// 名称
    ///</summary>
    public string Name { get; set; }
    /// <summary>
    /// 编码
    ///</summary>
    public string Code { get; set; }
    /// <summary>
    /// IP地址
    /// </summary>
    public string Ip { get; set; }
    /// <summary>
    /// 排序码
    ///</summary>
    public int? SortCode { get; set; }
    
    /// <summary>
    /// 连接状态
    /// </summary>
    public bool IsConn { get; set; }
    /// <summary>
    /// 连接句柄Id
    /// </summary>
    public string ClientId { get; set; }
    /// <summary>
    /// 日志汇总
    /// </summary>
    public string Log { get; set; }
    /// <summary>
    /// 日志明细
    /// </summary>
    public List<string> Logs { get; set; } = new List<string>();
    /// <summary>
    /// 接收到的扭力,角度和结果 默认存放50条
    /// </summary>
    public List<RecvOutput> RecvOutputList {  get; set; } = new List<RecvOutput>();
    /// <summary>
    /// 最大保存条数 50
    /// </summary>
    public int MaxCount { get; set; } = 50;
    /// <summary>
    /// 连接时间
    /// </summary>
    public DateTime AcceptTime { get; set; }
    /// <summary>
    /// 断开时间
    /// </summary>
    public DateTime CloseTime { get; set;}
}

public class RecvOutput
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public bool Result { get; set; }
    /// <summary>
    /// 螺丝枪当前程序号
    /// </summary>
    public int ProNum { get; set; }
    /// <summary>
    /// 设置扭力
    /// </summary>
    public float SetTorque { get; set; }
    /// <summary>
    /// 设置角度
    /// </summary>
    public float SetAngle { get; set; }
    /// <summary>
    /// 运行时间-秒
    /// </summary>
    public float RunTimeS { get; set; }
    /// <summary>
    /// 扭力值
    /// </summary>
    public float Torque { get; set; }
    /// <summary>
    /// 角度值
    /// </summary>
    public float Angle { get; set; }
    /// <summary>
    /// 解析结果
    /// </summary>
    public bool PaseResult { get; set; }
    /// <summary>
    /// 解析信息
    /// </summary>
    public string PaseMsg { get; set; }
    /// <summary>
    /// 是否使用
    /// </summary>
    public bool IsUse { get; set; }
    /// <summary>
    /// 接收到数据时间
    /// </summary>
    public DateTime RevTime { get; set; }
    /// <summary>
    /// 唯一编码
    /// </summary>
    public string Code { get; set; }
}
