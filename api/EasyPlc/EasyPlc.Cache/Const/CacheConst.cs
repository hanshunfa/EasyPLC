namespace EasyPlc.Cache;

/// <summary>
/// Redis常量
/// </summary>
public class CacheConst
{
    /// <summary>
    /// Redis Key前缀(可删除)
    /// </summary>
    public const string Cache_Prefix_Web = "EasyPlcWeb:";

    /// <summary>
    /// Redis Key前缀(需要持久化，不随系统重启删除)
    /// </summary>
    public const string Cache_Prefix = "EasyPlc:";

    /// <summary>
    /// Redis Hash类型
    /// </summary>
    public const string Cache_Hash = "Hash";

    /// <summary>
    /// 系统配置表缓存Key
    /// </summary>
    public const string Cache_DevConfig = Cache_Prefix_Web + "DevConfig:";

    /// <summary>
    /// 登录验证码缓存Key
    /// </summary>
    public const string Cache_Captcha = Cache_Prefix_Web + "Captcha:";

    /// <summary>
    /// 用户表缓存Key
    /// </summary>
    public const string Cache_SysUser = Cache_Prefix_Web + "SysUser";

    /// <summary>
    /// 用户手机号关系缓存Key
    /// </summary>
    public const string Cache_SysUserPhone = Cache_Prefix_Web + "SysUserPhone";

    /// <summary>
    /// 用户账户关系缓存Key
    /// </summary>
    public const string Cache_SysUserAccount = Cache_Prefix_Web + "SysUserAccount";

    /// <summary>
    /// 资源表缓存Key
    /// </summary>
    public const string Cache_SysResource = Cache_Prefix_Web + "SysResource:";

    /// <summary>
    /// 字典表缓存Key
    /// </summary>
    public const string Cache_DevDict = Cache_Prefix_Web + "DevDict";

    /// <summary>
    /// 关系表缓存Key
    /// </summary>
    public const string Cache_SysRelation = Cache_Prefix_Web + "SysRelation:";

    /// <summary>
    /// 关系表缓存Key
    /// </summary>
    public const string Cache_PlcRelation = Cache_Prefix_Web + "PlcRelation:";

    /// <summary>
    /// 关系表缓存Key
    /// </summary>
    public const string Cache_MacRelation = Cache_Prefix_Web + "MacRelation:";

    /// <summary>
    /// 机构表缓存Key
    /// </summary>
    public const string Cache_SysOrg = Cache_Prefix_Web + "SysOrg";

    /// <summary>
    /// 角色表缓存Key
    /// </summary>
    public const string Cache_SysRole = Cache_Prefix_Web + "SysRole";

    /// <summary>
    /// 职位表缓存Key
    /// </summary>
    public const string Cache_SysPosition = Cache_Prefix_Web + "SysPosition";

    /// <summary>
    /// mqtt认证登录信息key
    /// </summary>
    public const string Cache_MqttClientUser = Cache_Prefix_Web + "MqttClientUser:";

    /// <summary>
    /// 用户Token缓存Key
    /// </summary>
    public const string Cache_UserToken = Cache_Prefix_Web + "UserToken";


    /// <summary>
    /// 设备表缓存Key
    /// </summary>
    public const string Cache_MacEquipment = Cache_Prefix_Web + "MacEquipment";

    /// <summary>
    /// 型号表缓存Key
    /// </summary>
    public const string Cache_MacModel = Cache_Prefix_Web + "MacModel";

    /// <summary>
    /// 参数表缓存Key
    /// </summary>
    public const string Cache_MacParameter = Cache_Prefix_Web + "MacParameter";

    /// <summary>
    /// 载具表缓存Key
    /// </summary>
    public const string Cache_MacCarrier = Cache_Prefix_Web + "MacCarrier";
    /// <summary>
    /// 载具位置表缓存Key
    /// </summary>
    public const string Cache_MacPoint = Cache_Prefix_Web + "MacPoint";
    /// <summary>
    /// PLC配置表缓存Key
    /// </summary>
    public const string Cache_PlcConfig = Cache_Prefix_Web + "PlcConfig";
    /// <summary>
    /// PLC资源表缓存Key
    /// </summary>
    public const string Cache_PlcResource = Cache_Prefix_Web + "PlcResource:";

    /// <summary>
    /// PLC地址表缓存Key
    /// </summary>
    public const string Cache_PlcAddress = Cache_Prefix_Web + "PlcAddress:";

    /// <summary>
    /// 流程表缓存Key
    /// </summary>
    public const string Cache_MacFlow = Cache_Prefix_Web + "MacFlow";

    /// <summary>
    /// 流程参数表缓存Key
    /// </summary>
    public const string Cache_MacFlowParam = Cache_Prefix_Web + "MacFlowParam";

    /// <summary>
    /// 思谷RFID表缓存Key
    /// </summary>
    public const string Cache_SygoleRfid = Cache_Prefix_Web + "SygoleRfid";
    /// <summary>
    /// 固定扫码器表缓存Key
    /// </summary>
    public const string Cache_FixedScan = Cache_Prefix_Web + "FixedScan";
    /// <summary>
    /// 螺丝枪表缓存Key
    /// </summary>
    public const string Cache_KwScrewGun = Cache_Prefix_Web + "KwScrewGun";
    /// <summary>
    /// 工单表缓存Key
    /// </summary>
    public const string Cache_ProOrder = Cache_Prefix_Web + "ProOrder";
    /// <summary>
    /// 镭射表缓存Key
    /// </summary>
    public const string Cache_ProLaser = Cache_Prefix_Web + "ProLaser";
    /// <summary>
    /// 标签表缓存Key
    /// </summary>
    public const string Cache_ProLabel = Cache_Prefix_Web + "ProLabel";
    /// <summary>
    /// 加工过程缓存Key
    /// </summary>
    public const string Cache_ProWorkingStep = Cache_Prefix_Web + "ProWorkingStep";
    /// <summary>
    /// 扩展加工过程缓存Key
    /// </summary>
    public const string Cache_ExtWorkingStep = Cache_Prefix_Web + "ExtWorkingStep";
    /// <summary>
    /// 加工数据缓存Key
    /// </summary>
    public const string Cache_ProDataTmp = Cache_Prefix_Web + "ProDataTmp";
    public const string Cache_RabbitMq = Cache_Prefix_Web + "RabbitMq";
}