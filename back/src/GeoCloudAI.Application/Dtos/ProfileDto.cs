using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    /// <summary>
    /// This class represents a Data Transfer Object (DTO) for a profile.
    /// </summary>
    public class ProfileDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Required(ErrorMessage = "{0} is required") ]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The account identifier.
        /// </value>
        [Required(ErrorMessage = "{0} is required") ]
        public int AccountId { get; set; }

        /// <summary>
        /// Gets or sets the account.
        /// </summary>
        /// <value>
        /// The account.
        /// </value>
        public AccountDto? Account { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessage = "{0} is required") ]
        [ MinLength(4, ErrorMessage = "{0} must have at least 4 characters") ]
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the img.
        /// </summary>
        /// <value>
        /// The type of the img.
        /// </value>
        [MaxLength(4, ErrorMessage = "{0} must have a maximum of 4 characters") ]
        public string? ImgType { get; set; }
    }
}