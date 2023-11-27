using BusinessObject.Enums;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request.BookingRequest
{
    public class BookingTaskRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Required]
        public CalculationUnit CalculationUnit { get; set; }

        [Required]
        public double UnitInContract { get; set; }

        public Guid? InteriorItemId { get; set; }
    }
}
