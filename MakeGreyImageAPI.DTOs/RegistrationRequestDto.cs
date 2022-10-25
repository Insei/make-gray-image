namespace MakeGreyImageAPI.DTOs;
/// <summary>
/// 
/// </summary>
public class RegistrationRequestDto
{
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
    /// User e-mail
    /// </summary>
    public string Email { get; set; } = "";
    /// <summary>
    /// User password
    /// </summary>
    public string Password { get; set; } = "";
}