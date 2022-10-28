using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MakeGreyImageAPI.Models;
/// <summary>
/// Client Options when the client library was instantiated
/// </summary>
public class AuthOptions
{   
    /// <summary>
    /// AuthOptions constructor
    /// </summary>
    /// <param name="issuer">Token publisher</param>
    /// <param name="audience">Token consumer</param>
    /// <param name="key">Encryption key</param>
    /// <param name="lifetime">Token lifetime(in minutes)</param>
    public AuthOptions(string issuer, string audience, string key, int lifetime)
    {
        Issuer = issuer;
        Audience = audience;
        Key = key;
        Lifetime = lifetime;
    }
    /// <summary>
    /// AuthOptions constructor
    /// </summary>
    public AuthOptions(){}
    /// <summary>
    /// Token publisher
    /// </summary>
    public string Issuer { get; set; } = "";
    /// <summary>
    /// Token consumer
    /// </summary>
    public string Audience { get; set; } = "";
    /// <summary>
    /// Encryption key
    /// </summary>
    public string Key { get; set; } = "";
    /// <summary>
    /// Token lifetime(in minutes)
    /// </summary>
    public int Lifetime { get; set; }
    
    /// <summary>
    /// Method for getting symmetric security key
    /// </summary>
    /// <returns>Symmetric security key</returns>
    public SymmetricSecurityKey GetSymmetricSecurityKey(string key)
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
    }
}