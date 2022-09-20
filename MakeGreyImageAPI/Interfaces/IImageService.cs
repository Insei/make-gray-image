using System.Drawing;

namespace MakeGreyImageAPI.Interfaces;

public interface IImageService
{
    public Image ConvertToGrey (Image image);
}