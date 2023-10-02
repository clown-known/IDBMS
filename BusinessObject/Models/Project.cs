using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public enum ProjectStatus
{
    Draft = 0,
    PendingConfirmation = 1,
    Negotiating = 2,
    PendingDeposit = 3,
    Ongoing = 4,
    Suspended = 5,
    Canceled = 6,
    Done = 7
}

public enum ProjectType
{
    Decor = 0,
    Construction = 1,
}

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
    public ProjectType Type { get; set; }

    [Required]
    public int ProjectCategoryId { get; set; }
    public ProjectCategory ProjectCategory { get; set; } = new();

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
