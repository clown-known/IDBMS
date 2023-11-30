using BusinessObject.Enums;
using BusinessObject.Models;
using System.ComponentModel.DataAnnotations;

namespace IDBMS_API.DTOs.Request
{
    public class BookingRequestRequest
    {
        [Required]
        public ProjectType ProjectType { get; set; }

        [Required]
        public string ContactName { get; set; } = default!;

        [Required]
        public string ContactEmail { get; set; } = default!;

        [Required]
        public string ContactPhone { get; set; } = default!;

        [Required]
        public string ContactLocation { get; set; } = default!;

        public string? Note { get; set; } = default!;

        [Required]
        public Guid UserId { get; set; }
    }
}
