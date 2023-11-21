namespace EasyPlc.Application;

/// <summary>
/// 设备关系表
///</summary>
[SugarTable("mac_relation", TableDescription = "设备关系表")]
[Tenant(SqlsugarConst.DB_Default)]
public class MacRelation : SysRelation
{
}