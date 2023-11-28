using BusinessObject.Enums;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request
{
    public class InteriorItemCategoryRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? EnglishName { get; set; }

        public string? Description { get; set; }

        public string? EnglishDescription { get; set; }

        [Required]
        public IFormFile BannerImage { get; set; } = default!;

        [Required]
        public IFormFile IconImage { get; set; } = default!;

        [Required]
        public InteriorItemType InteriorItemType { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}
