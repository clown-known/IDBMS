using BusinessObject.Enums;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs.Request
{
    public class NotificationRequest
    {
        public NotificationCategory Category { get; set; }

        public string Content { get; set; } = default!;

        public Guid UserId { get; set; }

        public string? Link { get; set; } = default!;

        public bool IsSeen { get; set; }
    }
}
