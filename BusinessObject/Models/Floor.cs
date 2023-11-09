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
    //delete
    [Required]
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = new();

    [Required]
    public int SiteId { get; set; }
    public Site ProjectCategory { get; set; } = new();

    [Required]
    public bool IsDeleted { get; set; }

    public List<Room> Rooms { get; set; } = new();
}
