﻿using System.ComponentModel.DataAnnotations;

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
    public int Category { get; set; }

    [Required]
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = new();

    public Guid? ConstructionTaskReportId { get; set; }
    public ConstructionTaskReport? ConstructionTaskReport { get; set; }

    public Guid? DecorProgressReportId { get; set; }
    public DecorProgressReport? DecorProgressReport { get; set; }

    [Required]
    public Guid ProjectDocumentTemplateId { get; set; }
    public ProjectDocumentTemplate ProjectDocumentTemplate { get; set; } = new();

    [Required]
    public bool IsDeleted { get; set; }
}
