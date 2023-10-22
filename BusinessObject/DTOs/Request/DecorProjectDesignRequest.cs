using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs.Request
{
    public class DecorProjectDesignRequest
    {
        public decimal MinBudget { get; set; }

        public decimal MaxBudget { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}
