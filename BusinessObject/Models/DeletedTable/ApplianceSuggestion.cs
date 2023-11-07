using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models.DeletedTable;

public class ApplianceSuggestion
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    public Guid? InteriorItemId { get; set; }
    public InteriorItem? InteriorItem { get; set; }

    [Required]
    public Guid RoomId { get; set; }
    public Room Room { get; set; } = new();

    public Guid? ProjectId { get; set; }
    public Project? Project { get; set; }
}
