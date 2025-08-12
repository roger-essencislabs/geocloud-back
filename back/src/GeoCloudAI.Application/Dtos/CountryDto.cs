using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    /// <summary>
    /// This class represents a Data Transfer Object (DTO) for a country.
    /// </summary>
    public class CountryDto
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessage = "{0} is required") ]
        [ MaxLength(60, ErrorMessage = "{0} must have a maximum of 60 characters") ]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the acronym2.
        /// </summary>
        /// <value>
        /// The acronym2.
        /// </value>
        [Required(ErrorMessage = "{0} is required") ]
        [ MinLength(2, ErrorMessage = "{0} must have at least 2 characters") ]
        [ MaxLength(2, ErrorMessage = "{0} must have a maximum of 2 characters") ]
        public string? Acronym2 { get; set; }

        /// <summary>
        /// Gets or sets the acronym3.
        /// </summary>
        /// <value>
        /// The acronym3.
        /// </value>
        [Required(ErrorMessage = "{0} is required") ]
        [ MinLength(3, ErrorMessage = "{0} must have at least 3 characters") ]
        [ MaxLength(3, ErrorMessage = "{0} must have a maximum of 3 characters") ]
        public string? Acronym3 { get; set; }
    }
}