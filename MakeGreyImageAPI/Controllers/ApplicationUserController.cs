using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.DTOs.Results;
using MakeGreyImageAPI.DTOs.Sorts;
using MakeGreyImageAPI.Interfaces;
using MakeGreyImageAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakeGreyImageAPI.Controllers;
/// <summary>
/// controller for working with the user
/// </summary>
[Route("api/users/")]
[ApiController]
public class ApplicationUserController : Controller
{
    private readonly ApplicationUserService _service;
    /// <summary>
    /// ApplicationUserController controller
    /// </summary>
    /// <param name="service">ApplicationUserService</param>
    public ApplicationUserController(ApplicationUserService service)
    {
        _service = service;
    }
    /// <summary>
    /// Http request to create data in DB
    /// </summary>
    /// <param name="createDto">DTO for creating user data</param>
    /// <returns>result of the work</returns>
    ///  <response code="200">returns the newly created item</response>
    /// <response code="400">if the item is null</response>
    [HttpPost]
    public async Task<ActionResult<ApplicationUserDto>> Create([FromBody] ApplicationUserCreateDto createDto)
    {
        var dto = await _service.Create(createDto);
        if (dto != null) return Ok(dto);
        return BadRequest();
    }
    /// <summary>
    /// Http request to update data in DB
    /// </summary>
    /// <param name="updateDto">DTO for updating user data</param>
    /// <param name="id">entity Id</param>
    /// <returns>result of the work</returns>
    /// <response code="200">returns the updated item</response>
    /// <response code="400">if the item is null</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApplicationUserDto>> Update([FromBody] ApplicationUserUpdateDto updateDto, Guid id)
    {
       var dto =  await _service.Update(updateDto, id);
       if (dto != null) return Ok(dto);
       return BadRequest();
    }
    /// <summary>
    /// Http request to remove an entity from DB
    /// </summary>
    /// <param name="id">entity Id</param>
    /// <returns>http result of the work</returns>
    /// <response code="200">Returns the found item</response>
    /// <response code="404">If the item not found</response>
    /// <response code="204">Item successfully deleted</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var dto = await _service.GetById(id);
        if (dto == null) return NotFound();
        await _service.Delete(id); 
        return NoContent();
    }
    /// <summary>
    /// Http request to get an entity by Id
    /// </summary>
    /// <param name="id">entity Id</param>
    /// <returns>user entity</returns>
    /// <response code="200">returns the found item</response>
    /// <response code="404">if the item not found</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApplicationUserDto>> GetById(Guid id)
    {
        var result = await _service.GetById(id);
        if (result != null) return Ok(result);
        return NotFound();
    }
    /// <summary>
    /// Http request to get paginated list of entities
    /// </summary>
    /// <param name="pageNumber">the number of the displayed page</param>
    /// <param name="pageSize">number of items per page</param>
    /// <param name="orderBy">name of the sorting field</param>
    /// <param name="orderDirection">order direction</param>
    /// <param name="search">search string parameter</param>
    /// <returns>PaginatedResult list of ApplicationUser entities</returns>
    [HttpGet("list")]
    public async Task<PaginatedResult<List<ApplicationUserDto>>> GetList([FromQuery] int pageNumber = 0, int pageSize = 0,
        string? orderBy = "", string? orderDirection = "asc", string? search = "")
    {
        var directionEnum = SortDirection.Asc;
        if (orderDirection == "desc")
        {
            directionEnum = SortDirection.Desc;
        }
        return await _service.GetPaginatedList(pageNumber, pageSize, orderBy!, directionEnum, search!);
    }
}