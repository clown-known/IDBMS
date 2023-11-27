using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request
{
    public class FloorRequest
    {
        public string? Description { get; set; }

        [Required]
        public string UsePurpose { get; set; } = default!;

        [Required]
        public int FloorNo { get; set; }

        [Required]
        public double Area { get; set; }

        [Required]
        public Guid SiteId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

    }
}
