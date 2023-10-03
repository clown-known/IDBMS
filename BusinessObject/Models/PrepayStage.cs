using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public class PrepayStage
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public int StageNo { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string Description { get; set; } = default!;

    [Required]
    public bool IsPaid { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal TotalPaid { get; set; }

    [Required]
    public DateTime StartedDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = new();

    [Required]
    public bool IsHidden { get; set; }

    public List<Transaction> Transactions { get; set; } = new();

    public List<ConstructionTask> ConstructionTasks { get; set; } = new();
}

