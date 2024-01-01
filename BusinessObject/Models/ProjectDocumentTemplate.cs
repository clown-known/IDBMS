using System.ComponentModel.DataAnnotations;
using BusinessObject.Enums;

namespace BusinessObject.Models;

public class ProjectDocumentTemplate
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public DocumentTemplateType Type { get; set; } = default!;

    [Required]
    public Language Language { get; set; } = default!;

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public DateTime UpdatedDate { get; set; }

    [Required]
    public string CompanyName { get; set; } = default!;

    [Required]
    public string CompanyAddress { get; set; } = default!;
    [Required]
    public string Email { get; set; } = default!;
    [Required]
    public string Position { get; set; } = default!;

    [Required]
    public string CompanyPhone { get; set; } = default!;

    [Required]
    public string CompanyFax { get; set; } = default!;

    [Required]
    public string TaxCode { get; set; } = default!;

    [Required]
    public string AccountNo { get; set; } = default!;

    [Required]
    public string AccountName { get; set; } = default!;

    [Required]
    public string BankBranchName { get; set; } = default!;

    [Required]
    public string BankBranchAddress { get; set; } = default!;

    [Required]
    public string SwiftCode { get; set; } = default!;

    [Required]
    public string RepresentedBy { get; set; } = default!;

    public List<ProjectDocument> ProjectDocuments { get; set; } = new();
}
