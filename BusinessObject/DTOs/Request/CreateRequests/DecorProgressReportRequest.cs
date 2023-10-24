using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs.Request.CreateRequests
{
    public class DecorProgressReportRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public Guid PrepayStageId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
