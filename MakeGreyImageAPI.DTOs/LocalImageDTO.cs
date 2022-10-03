namespace MakeGreyImageAPI.DTOs;

public class LocalImageDTO : LocalImageBaseDTO
{
    /// <summary>
    /// Unique identifier of image
    /// </summary>
    public Guid Id { get; set; }
}