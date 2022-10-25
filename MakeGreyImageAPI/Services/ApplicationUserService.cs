using AutoMapper;
using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MakeGreyImageAPI.Services;
/// <summary>
/// 
/// </summary>
public class ApplicationUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="mapper"></param>
    public ApplicationUserService(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    /// <summary>
    /// Create a new entity of user
    /// </summary>
    /// <param name="createDto">DTO for creating user</param>
    /// <returns>ApplicationUserDto</returns>
    public async Task<ApplicationUserDto?> CreateUser(RegistrationRequestDto createDto)
    {
        var appUser = _mapper.Map<ApplicationUser>(createDto);
        var result = await _userManager.CreateAsync(appUser);
        if(!result.Succeeded)  return null;
        var passwordResult = await _userManager.AddPasswordAsync(appUser, createDto.Password);
        if (!passwordResult.Succeeded) return null;
        return _mapper.Map<ApplicationUserDto>(appUser);
    }
    /// <summary>
    /// Update ApplicationUser entity
    /// </summary>
    /// <param name="updateDto">New entity for updating</param>
    /// <param name="id">Entity ID</param>
    /// <returns>ApplicationUserDto</returns>
    public async Task<ApplicationUserDto?> Update(ApplicationUserUpdateDto updateDto, Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null) return null;
        var updatedUser = _mapper.Map(updateDto, user);
        var result = await _userManager.UpdateAsync(updatedUser);
        if (!result.Succeeded) return null;
        return _mapper.Map<ApplicationUserDto>(updatedUser);
    }
}