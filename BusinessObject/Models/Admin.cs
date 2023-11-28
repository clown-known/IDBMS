using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
    [JsonIgnore]
    public byte[] PasswordHash { get; set; } = default!;

    [Required]
    [JsonIgnore]
    public byte[] PasswordSalt { get; set; } = default!;
    [Required]
    public AdminStatus Status { get; set; }

    public string? AuthenticationCode { get; set; }
    public string? token { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    public Guid? CreatorId { get; set; }
    public Admin? Creator { get; set; }
}
