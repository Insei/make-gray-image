using MakeGreyImageAPI.DTOs;
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
    public async Task<ApiResponse<LocalImageDTO>> Add([FromForm] IFormFile? objFile)
    {
       if (objFile == null) return new ApiResponse<LocalImageDTO>
       {
           Status = new Status
           {
               Message = "null object file, please repeat"
           }
       };
       try
       {
           var response = new ApiResponse<LocalImageDTO>
           {
               Data = await _service.Create(objFile)
           };
           return response;
       }
       catch(Exception e)
       {
           return new ApiResponse<LocalImageDTO>
           {
               Status = new Status{Message = e.Message}
           };
       }
    }
    /// <summary>
    /// Http request to get an entity by Id
    /// </summary>
    /// <param name="id">entity ID</param>
    /// <returns>Image entity</returns>
    [HttpGet("{id}")]
    public async Task<ApiResponse<LocalImageDTO>> GetById(Guid id)
    {
        var response = new ApiResponse<LocalImageDTO>
        {
            Data = await _service.GetById(id)!
        };
        return response;
    }
    
    /// <summary>
    /// Http request to remove an entity from DB
    /// </summary>
    /// <param name="id">entity ID</param>
    [HttpDelete("{id}")]
    public async Task<EmptyApiResponse> Delete([FromQuery]Guid id)
    {
        await _service.Delete(id);
        var response = new EmptyApiResponse
        {
            Status = new Status
            {
                Message = "Successful deleting"
            }
        };
        return response;
    }
    
    /// <summary>
    /// Http request to update entity data
    /// </summary>
    /// <param name="updateImage">entity with changes</param>
    /// <param name="id">entity ID</param>
    /// <returns>updated entity</returns>
    [HttpPut("{id}")]
    public async Task<ApiResponse<LocalImageDTO>> Update(LocalImageUpdateDTO updateImage, Guid id)
    {
        var response = new ApiResponse<LocalImageDTO>
        {
            Data = await _service.Update(updateImage, id)
        };
        return response;
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
    /// Http request to download entity
    /// </summary>
    /// <param name="id">entity ID</param>
    /// <returns>image entity</returns>
    [HttpGet("{id}/download")]
    public async Task<IActionResult> Download(Guid id)
    {
        var imageExtension = _service.GetById(id)?.Result.Extension;
        if (imageExtension == null) return Content("Failed");
        try
        {
            var byteImage = await _service.GetImageByte(id);
            if (imageExtension.ToLower().Contains("jpeg") || imageExtension.ToLower().Contains("jpg"))
            {
                if (byteImage != null) return File(byteImage, "image/jpeg");
            }

            if (byteImage != null) return File(byteImage, "image/png");
        }
        catch (Exception ex)
        {
            return Content(ex.Message);
        }
        throw new InvalidOperationException();
    }
}