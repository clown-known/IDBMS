using BusinessObject.Enums;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request
{
    public class TransactionRequest
    {

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public string? Note { get; set; }

        [Required]
        public string PayerName { get; set; }

        public Guid? UserId { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public TransactionStatus Status { get; set; }

        public IFormFile? TransactionReceiptImage { get; set; } = default!;
    }
}
