using System;

namespace MakeGreyImageAPI.DTOs
{
    /// <summary>
    /// Entity dto class with information about actions with the account
    /// </summary>
    public class BaseDto
    {
        /// <summary>
        /// Id of the entity
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Created at DateTime UTC
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Last modified DateTime UTC
        /// </summary>
        public DateTime? Updated { get; set; }

        /// <summary>
        /// Create by user ID
        /// </summary>
        public virtual Guid CreatedBy { get; set; }

        /// <summary>
        /// Last modified by user ID
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}