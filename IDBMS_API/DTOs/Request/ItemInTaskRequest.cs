using BusinessObject.Models;
using System.ComponentModel.DataAnnotations;

namespace IDBMS_API.DTOs.Request
{
    public class ItemInTaskRequest
    {
        public decimal? EstimatePrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public Guid InteriorItemId { get; set; }

        [Required]
        public Guid ProjectTaskId { get; set; }
    }
}
