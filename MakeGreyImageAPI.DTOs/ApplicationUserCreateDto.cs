namespace MakeGreyImageAPI.DTOs;
/// <summary>
/// entity dto with information for crete user
/// </summary>
public class ApplicationUserCreateDto
{
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
    /// user e-mail
    /// </summary>
    public string Email { get; set; } = "";
    /// <summary>
    /// user phone number
    /// </summary>
    public string PhoneNumber { get; set; } = "";
    /// <summary>
    /// user password
    /// </summary>
    public string Password { get; set; } = "";
}