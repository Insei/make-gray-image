using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.DTOs.Results;
using MakeGreyImageAPI.DTOs.Sorts;
using MakeGreyImageAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakeGreyImageAPI.Controllers;


/// <summary>
/// Controller for working with converted images
/// </summary>
[System.Runtime.Versioning.SupportedOSPlatform("windows")]
[Route("api/imagesConvert/")]
[ApiController]

public class LocalImageConvertTaskController : Controller
{
    private readonly LocalImageConvertTaskService _imageConvertService;
    private readonly ImageService _imageService;

    /// <summary>
    /// Constructor of class LocalImageConvertTaskController
    /// </summary>
    /// <param name="imageConvertService">LocalImageConvertTaskService class</param>
    /// <param name="imageService">ImageService class</param>
    public LocalImageConvertTaskController(LocalImageConvertTaskService imageConvertService, ImageService imageService)
    {
        _imageConvertService = imageConvertService;
        _imageService = imageService;
    }

    /// <summary>
    /// Http request to add data to DB
    /// </summary>
    /// <param name="imageConvertTask"></param>
    /// <returns>image information</returns>
    [HttpPost]
    public async Task<ActionResult<LocalImageConvertTaskDTO>> Add([FromBody] LocalImageConvertTaskCreateDTO? imageConvertTask)
    {
        if (imageConvertTask == null) return BadRequest();
        try
        {
            return Ok(await _imageConvertService.Create(imageConvertTask));
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Http request to get an entity by Id
    /// </summary>
    /// <param name="id">entity ID</param>
    /// <returns>image entity</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        return Ok(await _imageConvertService.GetById(id));
    }
    
    /// <summary>
    /// Http request to remove an entity from DB
    /// </summary>
    /// <param name="id">entity ID</param>
    [HttpDelete("{id}")]
    public async Task Delete([FromQuery]Guid id)
    {
        await _imageConvertService.Delete(id);
        Ok("Successfully");
    }
    /// <summary>
    /// Http request to get paginated list of entities
    /// </summary>
    /// <param name="pageNumber">the number of the displayed page</param>
    /// <param name="pageSize">number of items per page</param>
    /// <param name="orderBy">name of the sorting field</param>
    /// <param name="orderDirection">order direction</param>
    /// <param name="search">search string parameter</param>
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