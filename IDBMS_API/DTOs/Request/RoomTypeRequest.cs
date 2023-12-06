using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request
{
    public class RoomTypeRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? EnglishName { get; set; }

        [Required]
        public IFormFile Image { get; set; } = default!;

        public string? Description { get; set; }

        public string? EnglishDescription { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal PricePerArea { get; set; }

        [Required]
        public double EstimateDayPerArea { get; set; }

        [Required]
        public bool IsHidden { get; set; }

        [Required]
        public IFormFile IconImage { get; set; } = default!;
    }
}
