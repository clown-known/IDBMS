using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs.Request
{
    public class ProjectCategoryRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string IconImageUrl { get; set; } = default!;

        [Required]
        public bool IsHidden { get; set; }
    }
}
