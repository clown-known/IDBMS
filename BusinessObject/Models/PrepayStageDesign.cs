using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class PrepayStageDesign
{
    [Key]
    public int Id { get; set; }

    [Required]
    public double PricePercentage { get; set; }

    [Required]
    public int StageNo { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    [Required]
    public int DecorProjectDesignId { get; set; }
    public DecorProjectDesign DecorProjectDesign { get; set; } = new();
}
