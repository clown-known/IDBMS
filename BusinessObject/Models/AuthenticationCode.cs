using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class AuthenticationCode
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
    public User User { get; set; } = new();

    [Required]
    public AuthenticationCodeStatus Status { get; set; }
}
