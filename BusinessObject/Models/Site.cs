using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Site
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Required]
        public string UsePurpose { get; set; } = default!;

        [Required]
        public int FloorNo { get; set; }

        [Required]
        public double Area { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = new();

        [Required]
        public bool IsDeleted { get; set; }

        public List<Floor> Floors { get; set; } = new();
    }
}
