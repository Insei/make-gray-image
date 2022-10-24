using Microsoft.AspNetCore.Identity;

namespace MakeGreyImageAPI.Entities;
/// <summary>
/// entity with information about user
/// </summary>
public class ApplicationUser : IdentityUser<Guid>
{
    /// <summary>
    /// user name
    /// </summary>
    public string Name { get; set; } = "";
    /// <summary>
    /// user lastname
    /// </summary>
    public string LastName { get; set; } = "";
    /// <summary>
    /// profile creation time
    /// </summary>
    public DateTime Created { get; set; }
    /// <summary>
    /// profile update time
    /// </summary>
    public DateTime? Updated { get; set; }
}