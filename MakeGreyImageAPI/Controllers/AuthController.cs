using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.Models;
using MakeGreyImageAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeGreyImageAPI.Controllers;
/// <summary>
/// 
/// </summary>
[Route("api/auth/")]
[ApiController]
public class AuthController : Controller
{
    private readonly AuthService _authService;
    private readonly ApplicationUserService _applicationUserService;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="authService"></param>
    /// <param name="applicationUserService"></param>
    public AuthController(AuthService authService, ApplicationUserService applicationUserService)
    {
        _authService = authService;
        _applicationUserService = applicationUserService;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPost("registration")]
    public async Task<ActionResult> Registration(RegistrationRequestDto createDto)
    {
      var dto = await _applicationUserService.CreateUser(createDto);
      if (dto == null) return BadRequest();
      return Ok(dto);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="login"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    [HttpGet("token")]
    public async Task<ActionResult> GetJwtToken(string login, string password)
    {
        var token = await _authService.GetToken(login, password);
        if (token == null) return BadRequest();
        return Ok(token);
    }
}