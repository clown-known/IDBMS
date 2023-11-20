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
    public class PaymentStageRequest
    {
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
        public decimal TotalContractPaid { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalIncurringPaid { get; set; }

        [Required]
        public bool IsPrepaid { get; set; }

        [Required]
        public double PricePercentage { get; set; }

        [Required]
        public DateTime PaidDate { get; set; }

        [Required]
        public DateTime StartedDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public DateTime EndTimePayment { get; set; }

        [Column(TypeName = "money")]
        public decimal? PenaltyFee { get; set; }

        [Required]
        public int EstimateBusinessDay { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public bool IsHidden { get; set; }
    }
}
