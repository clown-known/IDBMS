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

    public string? Description { get; set; }

    public string? AdvertisementDescription { get; set; }
    public string? EnglishAdvertisementDescription { get; set; }

    public string? RepresentImageUrl { get; set; }

    [Required]
    public ProjectType Type { get; set; }

    [Required]
    public int ProjectCategoryId { get; set; }
    public ProjectCategory ProjectCategory { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? WarrantyPeriodEndTime { get; set; }

    [Required]
    public string CreatedAdminUsername { get; set; } = default!;

    [Required]
    public Guid CreatedByAdminId { get; set; }
    public Admin CreatedByAdmin { get; set; }

    [Column(TypeName = "money")]
    public decimal? EstimatedPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal? FinalPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalWarrantyPaid { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal AmountPaid { get; set; }
    
    [Column(TypeName = "money")]
    public decimal? TotalPenaltyFee { get; set; }

    [Required]
    public double Area { get; set; }

    public int? EstimateBusinessDay { get; set; }

    [Required]
    public Language Language { get; set; }

    [Required]
    public ProjectStatus Status { get; set; }
    
    [Required]
    public AdvertisementStatus AdvertisementStatus { get; set; }

    public Guid? BasedOnDecorProjectId { get; set; }
    public Project? BasedOnDecorProject { get; set; }

    public int? ProjectDesignId { get; set; }
    public ProjectDesign? ProjectDesign { get; set; }

    public Guid? SiteId { get; set; }
    public Site? Site { get; set; }

    public List<Transaction> Transactions { get; set; } = new();
    public List<ProjectParticipation> ProjectParticipations { get; set; } = new();
    public List<PaymentStage> PaymentStages { get; set; } = new();
    public List<ProjectDocument> ProjectDocuments { get; set; } = new();
}
