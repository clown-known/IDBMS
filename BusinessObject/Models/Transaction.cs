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

    public string? Note { get; set; }    
    
    [Required]
    public string PayerName { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    public Guid? UserId { get; set; }
    public User? User { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; } = new();

    [Required]
    public TransactionStatus Status { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    public string? TransactionReceiptImageUrl { get; set; } = default!;
}
