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
    public class InteriorItemRequest
    {
        [Required]
        public string Code { get; set; } = default!;

        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public double Length { get; set; }

        [Required]
        public double Width { get; set; }

        [Required]
        public double Height { get; set; }

        [Required]
        public CalculationUnit CalculationUnit { get; set; }

        [Required]
        public string Material { get; set; } = default!;

        public string? Description { get; set; }

        public string? Origin { get; set; } = default!;

        [Required]
        [Column(TypeName = "money")]
        public decimal EstimatePrice { get; set; }

        [Required]
        public double LaborCost { get; set; }

        public int? InteriorItemColorId { get; set; }
        public InteriorItemColor? InteriorItemColor { get; set; }

        [Required]
        public int InteriorItemCategoryId { get; set; }

        [Required]
        public InteriorItemStatus Status { get; set; }

        public Guid? ParentItemId { get; set; }
    }
}
