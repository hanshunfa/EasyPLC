
namespace EasyPlc.Plugin.ScrewGun;

public class KwScrewGunReturnInfo
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
}
