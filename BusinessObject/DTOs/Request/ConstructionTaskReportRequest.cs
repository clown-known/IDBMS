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
    public class ConstructionTaskReportRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public CalculationUnit CalculationUnit { get; set; }

        [Required]
        public double UnitUsed { get; set; }

        public string? Description { get; set; }

        [Required]
        public Guid ConstructionTaskId { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
