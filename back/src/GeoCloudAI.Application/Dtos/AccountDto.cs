using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    /// <summary>
    /// This class represents a Data Transfer Object (DTO) for an account.
    /// </summary>
    public class AccountDto
    {
        /// <summary>
        /// Gets or sets the identifier. Id
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
        [ MinLength(4, ErrorMessage = "{0} must have at least 4 characters") ]
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        /// <value>
        /// The company.
        /// </value>
        [Required(ErrorMessage = "{0} is required") ]
        [ MinLength(4, ErrorMessage = "{0} must have at least 4 characters") ]
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? Company { get; set; }

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>
        /// The employees.
        /// </value>
        [Required(ErrorMessage = "{0} is required") ]
        [ MaxLength(10, ErrorMessage = "{0} must have a maximum of 10 characters") ]
        public string? Employees { get; set; }

        /// <summary>
        /// Gets or sets the acess maximum attempts.
        /// </summary>
        /// <value>
        /// The acess maximum attempts.
        /// </value>
        [Required(ErrorMessage = "{0} is required") ]
        [ Range(2, 10, ErrorMessage = "{0} must have a value between 2 and 10")]
        public int? AcessMaxAttempts { get; set; }

        /// <summary>
        /// Gets or sets the validity user password.
        /// </summary>
        /// <value>
        /// The validity user password.
        /// </value>
        [Required(ErrorMessage = "{0} is required") ]
        [ Range(10, 90, ErrorMessage = "{0} must have a value between 10 and 90")]
        public int? ValidityUserPassword { get; set; }

        /// <summary>
        /// Gets or sets the validity invite user.
        /// </summary>
        /// <value>
        /// The validity invite user.
        /// </value>
        [Required(ErrorMessage = "{0} is required") ]
        [ Range(2, 30, ErrorMessage = "{0} must have a value between 2 and 30")]
        public int? ValidityInviteUser { get; set; }

        /// <summary>
        /// Gets or sets the validity invite project.
        /// </summary>
        /// <value>
        /// The validity invite project.
        /// </value>
        [Required(ErrorMessage = "{0} is required") ]
        [ Range(2, 30, ErrorMessage = "{0} must have a value between 2 and 30")]
        public int? ValidityInviteProject { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public string? Guid { get; set; }
    }
}