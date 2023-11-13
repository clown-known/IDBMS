using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class Comment
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Content { get; set; } = default!;

    public Guid? ProjectTaskId { get; set; }
    public ProjectTask? ProjectTask { get; set; }

    public Guid? ProjectId { get; set; }
    public Project? Project { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; } = new();

    public string? FileUrl { get; set; }

    [Required]
    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    [Required]
    public CommentStatus Status { get; set; }

    [Required]
    public bool IsDeleted { get; set; }
}
