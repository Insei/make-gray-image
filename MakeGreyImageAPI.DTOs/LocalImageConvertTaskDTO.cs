namespace MakeGreyImageAPI.DTOs;

public class LocalImageConvertTaskDTO 
{
    /// <summary>
    /// Unique identifier of LocalImageConvertTask entity
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Unique identifier of original image
    /// </summary>
    public Guid InImageId { get; set; }
    /// <summary>
    /// Unique identifier of converted image
    /// </summary>
    public Guid? OutImageId { get; set; }
    /// <summary>
    /// image parameters(color, extension)
    /// </summary>
    public ConvertParameters? Parameters { get; set; }
    /// <summary>
    /// image conversion status
    /// </summary>
    public string Status { get; set; } = "";
}