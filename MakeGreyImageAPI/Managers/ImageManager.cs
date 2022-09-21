using System.Drawing;
using System.Drawing.Imaging;
using MakeGreyImageAPI.Interfaces;

namespace MakeGreyImageAPI.Managers;

/// <summary>
/// class ImageManager for working with images
/// </summary>
public class ImageManager : IImageManager
{
    /// <summary>
    /// A method for converting an image to black and white format
    /// </summary>
    /// <param name="image">parameter of the received image</param>
    /// <returns>Return image in byte array format</returns>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public async Task<byte[]> ConvertToGrey(IFormFile image) 
    { 
        await using (var memoryStream = new MemoryStream())
        {
            await image.CopyToAsync(memoryStream);
            using (var img = Image.FromStream(memoryStream))
            {
                var bmpImg = (Bitmap)img;
                var greyImg = new Bitmap(bmpImg.Width, bmpImg.Height);
                for (int j = 0; j < bmpImg.Height; j++)
                for (int i = 0; i < bmpImg.Width; i++)
                {
                    var pixel = (uint)bmpImg.GetPixel(x: i, j).ToArgb();
                    float r = (pixel & 0x00FF0000) >> 16; 
                    float g = (pixel & 0x0000FF00) >> 8; 
                    float b = pixel & 0x000000FF; 
                    r = g = b = (r + g + b) / 3.0f;
                    var newPixel = 0xFF000000 | ((uint)r << 16) | ((uint)g << 8) | (uint)b;
                    greyImg.SetPixel(i, j, Color.FromArgb((int)newPixel));
                } 
                greyImg.Save(memoryStream, ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
        }
    }
}