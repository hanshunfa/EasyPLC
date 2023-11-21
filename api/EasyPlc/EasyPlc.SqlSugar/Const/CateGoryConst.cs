namespace EasyPlc.SqlSugar;

/// <summary>
/// 分类常量
/// </summary>
public class CateGoryConst
{
    #region 系统配置

    /// <summary>
    /// 系统基础
    /// </summary>
    public const string Config_SYS_BASE = "SYS_BASE";

    /// <summary>
    /// 业务定义
    /// </summary>
    public const string Config_BIZ_DEFINE = "BIZ_DEFINE";

    /// <summary>
    /// 文件-本地
    /// </summary>
    public const string Config_FILE_LOCAL = "FILE_LOCAL";

    /// <summary>
    /// 文件-MINIO
    /// </summary>
    public const string Config_FILE_MINIO = "FILE_MINIO";

    #endregion 系统配置

    #region Mqtt配置

    /// <summary>
    /// MQTT配置
    /// </summary>
    public const string Config_MQTT_BASE = "MQTT_BASE";

    #endregion Mqtt配置

    #region 系统关系表

    /// <summary>
    /// 用户有哪些角色
    /// </summary>
    public const string Relation_SYS_USER_HAS_ROLE = "SYS_USER_HAS_ROLE";

    /// <summary>
    /// 角色有哪些资源
    /// </summary>
    public const string Relation_SYS_ROLE_HAS_RESOURCE = "SYS_ROLE_HAS_RESOURCE";

    /// <summary>
    ///用户有哪些资源
    /// </summary>
    public const string Relation_SYS_USER_HAS_RESOURCE = "SYS_USER_HAS_RESOURCE";

    /// <summary>
    /// 角色有哪些权限
    /// </summary>
    public const string Relation_SYS_ROLE_HAS_PERMISSION = "SYS_ROLE_HAS_PERMISSION";

    /// <summary>
    /// 角色有哪些权限
    /// </summary>
    public const string Relation_SYS_USER_HAS_PERMISSION = "SYS_USER_HAS_PERMISSION";

    /// <summary>
    /// 用户工作台数据
    /// </summary>
    public const string Relation_SYS_USER_WORKBENCH_DATA = "SYS_USER_WORKBENCH_DATA";

    /// <summary>
    /// 用户日程数据
    /// </summary>
    public const string Relation_SYS_USER_SCHEDULE_DATA = "SYS_USER_SCHEDULE_DATA";

    /// <summary>
    /// 站内信与接收用户
    /// </summary>
    public const string Relation_MSG_TO_USER = "MSG_TO_USER";

    #endregion 关系表

    #region 数据范围

    /// <summary>
    /// 本人
    /// </summary>
    public const string SCOPE_SELF = "SCOPE_SELF";

    /// <summary>
    /// 所有
    /// </summary>
    public const string SCOPE_ALL = "SCOPE_ALL";

    /// <summary>
    /// 仅所属组织
    /// </summary>
    public const string SCOPE_ORG = "SCOPE_ORG";

    /// <summary>
    /// 所属组织及以下
    /// </summary>
    public const string SCOPE_ORG_CHILD = "SCOPE_ORG_CHILD";

    /// <summary>
    /// 自定义
    /// </summary>
    public const string SCOPE_ORG_DEFINE = "SCOPE_ORG_DEFINE";

    #endregion 数据范围

    #region 系统资源表

    /// <summary>
    /// 模块
    /// </summary>
    public const string Resource_MODULE = "MODULE";

    /// <summary>
    /// 菜单
    /// </summary>
    public const string Resource_MENU = "MENU";

    /// <summary>
    /// 单页
    /// </summary>
    public const string Resource_SPA = "SPA";

    /// <summary>
    /// 按钮
    /// </summary>
    public const string Resource_BUTTON = "BUTTON";

    #endregion 资源表

    #region 日志表

    /// <summary>
    /// 登录
    /// </summary>
    public const string Log_LOGIN = "LOGIN";

    /// <summary>
    /// 登出
    /// </summary>
    public const string Log_LOGOUT = "LOGOUT";

    /// <summary>
    /// 操作
    /// </summary>
    public const string Log_OPERATE = "OPERATE";

    /// <summary>
    /// 异常
    /// </summary>
    public const string Log_EXCEPTION = "EXCEPTION";

    #endregion 日志表

    #region 字典表

    /// <summary>
    /// 框架
    /// </summary>
    public const string Dict_FRM = "FRM";

    /// <summary>
    /// 业务
    /// </summary>
    public const string Dict_BIZ = "BIZ";

    #endregion 字典表

    #region 组织表

    /// <summary>
    /// 部门
    /// </summary>
    public const string Org_DEPT = "DEPT";

    /// <summary>
    /// 公司
    /// </summary>
    public const string Org_COMPANY = "COMPANY";

    #endregion 组织表

    #region PLC配置表

    /// <summary>
    /// PLC
    /// </summary>
    public const string Plc_PLC = "PLC";

    /// <summary>
    /// 公共区 读
    /// </summary>
    public const string Plc_GGQ_R = "GGQ-R";
    /// <summary>
    /// 公共区 写
    /// </summary>
    public const string Plc_GGQ_W = "GGQ-W";

    /// <summary>
    /// 自定义读区
    /// </summary>
    public const string Plc_CUSTOM_R = "CUSTOM-R";
    /// <summary>
    /// 自定义区 写
    /// </summary>
    public const string Plc_CUSTOM_W = "CUSTOM-W";

    /// <summary>
    /// 事件区 读
    /// </summary>
    public const string Plc_SJQ_R = "SJQ-R";
    /// <summary>
    /// 事件区 写
    /// </summary>
    public const string Plc_SJQ_W = "SJQ-W";

    #endregion

    #region 职位表

    /// <summary>
    /// 高层
    /// </summary>
    public const string Position_HIGH = "HIGH";

    /// <summary>
    /// 中层
    /// </summary>
    public const string Position_MIDDLE = "MIDDLE";

    /// <summary>
    /// 基层
    /// </summary>
    public const string Position_LOW = "LOW";

    #endregion 职位表

    #region 角色表

    /// <summary>
    /// 全局
    /// </summary>
    public const string Role_GLOBAL = "GLOBAL";

    /// <summary>
    /// 机构
    /// </summary>
    public const string Role_ORG = "ORG";

    #endregion 角色表

    #region 站内信表

    /// <summary>
    /// 通知
    /// </summary>
    public const string Message_INFORM = "INFORM";

    /// <summary>
    /// 公告
    /// </summary>
    public const string Message_NOTICE = "NOTICE";

    #endregion 站内信表

    #region 设备表
    /// <summary>
    /// 产线
    /// </summary>
    public const string Mac_LINE = "LINE";
    /// <summary>
    /// 设备
    /// </summary>
    public const string Mac_EQUIPMENT = "EQUIPMENT";
    /// <summary>
    /// 工位
    /// </summary>
    public const string Mac_STATION = "STATION";

    /// <summary>
    /// 分类
    /// </summary>
    public const string Mac_MODEL_CLASS = "MODEL_CLASS";
    /// <summary>
    /// 型号
    /// </summary>
    public const string Mac_MODEL_MODEL = "MODEL_MODEL";

    /// <summary>
    /// 正常流程
    /// </summary>
    public const string Mac_FLOW_MORMAL = "FLOW_MORMAL";
    /// <summary>
    /// 返修流程
    /// </summary>
    public const string Mac_FLOW_REPAIR = "FLOW_REPAIR";
   
    /// <summary>
    /// 参数
    /// </summary>
    public const string Mac_PARAMETER = "PARAMETER";

    /// <summary>
    /// 流线载具
    /// </summary>
    public const string Mac_CARRIER_LINE = "CARRIER_LINE";

    #endregion

    #region 设备关系表

    /// <summary>
    /// 流程有哪些设备
    /// </summary>
    public const string Relation_MAC_FLOW_HAS_EQUIPMENT = "MAC_FLOW_HAS_EQUIPMENT";

    #endregion

    #region 外设

    #region 资源表
    /// <summary>
    /// 基础数据
    /// </summary>
    public const string Resource_BaseData = "BASEDATA";
    /// <summary>
    /// 结构数据
    /// </summary>
    public const string Resource_StructData = "STRUCTDATA";
    /// <summary>
    /// 数组数据
    /// </summary>
    public const string Resource_ArrData = "ARRDATA";

    /// <summary>
    /// 事件
    /// </summary>
    public const string Resource_Event = "EVENT";

    #endregion

    #region 外设关系表

    /// <summary>
    ///事件有哪些资源
    /// </summary>
    public const string Relation_PLC_EVENT_HAS_RESOURCE = "PLC_EVENT_HAS_RESOURCE";

    #endregion

    #endregion
}