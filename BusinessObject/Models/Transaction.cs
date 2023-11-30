using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public class Transaction
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public TransactionType Type { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    [Required]
    public string? Note { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; }

    [Required]
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }

    public Guid? WarrantyClaimId { get; set; }
    public WarrantyClaim? WarrantyClaim { get; set; }

    [Required]
    public TransactionStatus Status { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    [Required]
    public string TransactionReceiptImageUrl { get; set; } = default!;
}
