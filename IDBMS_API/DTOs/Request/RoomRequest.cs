using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Enums;

namespace IDBMS_API.DTOs.Request
{
    public class RoomRequest
    {

        [Required]
        public Guid FloorId { get; set; }

        public string? Description { get; set; }

        [Required]
        public string UsePurpose { get; set; } = default!;

        [Required]
        public double Area { get; set; }

        public int? RoomTypeId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        public Language? Language { get; set; }
    }
}
