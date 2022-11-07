using MakeGreyImageAPI.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MakeGreyImageAPI.Entities;
/// <summary>
/// Entity with information about user
/// </summary>
public class ApplicationUser : IdentityUser<Guid>, IKeyEntity<Guid>, ICreatedAtTrackedEntity
{
    /// <summary>
    /// User name
    /// </summary>
    public string Name { get; set; } = "";
    /// <summary>
    /// User lastname
    /// </summary>
    public string LastName { get; set; } = "";
    /// <summary>
    /// Profile creation time
    /// </summary>
    public DateTime Created { get; set; }
    /// <summary>
    /// Profile update time
    /// </summary>
    public DateTime? Updated { get; set; }
}