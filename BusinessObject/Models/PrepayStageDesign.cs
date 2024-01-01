using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class PrepayStageDesign
{
    [Key]
    public int Id { get; set; }

    [Required]
    public double PricePercentage { get; set; }

    [Required]
    public bool IsPrepaid { get; set; }

    [Required]
    public bool IsWarrantyStage { get; set; }

    [Required]
    public int StageNo { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    [Required]
    public int ProjectDesignId { get; set; }
    public ProjectDesign ProjectDesign { get; set; }
}
