using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MakeGreyImageAPI.Models;
/// <summary>
/// Client Options when the client library was instantiated
/// </summary>
public class AuthOptions
{
    /// <summary>
    /// Token publisher
    /// </summary>
    public const string Issuer = "MyAuthServer";
    /// <summary>
    /// Token consumer
    /// </summary>
    public const string Audience = "MyAuthClient";
    /// <summary>
    /// Encryption key
    /// </summary>
    private const string Key = "mysupersecret_secretkey!123";
    /// <summary>
    /// Token lifetime(in minutes)
    /// </summary>
    public const int Lifetime = 15;
    /// <summary>
    /// Method for getting symmetric security key
    /// </summary>
    /// <returns></returns>
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}