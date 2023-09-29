using System.ComponentModel.DataAnnotations;

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

    [Required]
    public Guid ProjectOwnerUserId { get; set; }

    [Required]
    public Guid LeadArchitectUserId { get; set; }

    public decimal EstimatedPrice { get; set; }

    public decimal FinalPrice { get; set; }

    [Required]
    public int Language { get; set; }

    [Required]
    public bool IsAdvertisement { get; set; }

    [Required]
    public int Status { get; set; }

    public virtual User ProjectOwner { get; set; } = default!;

    public virtual User LeadArchitect { get; set; } = default!;
}
