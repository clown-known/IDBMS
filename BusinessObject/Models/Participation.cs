using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class Participation
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid ProjectId { get; set; }

    [Required]
    public int Role { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    public User User { get; set; } = default!;

    public Project Project { get; set; } = default!;
}
