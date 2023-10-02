using BusinessObject.Enums;
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
    public int Type { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public DateTime UpdatedDate { get; set; }

    [Required]
    public int NoStage { get; set; }

    [Column(TypeName = "money")]
    public decimal EstimatedPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal FinalPrice { get; set; }

    [Required]
    public int Language { get; set; }

    [Required]
    public ProjectStatus Status { get; set; }

    [Required]
    public bool IsAdvertisement { get; set; }

    public string? AdminNote { get; set; } = default!;

    public Guid? BasedOnDecorProjectId { get; set; }

    [ForeignKey("BasedOnDecorProjectId")]
    public Project BasedOnDecorProject { get; set; } = default!;

    public List<Participation> Participations { get; set; } = default!;

    public List<PrepayStage> PrepayStages { get; set; } = default!;
}
