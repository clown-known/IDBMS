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
        public double PricePercentage { get; set; }

        public bool IsPrepaid { get; set; }

        public int StageNo { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public int DecorProjectDesignId { get; set; }
    }
}
