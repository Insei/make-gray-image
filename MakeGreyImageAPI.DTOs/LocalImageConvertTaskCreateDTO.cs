namespace MakeGreyImageAPI.DTOs;
/// <summary>
/// Entity dto of local image to create a converting task
/// </summary>
public class LocalImageConvertTaskCreateDTO
{
    /// <summary>
    /// Unique identifier of original image
    /// </summary>
    public Guid ImageId { get; set; }
     /// <summary>
     /// Image parameters(color, extension)
     /// </summary>
     public ConvertParameters? Parameters { get; set; }
}