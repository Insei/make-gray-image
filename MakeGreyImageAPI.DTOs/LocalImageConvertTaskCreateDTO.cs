using System;

namespace MakeGreyImageAPI.DTOs
{
    /// <summary>
    /// Entity dto of local image to create a converting task
    /// </summary>
    public class LocalImageConvertTaskCreateDto
    {
        /// <summary>
        /// Unique identifier of original image
        /// </summary>
        public Guid ImageId { get; set; }
    }
}