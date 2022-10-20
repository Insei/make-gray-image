namespace MakeGreyImageAPI.DTOs;
/// <summary>
/// Entity dto of local image for converting task
/// </summary>
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
    /// Image parameters(color, extension)
    /// </summary>
    public ConvertParameters? Parameters { get; set; }
    /// <summary>
    /// Image conversion status
    /// </summary>
    public string Status { get; set; } = "";
}