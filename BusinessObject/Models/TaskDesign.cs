using BusinessObject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class TaskDesign
    {
        [Key]
        public int Id { get; set; }

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

        [Required]
        public int InteriorItemCategoryId { get; set; }
        public InteriorItemCategory InteriorItemCategory { get; set; } = new();

        [Required]
        public int TaskCategoryId { get; set; }
        public TaskCategory TaskCategory { get; set; } = new();

        public List<ProjectTask> Tasks { get; set; } = new();
    }
}
