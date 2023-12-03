using BusinessObject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class BookingRequest
    {
        [Key]
        public Guid Id { get; set; }

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
        public User User { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Required]
        public BookingRequestStatus Status { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
