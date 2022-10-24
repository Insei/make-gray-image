namespace MakeGreyImageAPI.DTOs;
/// <summary>
/// entity dto with information for updating user
/// </summary>
public class ApplicationUserUpdateDto 
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
    /// user e-mail
    /// </summary>
    public string Email { get; set; } = "";
    /// <summary>
    /// user phone number
    /// </summary>
    public string PhoneNumber { get; set; } = "";
}