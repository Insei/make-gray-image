namespace MakeGreyImageAPI.DTOs
{
    /// <summary>
    /// Entity dto with information for updating user
    /// </summary>
    public class ApplicationUserUpdateDto
    {
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// User lastname
        /// </summary>
        public string LastName { get; set; } = "";

        /// <summary>
        /// User e-mail
        /// </summary>
        public string Email { get; set; } = "";

        /// <summary>
        /// User phone number
        /// </summary>
        public string PhoneNumber { get; set; } = "";
    }
}