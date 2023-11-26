using BusinessObject.Enums;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs.Request
{
    public class InteriorItemCategoryRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? EnglishName { get; set; }

        public string? Description { get; set; }

        public string? EnglishDescription { get; set; }

        [Required]
        public string BannerImageUrl { get; set; } = default!;

        [Required]
        public string IconImageUrl { get; set; } = default!;

        [Required]
        public InteriorItemType InteriorItemType { get; set; }

        public int? ParentCategoryId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
