using BusinessObject.Enums;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs.Request.CreateRequests
{
    public class CommentRequest
    {
        [Required]
        public string Content { get; set; } = default!;

        public Guid? ConstructionTaskId { get; set; }

        public Guid? DecorProgressReportId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public string? FileUrl { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }

        public DateTime? LastModifiedTime { get; set; }

        [Required]
        public CommentStatus Status { get; set; }
    }
}
