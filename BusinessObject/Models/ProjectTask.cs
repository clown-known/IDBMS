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
    public class ProjectTask
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Code { get; set; } = default!;

        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Percentage { get; set; }

        [Required]
        public CalculationUnit CalculationUnit { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal PricePerUnit { get; set; }

        [Required]
        public double UnitInContract { get; set; }

        [Required]
        public double UnitUsed { get; set; }

        [Required]
        public bool IsExceed { get; set; }

        [Required]
        public DateTime StartedDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? NoDate { get; set; }

        public Guid? ParentTaskId { get; set; }
        public ConstructionTask? ParentTask { get; set; }

        public int? TaskCategoryId { get; set; }
        public TaskCategory? TaskCategory { get; set; } = new();

        public Guid? ProjectId { get; set; }
        public Project? Project { get; set; } = new();

        [Required]
        public Guid PaymentStageId { get; set; }
        public PaymentStage PaymentStage { get; set; } = new();

        public Guid? InteriorItemId { get; set; }
        public InteriorItem? InteriorItem { get; set; }

        [Required]
        public int TaskDesignId { get; set; }
        public TaskDesign ConstructionTaskDesign { get; set; } = new();

        [Required]
        public ConstructionTaskStatus Status { get; set; }

        public List<Comment> Comments { get; set; } = new();
        public List<TaskReport> TaskReports { get; set; } = new();
    }
}

