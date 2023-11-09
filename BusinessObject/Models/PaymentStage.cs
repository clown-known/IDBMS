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

    [Required]
    public string Description { get; set; } = default!;

    [Required]
    public bool IsPaid { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal TotalPaid { get; set; }

    [Required]
    public bool IsPrepaid { get; set; }

    [Required]
    public double PricePercentage { get; set; }

    [Required]
    public DateTime PaidDate { get; set; }

    [Required]
    public DateTime StartedDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public DateTime EndTimePayment { get; set; }

    public decimal? PenaltyFee { get; set; }

    [Required]
    public int EstimateBusinessDay { get; set; }

    [Required]
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = new();

    [Required]
    public bool IsHidden { get; set; }

    public List<ProjectTask> Tasks { get; set; } = new();
    public List<Transaction> Transactions { get; set; } = new();
}

