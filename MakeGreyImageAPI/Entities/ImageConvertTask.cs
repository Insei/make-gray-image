namespace MakeGreyImageAPI.Entities;

/// <summary>
/// 
/// </summary>
public class ImageConvertTask
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Guid InImageId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Guid? OutImageId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ConvertStatus { get; set; } = "";
}