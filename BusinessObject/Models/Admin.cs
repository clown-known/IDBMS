using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class Admin
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string Email { get; set; } = default!;

    [Required]
    public byte[] PasswordHash { get; set; } = default!;
    [Required]
    public byte[] PasswordSalt { get; set; } = default!;

    public string? AuthenticationCode { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    public Guid? CreatorId { get; set; }
    public Admin? Creator { get; set; }
}
