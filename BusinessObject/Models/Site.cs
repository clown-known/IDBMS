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
        public string ContactName { get; set; } = default!;

        [Required]
        public string ContactEmail { get; set; } = default!;

        [Required]
        public string ContactPhone { get; set; } = default!;

        [Required]
        public string ContactLocation { get; set; } = default!;

        public string? CompanyCode { get; set; }

        [Required]
        public string Address { get; set; } = default!;

        [Required]
        public bool IsDeleted { get; set; }

        public List<Project> Projects { get; set; } = new();
    }
}
