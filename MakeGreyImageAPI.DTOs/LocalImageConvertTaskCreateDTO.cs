namespace MakeGreyImageAPI.DTOs;

public class LocalImageConvertTaskCreateDTO
{
    /// <summary>
    /// Unique identifier of original image
    /// </summary>
    public Guid ImageId { get; set; }
     /// <summary>
     /// image parameters(color, extension)
     /// </summary>
     public ConvertParameters? Parameters { get; set; }
}