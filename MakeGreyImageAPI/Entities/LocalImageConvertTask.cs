namespace MakeGreyImageAPI.Entities;

/// <summary>
/// entity of local image for converting task
/// </summary>
public class LocalImageConvertTask
{
    /// <summary>
    /// unique identifier of entity
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// unique identifier of original image
    /// </summary>
    public Guid InImageId { get; set; }
    
    /// <summary>
    ///  unique identifier of converted image
    /// </summary>
    public Guid? OutImageId { get; set; }

    /// <summary>
    /// converting status
    /// </summary>
    public string ConvertStatus { get; set; } = "";
}