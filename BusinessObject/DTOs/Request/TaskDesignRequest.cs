using BusinessObject.Enums;
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
    public class TaskDesignRequest
    {
        [Required]
        public string Code { get; set; } = default!;

        [Required]
        public string Name { get; set; } = default!;

        public string? EnglishName { get; set; }

        public string? Description { get; set; }

        public string? EnglishDescription { get; set; }

        [Required]
        public CalculationUnit CalculationUnit { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal EstimatePricePerUnit { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public int? InteriorItemCategoryId { get; set; }

        [Required]
        public int TaskCategoryId { get; set; }
    }
}
