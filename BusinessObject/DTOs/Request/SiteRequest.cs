using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs.Request
{
    public class SiteRequest
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

        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
