using MakeGreyImageAPI.Interfaces;
using MakeGreyImageAPI.Services;

namespace MakeGreyImageAPI.Controllers;

using System.Drawing;
using System.Net;
using Microsoft.AspNetCore.Mvc;


[System.Runtime.Versioning.SupportedOSPlatform("windows")]
[Route("api/[controller]/")]
[ApiController]

public class ImageController : Controller
{
   private static IWebHostEnvironment _environment;
   private IImageService _service;

   public ImageController(IWebHostEnvironment environment, IImageService service)
   {
      _environment = environment;
      _service = service;
   }

   public class FileUploadApi
   {
      public IFormFile files { get; set; }
   }


   [HttpPost]
   public async Task<IActionResult> ConvertImageToGrey([FromForm] FileUploadApi objFile)
   {
      if (objFile.files.Length <= 0) return Content("Failed");
      try
      {
         if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
         {
            Directory.CreateDirectory((_environment.WebRootPath + "\\Upload\\"));
         }

         await using (var memoryStream = new MemoryStream())
         {
            await objFile.files.CopyToAsync(memoryStream);
            using (var img = Image.FromStream(memoryStream))
            {
               var greyImg = _service.ConvertToGrey(img);
               greyImg.Save(_environment.WebRootPath + "\\Upload\\" + objFile.files.FileName);
               var greyImgPath = Path.Combine(_environment.WebRootPath + "\\Upload\\" + objFile.files.FileName);
               var byteImg = await System.IO.File.ReadAllBytesAsync(greyImgPath);
               memoryStream.Flush();
               return File(byteImg, "image/jpeg");
            }
         }
      }
      catch (Exception ex)
      {
         return Content(ex.Message);
      }
   }
}
