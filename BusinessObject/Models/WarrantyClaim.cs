using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class WarrantyClaim
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        public string? Reason { get; set; }

        public string? Solution { get; set; }

        public string? Note { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal TotalPaid { get; set; }

        [Required]
        public bool IsCompanyCover { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? ConfirmationDocument { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        public Guid? UserId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public List<Transaction> Transactions { get; set; } = new();
    }
}
