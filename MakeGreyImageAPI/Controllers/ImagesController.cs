using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Interfaces;
using MakeGreyImageAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakeGreyImageAPI.Controllers;

/// <summary>
/// 
/// </summary>
[System.Runtime.Versioning.SupportedOSPlatform("windows")]
[Route("api/[controller]/")]
[ApiController]

public class ImagesController : Controller
{
    private static IWebHostEnvironment _environment = null!;
    private static ImageService _service = null!;

    /// <summary>
    /// Constructor of class LocalImageController
    /// </summary>
    /// <param name="environment">parameter for interacting with the environment in which the application is running</param>
    /// <param name="service"></param>
    public LocalImageController(IWebHostEnvironment environment, ImageService service)
    {
        _environment = environment;
        _service = service;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="objFile"></param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task<ApiResponse<LocalImageDTO>> Add([FromForm] IFormFile? objFile)
    {
       if (objFile == null) return new ApiResponse<LocalImageDTO>()
       {
           Status = new Status()
           {
               Message = "null object file, please repeat"
           }
       };
       try
       {
           var response = new ApiResponse<LocalImageDTO>()
           {
               Data = await _service.Create(objFile)
           };
           return response;
       }
       catch(Exception e)
       {
           return new ApiResponse<LocalImageDTO>()
           {
               Status = new Status(){Message = e.Message}
           };
       }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ApiResponse<LocalImageDTO>> GetById([FromForm]Guid id)
    {
        var image = await _service.GetById(id)!;
        var response = new ApiResponse<LocalImageDTO>()
        {
            Data = image
        };
        return response;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
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
    /// 
    /// </summary>
    /// <param name="updateImage"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ApiResponse<LocalImageDTO>> Update(LocalImageUpdateDTO updateImage, Guid id)
    {
        var response = new ApiResponse<LocalImageDTO>()
        {
            Data = await _service.Update(updateImage, id)
        };
        return response;
    }
    
  
    /// <summary>
    /// 
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<ApiResponse<List<LocalImageDTO>>> GetList([FromQuery]string search)
    {
        var response = new ApiResponse<List<LocalImageDTO>>()
        {
            Data = await _service.GetList(search)
        };
        return response;
    }
}