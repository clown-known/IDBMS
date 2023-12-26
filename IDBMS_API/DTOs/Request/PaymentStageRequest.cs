using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Enums;

namespace IDBMS_API.DTOs.Request
{
    public class PaymentStageRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Required]
        public bool IsPrepaid { get; set; }

        [Required]
        public double PricePercentage { get; set; }

        public DateTime? EndTimePayment { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
    }
}
