namespace MakeGreyImageAPI.DTOs;

public class LocalImageConvertTaskCreateDTO
{
    public Guid ImageId { get; set; }
    public ConvertParameters Parameters { get; set; }
}