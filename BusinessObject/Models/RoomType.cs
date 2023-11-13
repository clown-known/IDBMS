using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public class RoomType
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? EnglishName { get; set; }

    [Required]
    public string ImageUrl { get; set; } = default!;

    public string? Description { get; set; }

    public string? EnglishDescription { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal PricePerArea { get; set; }

    [Required]
    public bool IsHidden { get; set; }

    [Required]
    public string IconImageUrl { get; set; } = default!;

    public List<Room> Rooms { get; set; } = new();
}
