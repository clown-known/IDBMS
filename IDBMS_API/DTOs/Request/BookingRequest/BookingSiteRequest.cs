using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request.BookingRequest
{
    public class BookingSiteRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Required]
        public string Address { get; set; } = default!;

        [Required]
        public string UsePurpose { get; set; } = default!;

        [Required]
        public double Area { get; set; }

        public List<BookingFloorRequest>? Floors { get; set; }
        [Required]
        public Guid ProjectId { get; set; }
    }
}
