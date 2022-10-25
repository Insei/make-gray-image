using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MakeGreyImageAPI.Models;
/// <summary>
/// 
/// </summary>
public class AuthOptions
{
    /// <summary>
    /// 
    /// </summary>
    public const string Issuer = "MyAuthServer"; // издатель токена
    /// <summary>
    /// 
    /// </summary>
    public const string Audience = "MyAuthClient"; // потребитель токена
    /// <summary>
    /// 
    /// </summary>
    private const string Key = "mysupersecret_secretkey!123";   // ключ для шифрации
    /// <summary>
    /// 
    /// </summary>
    public const int Lifetime = 15; // время жизни токена - 1 минута
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}