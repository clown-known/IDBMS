using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class Transaction
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public int Category { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public string? Note { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public Guid PrepayStageId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public int Status { get; set; }

    [Required]
    public string TransactionReceiptImageUrl { get; set; } = default!;

    public string? AdminNote { get; set; }

    public User User { get; set; } = new();

    public PrepayStage PrepayStage { get; set; } = new();
}
