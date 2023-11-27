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
    public class NotificationRequest
    {
        [Required]
        public NotificationCategory Category { get; set; }
        [Required]
        public string Content { get; set; } = default!;

        public string? Link { get; set; } = default!;
        [Required]
        public bool IsSeen { get; set; }

        public List<Guid>? listUserId { get; set; }
    }
}
