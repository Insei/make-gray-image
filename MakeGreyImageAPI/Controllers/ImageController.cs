using System;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using MakeGreyImageAPI.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakeGreyImageAPI.Controllers;

/// <summary>
/// Controller for working with the image
/// </summary>
[System.Runtime.Versioning.SupportedOSPlatform("windows")]
[Route("api/[controller]/")]
[ApiController]

public class ImageController : Controller
{
   private static IWebHostEnvironment _environment = null!;
   private readonly IImageManager _manager;

   /// <summary>
   /// Constructor of class ImageController
   /// </summary>
   /// <param name="environment">parameter for interacting with the environment in which the application is running</param>
   /// <param name="manager">the interface parameter for working with the image</param>
   public ImageController(IWebHostEnvironment environment, IImageManager manager)
   {
      _environment = environment;
      _manager = manager;
   }

   /// <summary>
   /// Converting an image to black and white format
   /// </summary>
   /// <param name="objFile">accepted image value</param>
   /// <returns></returns>
   [HttpPost]
   public async Task<IActionResult> ConvertImageToGrey([FromForm] IFormFile? objFile)
   {
      if (objFile == null) return Content("Failed");
      try
      {
         var bytes = await _manager.ConvertToGrey(objFile);
         if (objFile.Name.ToLower().Contains("jpeg") || objFile.Name.ToLower().Contains("jpg"))
         {
            return File(bytes, "image/jpeg");
         }
         return File(bytes, "image/png");
      }
      catch (Exception ex)
      {
         return Content(ex.Message);
      }
   }
}
