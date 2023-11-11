using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class ProjectDocument
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    [Required]
    public string Url { get; set; } = default!;

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public ProjectDocumentCategory Category { get; set; }

    [Required]
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = new();

    [Required]
    public int ProjectDocumentTemplateId { get; set; }
    public ProjectDocumentTemplate ProjectDocumentTemplate { get; set; } = new();

    [Required]
    public bool IsPublicAdvertisement { get; set; }

    [Required]
    public bool IsDeleted { get; set; }
}
