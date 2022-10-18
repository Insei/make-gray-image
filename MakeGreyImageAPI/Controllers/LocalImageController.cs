using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.DTOs.Results;
using MakeGreyImageAPI.DTOs.Sorts;
using MakeGreyImageAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakeGreyImageAPI.Controllers;

/// <summary>
/// Controller for working with original images
/// </summary>
[System.Runtime.Versioning.SupportedOSPlatform("windows")]
[Route("api/images/")]
[ApiController]

public class LocalImageController : Controller
{
    private static ImageService _service = null!;

    /// <summary>
    /// Constructor of class LocalImageController
    /// </summary>
    /// <param name="service">ImageService class</param>
    public LocalImageController(ImageService service)
    {
        _service = service;
    }

    /// <summary>
    /// Http request to add data to DB
    /// </summary>
    /// <param name="objFile">accepted file</param>
    /// <returns>image information</returns>
    [HttpPost("add")]
    public async Task<ActionResult<LocalImageDTO>> Add([FromForm] IFormFile? objFile)
    {
        if (objFile != null)
        {
            return Ok(await _service.Create(objFile));
        }
        return BadRequest();
    }
    
    /// <summary>
    /// Http request to get an entity by Id
    /// </summary>
    /// <param name="id">entity ID</param>
    /// <returns>Image entity</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LocalImageDTO>> GetById(Guid id)
    {
        var result = await _service.GetById(id);
        if (result != null) return Ok(result);
        return NotFound();
    }
    
    /// <summary>
    /// Http request to remove an entity from DB
    /// </summary>
    /// <param name="id">entity ID</param>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var image = await _service.GetById(id);
        if (image == null) return NotFound();
        await _service.Delete(id); 
        return NoContent();
    }
    /// <summary>
    /// Http request to get paginated list of entities
    /// </summary>
    /// <param name="pageNumber">the number of the displayed page</param>
    /// <param name="pageSize">number of items per page</param>
    /// <param name="orderBy">name of the sorting field</param>
    /// <param name="orderDirection">order direction</param>
    /// <param name="search">search string parameter</param>
    /// <returns>PaginatedResult list of LocalImageDTO entities</returns>
    [HttpGet("list")]
    public async Task<PaginatedResult<List<LocalImageDTO>>> GetList([FromQuery] int pageNumber = 0, int pageSize = 0,
        string? orderBy = "", string? orderDirection = "asc", string? search = "")
    {
        var directionEnum = SortDirection.Asc;
        if (orderDirection == "desc")
        {
            directionEnum = SortDirection.Desc;
        }
        return await _service.GetPaginatedList(pageNumber, pageSize, orderBy!, directionEnum, search!);
    }
    
    /// <summary>
    /// Http request to download image
    /// </summary>
    /// <param name="id">entity ID</param>
    /// <returns>image entity</returns>
    [HttpGet("{id}/download")]
    public async Task<IActionResult> Download(Guid id)
    {
        var image = await _service.GetById(id);
        if (image == null) return NotFound();
        var imageExtension = image.Extension;
       
        var byteImage = await _service.GetImageByte(id);
        var contentType = "image/png";
        if (imageExtension.ToLower().Contains("jpeg") || imageExtension.ToLower().Contains("jpg"))
        {
            contentType = "image/jpeg";
        }
        if (byteImage != null) return Ok(File(byteImage, contentType));
   
        return BadRequest();
    }
}