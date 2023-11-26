using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class InteriorItemBookmark
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; } = new();

    [Required]
    public Guid InteriorItemId { get; set; }
    public InteriorItem InteriorItem { get; set; } = new();
}
