using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class ProjectParticipation
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; }

    [Required]
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }

    [Required]
    public ParticipationRole Role { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    public List<TaskAssignment> TaskAssignments { get; set; } = new();
}
