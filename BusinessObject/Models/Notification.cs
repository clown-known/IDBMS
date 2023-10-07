using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class Notification
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public NotificationCategory Category { get; set; }

    [Required]
    public string Content { get; set; } = default!;

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; } = new();

    public string? Link { get; set; } = default!;

    [Required]
    public bool IsSeen { get; set; }
}
