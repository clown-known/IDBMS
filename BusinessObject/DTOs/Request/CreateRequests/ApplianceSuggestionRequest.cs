using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs.Request.CreateRequests
{
    public class ApplianceSuggestionRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public Guid? InteriorItemId { get; set; }
        public InteriorItem? InteriorItem { get; set; }

        [Required]
        public Guid RoomId { get; set; }

        public Guid? ProjectId { get; set; }
    }
}
