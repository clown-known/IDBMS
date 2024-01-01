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
    public string UsePurpose { get; set; } = default!;

    [Required]
    public double Area { get; set; }

    [Column(TypeName = "money")]
    public decimal? PricePerArea { get; set; }

    public int? RoomTypeId { get; set; }
    public RoomType RoomType { get; set; }

    public Guid? ProjectId { get; set; }
    public Project? Project { get; set; } = new();

    public List<ApplianceSuggestion> ApplianceSuggestions { get; set; } = new();
}
