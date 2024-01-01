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

    public string? CompanyName { get; set; } = default!;
    public string? JobPosition { get; set; } = default!;
    public string? Bio { get; set; } = default!;

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
    public DateTime? DateOfBirth { get; set; }

    [Required]
    public DateTime UpdatedDate { get; set; }

    [Required]
    public Language Language { get; set; }

    [Required]
    public CompanyRole Role { get; set; }

    [Required]
    public UserStatus Status { get; set; }

    [JsonIgnore]
    public string? ExternalId { get; set; } = default!;

    [JsonIgnore]
    public string? Token { get; set; } = default!;

    public DateTime? LockedUntil { get; set; }

    public List<Comment> Comments { get; set; } = new();
    public List<ConstructionTaskReport> ConstructionTaskReports { get; set; } = new();
    public List<DecorProgressReport> DecorProgressReports { get; set; } = new();
    public List<InteriorItemBookmark> InteriorItemBookmarks { get; set; } = new();
    public List<Notification> Notifications { get; set; } = new();
    public List<Transaction> Transactions { get; set; } = new();
    public List<ProjectParticipation> Participations { get; set; } = new();
    public List<AuthenticationCode> AuthenticationCodes { get; set; } = new();
    public List<BookingRequest> BookingRequests { get; set; } = new();
}
