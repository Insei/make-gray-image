namespace MakeGreyImageAPI.DTOs;
/// <summary>
/// Entity dto with information about user
/// </summary>
public class ApplicationUserDto
{
    /// <summary>
    /// User Id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// User login name
    /// </summary>
    public string UserName { get; set; } = "";
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
    /// <summary>
    /// User e-mail
    /// </summary>
    public string Email { get; set; } = "";
    /// <summary>
    /// User phone number
    /// </summary>
    public string PhoneNumber { get; set; } = "";

}