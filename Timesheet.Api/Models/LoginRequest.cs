using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Timesheet.Api.Models
{
    /// <summary>
    /// LoginRequest to auth in api
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// User's Last Name
        /// </summary>
        [Required]
        public string LastName { get; set; }
    }

    public class LoginRequestFluentValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestFluentValidator()
        {
            RuleFor(x => x.LastName)
                .NotEmpty();
        }
    }
}
