using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace IDBMS_API.DTOs.Request
{
    public class ProcessBookingRequestRequest
    {
        [Required]
        public string ContactEmail { get; set; } = default!;

        [Required]
        public string ContactName { get; set; } = default!;

        [Required]
        public string ContactPhone { get; set; } = default!;

        [Required]
        public string ContactLocation { get; set; } = default!;

        [Required]
        public Language Language { get; set; }

        public string? AdminReply { get; set; }

        [Required]
        public BookingRequestStatus Status { get; set; }
    }
}
