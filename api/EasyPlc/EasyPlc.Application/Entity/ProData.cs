namespace EasyPlc.Application;

[SugarTable("pro_data", TableDescription = "产品质量数据表")]
[Tenant(SqlsugarConst.DB_Default)]
public class ProData : ProDataTmp
{

}
