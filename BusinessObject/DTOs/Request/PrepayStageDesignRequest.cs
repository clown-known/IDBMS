using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs.Request
{
    public class PrepayStageDesignRequest
    {
        [Required]
        public double PricePercentage { get; set; }

        [Required]
        public bool IsPrepaid { get; set; }

        [Required]
        public int StageNo { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Required]
        public int DecorProjectDesignId { get; set; }
    }
}
