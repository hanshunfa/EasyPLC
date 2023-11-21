namespace EasyPlc.Application;

/// <summary>
/// PLC关系表
///</summary>
[SugarTable("plc_relation", TableDescription = "PLC关系表")]
[Tenant(SqlsugarConst.DB_Default)]
public class PlcRelation : SysRelation
{
}
