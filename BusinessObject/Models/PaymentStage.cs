using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public class PaymentStage
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public int StageNo { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    [Required]
    public bool IsContractAmountPaid { get; set; }

    [Required]
    public bool IsIncurredAmountPaid { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal TotalContractPaid { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalIncurredPaid { get; set; }

    [Required]
    public bool IsPrepaid { get; set; }

    [Required]
    public double PricePercentage { get; set; }

    public DateTime? StartedDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? EndTimePayment { get; set; }

    [Column(TypeName = "money")]
    public decimal? PenaltyFee { get; set; }

    public int? EstimateBusinessDay { get; set; }

    [Required]
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    [Required]
    public StageStatus Status { get; set; }

    public List<ProjectTask> Tasks { get; set; } = new();
}

