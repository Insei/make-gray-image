namespace MakeGreyImageAPI.Entities;

/// <summary>
/// Entity of local image for converting task
/// </summary>
public class LocalImageConvertTask
{
    /// <summary>
    /// Unique identifier of entity
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Unique identifier of original image
    /// </summary>
    public Guid InImageId { get; set; }
    
    /// <summary>
    ///  Unique identifier of converted image
    /// </summary>
    public Guid? OutImageId { get; set; }

    /// <summary>
    /// Converting status
    /// </summary>
    public string ConvertStatus { get; set; } = "";
}