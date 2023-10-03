using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public enum UserStatus
{
    Active = 0,
    Unverified = 1,
    Suspended = 2,
    Locked = 3
}

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
    public byte[] Password { get; set; } = default!;

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public DateTime UpdatedDate { get; set; }

    [Required]
    public string Language { get; set; } = default!;

    [Required]
    public UserStatus Status { get; set; }

    public string? ExternalId { get; set; } = default!;

    public string? Token { get; set; } = default!;

    public DateTime? LockedUntil { get; set; }

    public List<Comment> Comments { get; set; } = new();
    public List<ConstructionTaskReport> ConstructionTaskReports { get; set; } = new();
    public List<DecorProgressReport> DecorProgressReports { get; set; } = new();
    public List<Transaction> Transactions { get; set; } = new();
    public List<UserRole> UserRoles { get; set; } = new();
    public List<Participation> Participations { get; set; } = new();
}
