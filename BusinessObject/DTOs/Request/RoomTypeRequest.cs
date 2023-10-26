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
    public class RoomTypeRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string ImageUrl { get; set; } = default!;

        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal PricePerArea { get; set; }

        [Required]
        public bool IsHidden { get; set; }

        [Required]
        public string IconImageUrl { get; set; } = default!;
    }
}
