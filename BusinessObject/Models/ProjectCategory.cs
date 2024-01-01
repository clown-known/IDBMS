using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class ProjectCategory
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? EnglishName { get; set; }

    public string? IconImageUrl { get; set; } = default!;

    [Required]
    public bool IsHidden { get; set; }

    public List<Project> Projects { get; set; } = new();
}
