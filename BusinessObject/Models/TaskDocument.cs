using BusinessObject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class TaskDocument
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        public string? Document { get; set; } = default!;

        [Required]
        public Guid TaskReportId { get; set; }
        public TaskReport TaskReport { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
