namespace MakeGreyImageAPI.DTOs;

public class LocalImageConvertTaskDTO 
{
    public Guid Id { get; set; }
    public Guid InImageId { get; set; }
    public Guid? OutImageId { get; set; }
    public ConvertParameters? Parameters { get; set; }
    public string Status { get; set; } = "";
}