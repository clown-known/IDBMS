using BusinessObject.Enums;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs.Request
{
    public class ProjectDesignRequest
    {
        [Required]
        [Column(TypeName = "money")]
        public decimal MinBudget { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal MaxBudget { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Required]
        public ProjectType Type { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public List<PaymentStageDesign> PaymentStageDesigns { get; set; } = new();
        public List<Project> Projects { get; set; } = new();
    }
}
