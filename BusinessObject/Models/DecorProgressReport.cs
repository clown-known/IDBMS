using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class DecorProgressReport
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    [Required]
    public Guid AuthorId { get; set; }
    public User Author { get; set; } = default!;

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public Guid PrepayStageId { get; set; }
    public PrepayStage PrepayStage { get; set; } = new();

    [Required]
    public bool IsDeleted { get; set; }

    public List<Comment> Comments { get; set; } = new();
}
