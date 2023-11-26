using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Enums;

namespace BusinessObject.Models
{
    public class ProjectDesign
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal MinBudget { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal MaxBudget { get; set; }

        [Required]
        public int EstimateBusinessDay { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Required]
        public ProjectType ProjectType { get; set; }

        [Required]
        public bool IsHidden { get; set; }

        public List<PaymentStageDesign> PaymentStageDesigns { get; set; } = new();
        public List<Project> Projects { get; set; } = new();
    }
}
