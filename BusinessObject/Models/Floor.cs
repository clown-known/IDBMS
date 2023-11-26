using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class Floor
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    [Required]
    public string UsePurpose { get; set; } = default!;

    [Required]
    public int FloorNo { get; set; }

    [Required]
    public double Area { get; set; }

    [Required]
    public Guid SiteId { get; set; }
    public Site Site { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    public List<Room> Rooms { get; set; } = new();
}
