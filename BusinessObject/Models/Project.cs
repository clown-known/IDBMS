using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class Project
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string CompanyName { get; set; } = default!;

    [Required]
    public string Location { get; set; } = default!;

    public string? Description { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public DateTime UpdatedDate { get; set; }

    [Required]
    public int NoStage { get; set; }

    public double EstimatedPrice { get; set; }

    public double FinalPrice { get; set; }

    [Required]
    public int Language { get; set; }

    [Required]
    public bool IsAdvertisement { get; set; }

    [Required]
    public int Status { get; set; }
}
