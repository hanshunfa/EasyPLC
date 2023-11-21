namespace EasyPlc.Web.Core;

/// <summary>
/// 基础控制器
/// </summary>
[Route("sys/[controller]")]
//[SuperAdmin]
[AllowAnonymous]
public class BaseController : IDynamicApiController
{
}