using AutoMapper;
using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.DTOs.Results;
using MakeGreyImageAPI.DTOs.Sorts;
using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Infrastructure.Context;
using MakeGreyImageAPI.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MakeGreyImageAPI.Services;
/// <summary>
/// service for working with user entity
/// </summary>
public class ApplicationUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IGenericRepository _repository;
    // private readonly SignInManager<ApplicationUser> _signInManager;
    // private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IMapper _mapper;
   /// <summary>
   /// constructor of ApplicationUserService class
   /// </summary>
   /// <param name="userManager">UserManager</param>
   /// <param name="mapper">IMapper</param>
   /// <param name="repository">IGenericRepository</param>
    public ApplicationUserService(UserManager<ApplicationUser> userManager, IMapper mapper, IGenericRepository repository)
    {
        _userManager = userManager;
        // _signInManager = signInManager;
        // _roleManager = roleManager;
        _mapper = mapper;
        _repository = repository;
    }
    /// <summary>
    /// create a new entity of user
    /// </summary>
    /// <param name="appUserCreateDto">DTO for creating user</param>
    /// <returns>ApplicationUserDto</returns>
    public async Task<ApplicationUserDto?> Create(ApplicationUserCreateDto appUserCreateDto)
    {
        var appUser = _mapper.Map<ApplicationUser>(appUserCreateDto);
        var result = await _userManager.CreateAsync(appUser);
        if(!result.Succeeded)  return null;
        var passwordResult = await _userManager.AddPasswordAsync(appUser, appUserCreateDto.Password);
        if (!passwordResult.Succeeded) return null;
        return _mapper.Map<ApplicationUserDto>(appUser);
    }
    /// <summary>
    /// update ApplicationUser entity
    /// </summary>
    /// <param name="updateDto">new entity for updating</param>
    /// <param name="id">entity ID</param>
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
    /// <summary>
    /// delete Entity
    /// </summary>
    /// <param name="id">entity Id</param>
    public async Task Delete(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if(user != null)  await _userManager.DeleteAsync(user);
    }
    /// <summary>
    /// get DTO Entity by ID
    /// </summary>
    /// <param name="id">entity Id</param>
    /// <returns>ApplicationUserDto</returns>
    public async Task<ApplicationUserDto?> GetById(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        return user == null ? null : _mapper.Map<ApplicationUserDto>(user);
    }

    /// <summary>
    /// get paginated entity list
    /// </summary>
    /// <param name="pageNumber">page number</param>
    /// <param name="pageSize">page size</param>
    /// <param name="orderBy">sorting</param>
    /// <param name="orderDirection">sorting direction</param>
    /// <param name="search">search string</param>
    /// <returns>paginated list of entities</returns>
    public async Task<PaginatedResult<List<ApplicationUserDto>>> GetPaginatedList(int pageNumber = 0, int pageSize = 0,
        string orderBy = "", SortDirection orderDirection = SortDirection.Asc, string search = "")
    {
        var users = await _userManager.Users.ToListAsync();
        var count = _userManager.Users.Count();
        var pagination = Pagination.Generate(pageNumber,pageSize,count);
        var entities = await _repository.GetPaginatedList<ApplicationUser>(search, orderBy, orderDirection, 
            pagination.CurrentPage, pagination.PageSize);
        
        var result = new PaginatedResult<List<ApplicationUserDto>>
        {
            Pagination = pagination,
            Data = _mapper.Map<List<ApplicationUserDto>>(entities)
        };
        return result;
    }
}