using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class ApplianceSuggestion
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public string? SketchImageUrl { get; set; }

    public Guid? InteriorItemId { get; set; }
    public InteriorItem? InteriorItem { get; set; }

    [Required]
    public Guid RoomId { get; set; }
    public Room Room { get; set; } = new();
}
