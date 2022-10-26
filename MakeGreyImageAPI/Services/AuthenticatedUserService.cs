using System.Security.Claims;
using MakeGreyImageAPI.Interfaces;

namespace MakeGreyImageAPI.Services;
/// <summary>
/// 
/// </summary>
public class AuthenticatedUserService : IAuthenticatedUserService
{
    private readonly Guid _userId;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="accessor"></param>
    public AuthenticatedUserService(IHttpContextAccessor accessor)
    {
        var id = accessor.HttpContext?.User.FindFirstValue("uid");
        if (!string.IsNullOrEmpty(id))
        {
           _userId = Guid.Parse(id);
        }
        
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Guid GetUserId()
    {
        return _userId;
    }
}