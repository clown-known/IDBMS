using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public class RecoveryCode
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Code { get; set; } = default!;

    [Required]
    public DateTime CreatedTime { get; set; }

    [Required]
    public DateTime ExpiredTime { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public int Status { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; } = default!;
}
