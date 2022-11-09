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
    private readonly IGenericRepository<Guid, ApplicationUser> _applicationUserRepository;
    private readonly IMapper _mapper;
    
   /// <summary>
   /// Constructor of ApplicationUserService class
   /// </summary>
   /// <param name="mapper">IMapper</param>
   /// <param name="applicationUserRepository"></param>
    public ApplicationUserAdminService(IMapper mapper, IGenericRepository<Guid, ApplicationUser> applicationUserRepository)
    {
        _mapper = mapper;
        _applicationUserRepository = applicationUserRepository;
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
        var result = await _applicationUserRepository.Insert(appUser);
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
        var user = await _applicationUserRepository.GetById(id);
        if (user == null) return null;
        var updatedUser = _mapper.Map(updateDto, user);
        var result = await _applicationUserRepository.Update(updatedUser);
        return _mapper.Map<ApplicationUserDto>(result);
    }
    /// <summary>
    /// Delete Entity
    /// </summary>
    /// <param name="id">Entity Id</param>
    public async Task Delete(Guid id)
    {
        var user = await _applicationUserRepository.GetById(id);
        if(user != null) _applicationUserRepository.Delete(id);
    }
    /// <summary>
    /// Get DTO Entity by ID
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <returns>ApplicationUserDto</returns>
    public async Task<ApplicationUserDto?> GetById(Guid id)
    {
        var user = await _applicationUserRepository.GetById(id);
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
        var count = await _applicationUserRepository.Count();
        var pagination = Pagination.Generate(pageNumber,pageSize,count);
        var entities = await _applicationUserRepository.GetList(search, orderBy, orderDirection, 
            pagination.CurrentPage, pagination.PageSize);
        
        var result = new PaginatedResult<List<ApplicationUserDto>>(_mapper.Map<List<ApplicationUserDto>>(entities))
        {
            Pagination = pagination
        };
        return result;
    }
}