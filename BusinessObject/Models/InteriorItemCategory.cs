using System.ComponentModel.DataAnnotations;
using BusinessObject.Enums;

namespace BusinessObject.Models;

public class InteriorItemCategory
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? EnglishName { get; set; }

    public string? Description { get; set; }

    public string? EnglishDescription { get; set; }

    public string? BannerImageUrl { get; set; } = default!;

    public string? IconImageUrl { get; set; } = default!;

    [Required]
    public InteriorItemType InteriorItemType { get; set; }

    public int? ParentCategoryId { get; set; }
    public InteriorItemCategory? ParentCategory { get; set; }

    [Required]
    public bool IsDeleted { get; set; }
}
