namespace MakeGreyImageAPI.DTOs;
/// <summary>
/// entity dto with information about user
/// </summary>
public class ApplicationUserDto
{
    /// <summary>
    /// user Id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// user login name
    /// </summary>
    public string UserName { get; set; } = "";
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

    /// <summary>
    /// user e-mail
    /// </summary>
    public string Email { get; set; } = "";

    /// <summary>
    /// user phone number
    /// </summary>
    public string PhoneNumber { get; set; } = "";

}