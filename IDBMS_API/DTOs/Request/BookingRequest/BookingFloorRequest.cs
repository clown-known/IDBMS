using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request.BookingRequest
{
    public class BookingFloorRequest
    {
        public string? Description { get; set; }

        [Required]
        public string UsePurpose { get; set; } = default!;

        [Required]
        public int FloorNo { get; set; }

        [Required]
        public double Area { get; set; }

        public List<BookingRoomRequest>? Rooms { get; set; }
    }
}
