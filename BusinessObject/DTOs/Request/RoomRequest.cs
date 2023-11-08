using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs.Request
{
    public class RoomRequest
    {
        [Required]
        public Guid FloorId { get; set; }

        public string? Description { get; set; }

        [Required]
        public int RoomNo { get; set; }

        [Required]
        public string UsePurpose { get; set; } = default!;

        [Required]
        public double Area { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal PricePerArea { get; set; }

        [Required]
        public int RoomTypeId { get; set; }

        public Guid ProjectId { get; set; }
    }
}
