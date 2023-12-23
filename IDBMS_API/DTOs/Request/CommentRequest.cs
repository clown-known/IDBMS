using BusinessObject.Enums;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request
{
    public class CommentRequest
    {
        [Required]
        public Guid ProjectTaskId { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public CommentType Type { get; set; }

        public string? Content { get; set; } = default!;

        public Guid? ItemId { get; set; }

        public IFormFile? File { get; set; }
    }
}
