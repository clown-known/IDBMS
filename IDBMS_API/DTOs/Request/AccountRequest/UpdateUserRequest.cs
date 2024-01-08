using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace IDBMS_API.DTOs.Request.AccountRequest
{
    public class UpdateUserRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? JobPosition { get; set; } = default!;

        public string? CompanyName { get; set; } = default!;

        public string? Bio { get; set; } = default!;

        [Required]
        public string Address { get; set; } = default!;

        [Required]
        public string Email { get; set; } = default!;

        public string? Password { get; set; } = default!;

        [Required]
        public string Phone { get; set; } = default!;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Language Language { get; set; } = default!;

        [Required]
        public CompanyRole Role { get; set; }

        public string? ExternalId { get; set; } = default!;
    }
}
