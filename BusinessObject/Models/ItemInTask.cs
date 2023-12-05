using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class ItemInTask
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "money")]
        public decimal? EstimatePrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public Guid InteriorItemId { get; set; }
        public InteriorItem InteriorItem { get; set; }

        [Required]
        public Guid ProjectTaskId { get; set; }
        public ProjectTask ProjectTask { get; set; }
    }
}
