using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public DateTime CreatedDate { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; }

    public string? Link { get; set; } = default!;

    [Required]
    public bool IsSeen { get; set; }
}
