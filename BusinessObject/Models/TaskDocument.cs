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

        public string? Description { get; set; }

        public string? Document { get; set; } = default!;

        [Required]
        public Guid TaskReportId { get; set; }
        public TaskReport TaskReport { get; set; } = new();

        [Required]
        public bool IsDeleted { get; set; }
    }
}
