using MakeGreyImageAPI.DTOs;
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
    public async Task<ApiResponse<LocalImageConvertTaskDTO>> Add([FromForm] LocalImageConvertTaskCreateDTO? imageConvertTask)
    {
        if (imageConvertTask == null) return new ApiResponse<LocalImageConvertTaskDTO>()
        {
            Status = new Status()
            {
                Message = "null object file, please repeat"
            }
        };
        try
        {
            var response = new ApiResponse<LocalImageConvertTaskDTO>()
            {
                Data = await _imageConvertService.Create(imageConvertTask)
            };
            return response;
        }
        catch(Exception e)
        {
            return new ApiResponse<LocalImageConvertTaskDTO>()
            {
                Status = new Status(){Message = e.Message}
            };
        }
    }
    /// <summary>
    /// Http request to get an entity by Id
    /// </summary>
    /// <param name="id">entity ID</param>
    /// <returns>image entity</returns>
    [HttpGet("{id}")]
    public async Task<ApiResponse<LocalImageConvertTaskDTO>> GetById(Guid id)
    {
        var localImageConvert = await _imageConvertService.GetById(id);
        var response = new ApiResponse<LocalImageConvertTaskDTO>()
        {
            Data = localImageConvert
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
        await _imageConvertService.Delete(id);
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
    /// <param name="updateImage">new entity for updating</param>
    /// <param name="id">entity ID</param>
    /// <returns>updated entity</returns>
    [HttpPut("{id}")]
    public async Task<ApiResponse<LocalImageConvertTaskDTO>> Update(LocalImageConvertTaskDTO updateImage, Guid id)
    {
        var response = new ApiResponse<LocalImageConvertTaskDTO>()
        {
            Data = await _imageConvertService.Update(updateImage, id)
        };
        return response;
    }
    
  
    /// <summary>
    /// Http request to get the list of entities
    /// </summary>
    /// <param name="search">parameter for searching from entities</param>
    /// <returns>list of entities</returns>
    [HttpGet("list")]
    public async Task<ApiResponse<List<LocalImageConvertTaskDTO>>> GetList([FromQuery]string search)
    {
        var response = new ApiResponse<List<LocalImageConvertTaskDTO>>()
        {
            Data = await _imageConvertService.GetList(search)
        };
        return response;
    }

    /// <summary>
    /// Http request to download entity
    /// </summary>
    /// <param name="id">entity ID</param>
    /// <returns>image entity</returns>
    /// <exception cref="InvalidOperationException"></exception>
    [HttpGet("{id}/download")]
    public async Task<IActionResult> Download(Guid id)
    {
        var imageExtension = _imageService.GetById(id)?.Result.Extension;
        if (imageExtension == null) return Content("Failed");
        try
        {
            var byteImage = await _imageService.GetImageByte(id);
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