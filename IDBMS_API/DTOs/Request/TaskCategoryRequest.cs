using BusinessObject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request
{
    public class TaskCategoryRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? EnglishName { get; set; }

        public string? Description { get; set; }

        public string? EnglishDescription { get; set; }

        [Required]
        public ProjectType ProjectType { get; set; }

        [Required]
        public IFormFile IconImage { get; set; } = default!;

        [Required]
        public bool IsDeleted { get; set; }
    }
}
