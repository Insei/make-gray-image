namespace MakeGreyImageAPI.Entities;

/// <summary>
/// Image entity
/// </summary>
public class LocalImage : BaseEntity<Guid, Guid>
{
    /// <summary>
    /// Image name
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// Image in byte format
    /// </summary>
    public byte[]? Image { get; set; }
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