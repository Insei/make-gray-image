namespace MakeGreyImageAPI.Interfaces;
/// <summary>
/// A class with methods for authenticate the user
/// </summary>
public interface IAuthenticatedUserService
{   /// <summary>
    /// Getting the user account Id
    /// </summary>
    /// <returns></returns>
    public Guid GetUserId();
}