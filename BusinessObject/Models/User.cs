using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? Bio { get; set; } = default!;

    [Required]
    [Column(TypeName = "money")]
    public decimal Balance { get; set; }

    [Required]
    public string Address { get; set; } = default!;

    [Required]
    public string Email { get; set; } = default!;

    [Required]
    public string Phone { get; set; } = default!;

    [Required]
    public byte[] PasswordHash { get; set; } = default!;
    [Required]
    public byte[] PasswordSalt { get; set; } = default!;

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public DateTime UpdatedDate { get; set; }

    [Required]
    public Language Language { get; set; } = default!;

    [Required]
    public UserStatus Status { get; set; }

    public string? ExternalId { get; set; } = default!;

    public string? Token { get; set; } = default!;

    public DateTime? LockedUntil { get; set; }

    public List<Comment> Comments { get; set; } = new();
    public List<InteriorItemBookmark> InteriorItemBookmarks { get; set; } = new();
    public List<Notification> Notifications { get; set; } = new();
    public List<Transaction> Transactions { get; set; } = new();
    public List<UserRole> UserRoles { get; set; } = new();
    public List<Participation> Participations { get; set; } = new();
}
