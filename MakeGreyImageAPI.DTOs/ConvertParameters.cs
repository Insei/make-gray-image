using System.ComponentModel.DataAnnotations;
namespace MakeGreyImageAPI.DTOs;
/// <summary>
/// The class of required parameters during conversion
/// </summary>
public class ConvertParameters
{
    
    /// <summary>
    /// Image extension
    /// </summary>
    public string Extension { get; set; } = "";
    /// <summary>
    /// Image color
    /// </summary>
    public string Color { get; set; } = "";
}