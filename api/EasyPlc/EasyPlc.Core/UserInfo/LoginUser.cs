
namespace EasyPlc.Core.UserInfo;

public class LoginUser
{
    public static string UserId { get; set; }
    public static string Account { get; set; }
    public static string Name { get; set; }
    public static string OrgId { get; set; }
    public static bool IsLogin { get; set; } = false;
    public static bool IsSuperAdmin { get; set;} = true;
}
