using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.DeletedTable;

public class DecorProjectDesign
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal MinBudget { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal MaxBudget { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    public List<PaymentStageDesign> PaymentStageDesigns { get; set; } = new();
    public List<Project> Projects { get; set; } = new();
}
