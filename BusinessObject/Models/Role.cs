using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class Role
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string Description { get; set; } = default!;

    public List<UserRole> UserRoles { get; set; } = default!;

    public List<User> Users { get; set; } = default!;
}
