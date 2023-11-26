using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class InteriorItemColor
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public ColorType Type { get; set; }

    [Required]
    public string PrimaryColor { get; set; } = default!;

    public string? SecondaryColor { get; set; }

    public List<InteriorItem> InteriorItems { get; set; } = new();
}
