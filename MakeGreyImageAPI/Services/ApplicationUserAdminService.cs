using AutoMapper;
using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.DTOs.Results;
using MakeGreyImageAPI.DTOs.Sorts;
using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Interfaces;

namespace MakeGreyImageAPI.Services;
/// <summary>
/// Service for working with user entity
/// </summary>
public class ApplicationUserAdminService
{
    private readonly IGenericRepository _repository;
    // private readonly SignInManager<ApplicationUser> _signInManager;
    // private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IMapper _mapper;
    
   /// <summary>
   /// Constructor of ApplicationUserService class
   /// </summary>
   /// <param name="mapper">IMapper</param>
   /// <param name="repository">IGenericRepository</param>
    public ApplicationUserAdminService(IMapper mapper, IGenericRepository repository)
    {
        // _signInManager = signInManager;
        // _roleManager = roleManager;
        _mapper = mapper;
        _repository = repository;
    }
    /// <summary>
    /// Create a new entity of user
    /// </summary>
    /// <param name="appUserCreateDto">DTO for creating user</param>
    /// <returns>ApplicationUserDto</returns>
    public async Task<ApplicationUserDto?> Create(ApplicationUserCreateDto appUserCreateDto)
    {
        var appUser = _mapper.Map<ApplicationUser>(appUserCreateDto);
        var data = System.Text.Encoding.ASCII.GetBytes(appUserCreateDto.Password);
        data = new System.Security.Cryptography.HMACSHA256().ComputeHash(data);
        var hash = System.Text.Encoding.ASCII.GetString(data);
        appUser.PasswordHash = hash;
        var result = await _repository.Insert(appUser);
        return _mapper.Map<ApplicationUserDto>(result);
    }
    /// <summary>
    /// Update ApplicationUser entity
    /// </summary>
    /// <param name="updateDto">New entity for updating</param>
    /// <param name="id">Entity ID</param>
    /// <returns>ApplicationUserDto</returns>
    public async Task<ApplicationUserDto?> Update(ApplicationUserUpdateDto updateDto, Guid id)
    {
        var user = await _repository.GetById<ApplicationUser>(id);
        if (user == null) return null;
        var updatedUser = _mapper.Map(updateDto, user);
        var result = await _repository.Update(updatedUser);
        return _mapper.Map<ApplicationUserDto>(result);
    }
    /// <summary>
    /// Delete Entity
    /// </summary>
    /// <param name="id">Entity Id</param>
    public async Task Delete(Guid id)
    {
        var user = await _repository.GetById<ApplicationUser>(id);
        if(user != null) _repository.Delete(user);
    }
    /// <summary>
    /// Get DTO Entity by ID
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <returns>ApplicationUserDto</returns>
    public async Task<ApplicationUserDto?> GetById(Guid id)
    {
        var user = await _repository.GetById<ApplicationUser>(id);
        return user == null ? null : _mapper.Map<ApplicationUserDto>(user);
    }

    /// <summary>
    /// Get paginated entity list
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="orderBy">Sorting</param>
    /// <param name="orderDirection">Sorting direction</param>
    /// <param name="search">Search string</param>
    /// <returns>Paginated list of entities</returns>
    public async Task<PaginatedResult<List<ApplicationUserDto>>> GetPaginatedList(int pageNumber = 0, int pageSize = 0,
        string orderBy = "", SortDirection orderDirection = SortDirection.Asc, string search = "")
    {
        var count = await _repository.Count<ApplicationUser>();
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