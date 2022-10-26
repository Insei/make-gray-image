using System.Security.Claims;
using MakeGreyImageAPI.Interfaces;

namespace MakeGreyImageAPI.Services;
/// <summary>
/// A class with methods for authenticate the user
/// </summary>
public class AuthenticatedUserService : IAuthenticatedUserService
{
    private readonly Guid _userId;
    /// <summary>
    /// AuthenticatedUserService constructor
    /// </summary>
    /// <param name="accessor">IHttpContextAccessor</param>
    public AuthenticatedUserService(IHttpContextAccessor accessor)
    {
        var id = accessor.HttpContext?.User.FindFirstValue("uid");
        if (!string.IsNullOrEmpty(id))
        {
            _userId = Guid.Parse(id);
        }
    }
    /// <summary>
    /// Getting the user account Id
    /// </summary>
    /// <returns>User Id</returns>
    public Guid GetUserId()
    {
        return _userId;
    }
}