using BusinessObject.Models;
using System.ComponentModel.DataAnnotations;

namespace IDBMS_API.DTOs.Request
{
    public class ItemInTaskRequest
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public Guid ProjectTaskId { get; set; }

        public Guid? InteriorItemId { get; set; }
        public InteriorItemRequest? newItem { get; set; }
    }
}
