using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public class ConstructionTaskDesign
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Code { get; set; } = default!;

    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    [Required]
    public int CalculationUnit { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal EstimatePricePerUnit { get; set; }

    public int? InteriorItemCategoryId { get; set; }
    public InteriorItemCategory? InteriorItemCategory { get; set; } = new();

    [Required]
    public int ConstructionTaskCategoryId { get; set; }
    public ConstructionTaskCategory ConstructionTaskCategory { get; set; } = new();

    public List<ConstructionTask> ConstructionTasks { get; set; } = new();
}
