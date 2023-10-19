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
        public string Name { get; set; } = default!;

        public string ImageUrl { get; set; } = default!;

        public string? Description { get; set; }

        public decimal PricePerArea { get; set; }

        public bool IsHidden { get; set; }

        public string IconImageUrl { get; set; } = default!;
    }
}
