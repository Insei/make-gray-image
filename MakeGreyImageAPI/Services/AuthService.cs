using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace MakeGreyImageAPI.Services;
/// <summary>
/// 
/// </summary>
public class AuthService
{
   
    private readonly UserManager<ApplicationUser> _userManager;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userManager"></param>
    public AuthService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="login"></param>
    /// <param name="password"></param>
    public async Task<string?> GetToken(string login, string password)
    {
        var claim = await GetClaims(login, password);
        if (claim == null) return null;
        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            AuthOptions.Issuer,
            AuthOptions.Audience,
            notBefore: now,
            claims: claim.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="login"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    private async Task<ClaimsIdentity?> GetClaims(string login, string password)
    {
        var user = await _userManager.FindByNameAsync(login);
        if (user == null) return null;
        var result = await _userManager.CheckPasswordAsync(user, password);
        if (!result) return null;
        var claims = new List<Claim>
        {
            new (ClaimsIdentity.DefaultNameClaimType, user.UserName)
        };
        return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, null);
    }
}