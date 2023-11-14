using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public class InteriorItem
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Code { get; set; } = default!;

    [Required]
    public string Name { get; set; } = default!;

    public string? EnglishName { get; set; }

    [Required]
    public double Length { get; set; }

    [Required]
    public double Width { get; set; }

    [Required]
    public double Height { get; set; }

    [Required]
    public CalculationUnit CalculationUnit { get; set; }

    [Required]
    public string Material { get; set; } = default!;

    public string? Description { get; set; }

    public string? EnglishDescription { get; set; }

    public string? Origin { get; set; } = default!;

    [Required]
    [Column(TypeName = "money")]
    public decimal EstimatePrice { get; set; }

    [Required]
    public double LaborCost { get; set; }

    public int? InteriorItemColorId { get; set; }
    public InteriorItemColor? InteriorItemColor { get; set; } = new();
    
    [Required]
    public int InteriorItemCategoryId { get; set; }
    public InteriorItemCategory InteriorItemCategory { get; set; } = new();

    [Required]
    public InteriorItemStatus Status { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    public Guid? ParentItemId { get; set; }
    public InteriorItem? ParentItem { get; set; }

    public List<ProjectTask> Tasks { get; set; } = new();
    public List<InteriorItemBookmark> InteriorItemBookmarks { get; set; } = new();
}
