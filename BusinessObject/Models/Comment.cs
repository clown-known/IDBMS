using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class Comment
{
    [Key]
    public Guid Id { get; set; }

    public string? Content { get; set; } = default!;

    public string? FileUrl { get; set; }

    [Required]
    public Guid ProjectTaskId { get; set; }
    public ProjectTask ProjectTask { get; set; }

    [Required]
    public Guid ProjectId { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; }

    [Required]
    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public Guid? ReplyCommentId { get; set; }
    public Comment? ReplyComment { get; set; }

    [Required]
    public CommentStatus Status { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    public List<Comment> CommentReplies { get; set; } = new();
}
