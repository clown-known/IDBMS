using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class Participation
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    [Required]
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = default!;

    [Required]
    public ParticipationRole Role { get; set; }

    [Required]
    public bool IsDeleted { get; set; }
}
