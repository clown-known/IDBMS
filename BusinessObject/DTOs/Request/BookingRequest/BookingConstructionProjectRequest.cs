using BusinessObject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs.Request.BookingRequest
{
    public class BookingConstructionProjectRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? CompanyName { get; set; }

        public string? CompanyAddress { get; set; }

        public string? CompanyCode { get; set; }

        public string? Description { get; set; }

        [Required]
        public int ProjectCategoryId { get; set; }

        [Required]
        public int Language { get; set; }

        public Guid? BasedOnDecorProjectId { get; set; }

        public List<BookingDocumentRequest>? Documents { get; set; }
    }
}
