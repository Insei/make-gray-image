using MakeGreyImageAPI.Entities;

namespace MakeGreyImageAPI.Interfaces;

/// <summary>
///  interface of methods for working with images
/// </summary>
public interface IImageManager
{
    /// <summary>
    /// A method for converting an image to black and white format
    /// </summary>
    /// <param name="image">parameter of the received image</param>
    /// <returns>Return image in byte array format</returns>
    public Task<byte[]> ConvertToGrey(LocalImage image);
}