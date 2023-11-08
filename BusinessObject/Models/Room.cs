using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public class Room
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid FloorId { get; set; }
    public Floor Floor { get; set; } = new();

    public string? Description { get; set; }

    [Required]
    public int RoomNo { get; set; }

    [Required]
    public string UsePurpose { get; set; } = default!;

    [Required]
    public double Area { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal PricePerArea { get; set; }

    [Required]
    public int RoomTypeId { get; set; }
    public RoomType RoomType { get; set; } = new();

    [Required]
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = new();

    [Required]
    public bool IsHidden { get; set; }

    public List<ProjectTask> Tasks { get; set; } = new();
}
