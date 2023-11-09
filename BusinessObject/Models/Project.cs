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

    public string? CompanyName { get; set; } = default!;

    public string? Description { get; set; }

    [Required]
    public ProjectType Type { get; set; }

    [Required]
    public int ProjectCategoryId { get; set; }
    public ProjectCategory ProjectCategory { get; set; } = new();

    [Required]
    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? NoStage { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal EstimatedPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal? FinalPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalWarrantyPaid { get; set; }

    public Guid? CurrentStageId { get; set; }

    [Required]
    public int Language { get; set; }

    [Required]
    public ProjectStatus Status { get; set; }
    //enum
    [Required]
    public int AdvertisementStatus { get; set; }

    public string? AdminNote { get; set; }

    public Guid? BasedOnDecorProjectId { get; set; }
    public Project? BasedOnDecorProject { get; set; }
    //delete
    public int? DecorProjectDesignId { get; set; }
    public DecorProjectDesign? DecorProjectDesign { get; set; } = new();

    public int? ProjectDesignId { get; set; }
    public ProjectDesign? ProjectDesign { get; set; } = new();

    public List<ConstructionTask> ConstructionTasks { get; set; } = new();
    public List<Floor> Floors { get; set; } = new();
    public List<Participation> Participations { get; set; } = new();
    public List<PaymentStage> PaymentStages { get; set; } = new();
    public List<ProjectDocument> ProjectDocuments { get; set; } = new();
    public List<InteriorItem> InteriorItems { get; set; } = new();
}
