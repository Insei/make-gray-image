namespace MakeGreyImageAPI.DTOs;
/// <summary>
/// Base dto entity of the image
/// </summary>
public class LocalImageDto : BaseDto
{
    /// <summary>
    /// Image name
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// Image extension
    /// </summary>
    public string Extension { get; set; } = null!;
    /// <summary>
    /// Image width
    /// </summary>
    public int Width { get; set; }
    /// <summary>
    /// Image height
    /// </summary>
    public int Height { get; set; }
}