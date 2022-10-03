namespace MakeGreyImageAPI.DTOs;

public class ImageConvertTaskCreateDTO
{
    private Guid ImageId { get; set; }
    public ConvertParameters Parameters { get; set; }
}