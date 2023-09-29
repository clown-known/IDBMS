using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public class Project
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string CompanyName { get; set; } = default!;

    [Required]
    public string Location { get; set; } = default!;

    public string? Description { get; set; }

    [Required]
    public bool IsDecor { get; set; }

    [Required]
    public bool IsConstruction { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public DateTime UpdatedDate { get; set; }

    [Required]
    public int NoStage { get; set; }

    public Guid ProjectOwnerUserId { get; set; }

    public Guid LeadArchitectUserId { get; set; }

    [Column(TypeName = "money")]
    public decimal EstimatedPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal FinalPrice { get; set; }

    [Required]
    public int Language { get; set; }

    [Required]
    public bool IsAdvertisement { get; set; }

    [Required]
    public int Status { get; set; }

    [ForeignKey("ProjectOwnerUserId")]
    public User ProjectOwner { get; set; } = default!;

    [ForeignKey("LeadArchitectUserId")]
    public User LeadArchitect { get; set; } = default!;

    [NotMapped]
    public List<User> ParticipatingUsers { get; set; } = default!;

    [NotMapped]
    public List<Participation> Participations { get; set; } = default!;
}
