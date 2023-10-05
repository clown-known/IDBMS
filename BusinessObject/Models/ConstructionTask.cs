using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public class ConstructionTask
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
    public int CalculationUnit { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal PricePerUnit { get; set; }

    [Required]
    public double UnitInContract { get; set; }

    [Required]
    public double UnitUsed { get; set; }

    [Required]
    public int IsExceed { get; set; }

    [Required]
    public DateTime StartedDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public int NoDate { get; set; }

    public int? ConstructionTaskCategoryId { get; set; }
    public ConstructionTaskCategory? ConstructionTaskCategory { get; set; } = new();

    public Guid? ProjectId { get; set; }
    public Project? Project { get; set; } = new();

    [Required]
    public Guid PrepayStageId { get; set; }
    public PrepayStage PrepayStage { get; set; } = new();

    public Guid? InteriorItemId { get; set; }

    [Required]
    public int ConstructionTaskDesignId { get; set; }
    public ConstructionTaskDesign ConstructionTaskDesign { get; set; } = new();

    [Required]
    public int Status { get; set; }

    public List<Comment> Comments { get; set; } = new();
    public List<ConstructionTaskReport> ConstructionTaskReports { get; set; } = new();
}
