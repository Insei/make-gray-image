using System.Drawing;
using MakeGreyImageAPI.Interfaces;

namespace MakeGreyImageAPI.Services;

[System.Runtime.Versioning.SupportedOSPlatform("windows")]
public class ImageService : IImageService
{
    public Image ConvertToGrey(Image image) 
    {
        var bmp = (Bitmap)image;   
        var outputImg = new Bitmap(bmp.Width, bmp.Height);
        for (int j = 0; j < bmp.Height; j++)
        for (int i = 0; i < bmp.Width; i++)
        {
            var pixel = (uint)bmp.GetPixel(x: i, j).ToArgb();
            float r = (pixel & 0x00FF0000) >> 16; 
            float g = (pixel & 0x0000FF00) >> 8; 
            float b = pixel & 0x000000FF; 
            r = g = b = (r + g + b) / 3.0f;
            var newPixel = 0xFF000000 | ((uint)r << 16) | ((uint)g << 8) | (uint)b;
            outputImg.SetPixel(i, j, Color.FromArgb((int)newPixel));
        }
        return outputImg;
    }
}