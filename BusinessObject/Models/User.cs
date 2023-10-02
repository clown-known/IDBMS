using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string Bio { get; set; } = default!;

    [Required]
    [Column(TypeName = "money")]
    public decimal Balance { get; set; }

    [Required]
    public string Address { get; set; } = default!;

    [Required]
    public string Email { get; set; } = default!;
    [Required]
    public string Password { get; set; } = default!;

    [Required]
    public string Phone { get; set; } = default!;

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public DateTime UpdatedDate { get; set; }

    [Required]
    public string Language { get; set; } = default!;

    [Required]
    public int Status { get; set; }

    public string ExternalId { get; set; } = default!;

    public string Token { get; set; } = default!;

    [NotMapped]
    public List<UserRole> UserRoles { get; set; } = default!;

    [NotMapped]
    public List<Project> ParticipateProjects { get; set; } = default!;

    [NotMapped]
    public List<Project> OwnedProjects { get; set; } = default!;

    [NotMapped]
    public List<Project> LeadProjects { get; set; } = default!;

    [NotMapped]
    public List<Participation> Participations { get; set; } = default!;
}
