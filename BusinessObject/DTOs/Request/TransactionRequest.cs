using BusinessObject.Enums;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs.Request
{
    public class TransactionRequest
    {

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Required]
        public string? Note { get; set; }

        [Required]
        public Guid PrepayStageId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string TransactionReceiptImageUrl { get; set; } = default!;

        public string? AdminNote { get; set; }
    }
}
