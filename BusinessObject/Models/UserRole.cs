using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class UserRole
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Enums.UserRole Role { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
