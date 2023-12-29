using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BusinessObject.Enums;
using System.Text.Json.Serialization;

namespace IDBMS_API.DTOs.Request.AccountRequest
{
    public class CreateUserRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? Bio { get; set; } = default!;
        public string? JobPosition { get; set; } = default!;

        public string? CompanyName { get; set; } = default!;

        [Required]
        public string Address { get; set; } = default!;

        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;

        [Required]
        public string Phone { get; set; } = default!;

        public DateTime? DateOfBirth { get; set; }

        [Required]
        public Language Language { get; set; }

        [Required]
        public CompanyRole Role { get; set; }

        public string? ExternalId { get; set; } = default!;

    }
}
