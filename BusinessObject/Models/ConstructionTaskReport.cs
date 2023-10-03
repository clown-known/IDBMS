using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class ConstructionTaskReport
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public int CalculationUnit { get; set; }

    [Required]
    public double UnitUsed { get; set; }

    public string? Description { get; set; }
    
    [Required]
    public Guid ConstructionTaskId { get; set; }
    public ConstructionTask ConstructionTask { get; set; } = new();

    [Required]
    public DateTime CreatedTime { get; set; }

    [Required]
    public Guid AuthorId { get; set; }
    public User Author { get; set; } = new();

    [Required]
    public bool IsDeleted { get; set; }
}
