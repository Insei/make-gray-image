using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakeGreyImageAPI.Controllers;
/// <summary>
/// Handles registration and authentication of a new user
/// </summary>
[Route("api/auth/")]
[ApiController]
public class AuthController : Controller
{
    private readonly AuthService _authService;
    private readonly ApplicationUserService _applicationUserService;
    /// <summary>
    /// AuthController constructor
    /// </summary>
    /// <param name="authService">AuthService</param>
    /// <param name="applicationUserService">ApplicationUserService</param>
    public AuthController(AuthService authService, ApplicationUserService applicationUserService)
    {
        _authService = authService;
        _applicationUserService = applicationUserService;
    }
    /// <summary>
    /// Registration new user
    /// </summary>
    /// <param name="createDto">Dto information for registration new user</param>
    /// <returns>Information about user</returns>
    [HttpPost("registration")]
    public async Task<ActionResult<ApplicationUserDto>> Registration(RegistrationRequestDto createDto)
    {
      var dto = await _applicationUserService.CreateUser(createDto);
      if (dto == null) return BadRequest();
      return Ok(dto);
    }
    /// <summary>
    /// Getting Jwt token
    /// </summary>
    /// <param name="login">User login</param>
    /// <param name="password">User password</param>
    /// <returns>Token</returns>
    [HttpGet("token")]
    public async Task<ActionResult<string>> GetJwtToken(string login, string password)
    {
        var token = await _authService.GetToken(login, password);
        if (token == null) return BadRequest();
        return Ok(token);
    }
}