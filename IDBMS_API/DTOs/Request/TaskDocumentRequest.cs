using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request
{
    public class TaskDocumentRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public IFormFile? Document { get; set; } = default!;

        [Required]
        public Guid TaskReportId { get; set; }
    }
}
