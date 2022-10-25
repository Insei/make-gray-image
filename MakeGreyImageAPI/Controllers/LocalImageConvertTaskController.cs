using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.DTOs.Results;
using MakeGreyImageAPI.DTOs.Sorts;
using MakeGreyImageAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeGreyImageAPI.Controllers;


/// <summary>
/// Controller for working with converted images
/// </summary>
[System.Runtime.Versioning.SupportedOSPlatform("windows")]
[Route("api/imagesConvert/")]
[ApiController]
[Authorize]
public class LocalImageConvertTaskController : Controller
{
    private readonly LocalImageConvertTaskService _imageConvertService;

    /// <summary>
    /// Constructor of class LocalImageConvertTaskController
    /// </summary>
    /// <param name="imageConvertService">LocalImageConvertTaskService class</param>
    public LocalImageConvertTaskController(LocalImageConvertTaskService imageConvertService)
    {
        _imageConvertService = imageConvertService;
    }

    /// <summary>
    /// Http request to add data to DB
    /// </summary>
    /// <param name="imageConvertTask">Entity of LocalImageConvertTask</param>
    /// <returns>Image information</returns>
    /// <response code="200">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    [HttpPost]
    public async Task<ActionResult<LocalImageConvertTaskDTO>> Add([FromBody] LocalImageConvertTaskCreateDTO? imageConvertTask)
    {
        if (imageConvertTask == null) return BadRequest();
        return Ok(await _imageConvertService.Create(imageConvertTask));
    }
    /// <summary>
    /// Http request to get an entity by Id
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <returns>Image entity</returns>
    /// <response code="200">Returns the found item</response>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        return Ok(await _imageConvertService.GetById(id));
    }
    
    /// <summary>
    /// Http request to remove an entity from DB
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <response code="204">Item successfully deleted</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromQuery]Guid id)
    {
        await _imageConvertService.Delete(id);
        return NoContent();
    }
    /// <summary>
    /// Http request to get paginated list of entities
    /// </summary>
    /// <param name="pageNumber">The number of the displayed page</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="orderBy">Name of the sorting field</param>
    /// <param name="orderDirection">Order direction</param>
    /// <param name="search">Search string parameter</param>
    /// <returns>PaginatedResult list of LocalImageConvertTaskDTO entities</returns>
    [HttpGet("list")]
    public async Task<PaginatedResult<List<LocalImageConvertTaskDTO>>> GetList([FromQuery] int pageNumber = 0, 
        int pageSize = 0, string? orderBy = "", string? orderDirection = "asc", string? search = "")
    {
        var directionEnum = SortDirection.Asc;
        if (orderDirection == "desc")
        {
            directionEnum = SortDirection.Desc;
        }
        return await _imageConvertService.GetPaginatedList(pageNumber, pageSize, orderBy!, directionEnum, search!);
    }
}