using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class UserRole
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int RoleId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    public virtual Role Role { get; set; } = default!;

    public virtual User User { get; set; } = default!;
}
