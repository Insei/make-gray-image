namespace MakeGreyImageAPI.DTOs;
/// <summary>
/// Entity of the dto image
/// </summary>
public class LocalImageDTO : LocalImageBaseDTO
{
    /// <summary>
    /// Unique identifier of image
    /// </summary>
    public Guid Id { get; set; }
}