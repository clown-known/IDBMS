using BusinessObject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class TaskReport
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public CalculationUnit CalculationUnit { get; set; }

        [Required]
        public double UnitUsed { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }

        public DateTime? UpdatedTime { get; set; }

        [Required]
        public Guid ProjectTaskId { get; set; }
        public ProjectTask ProjectTask { get; set; } = new();

        [Required]
        public bool IsDeleted { get; set; }

        public List<TaskDocument> TaskDocuments { get; set; } = new();

    }
}
