using BusinessObject.Enums;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request.BookingRequest
{
    public class BookingDecorProjectRequest
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
        [DataType(DataType.Currency)]
        public decimal EstimatedPrice { get; set; }

        [Required]
        public int Language { get; set; }

        public int? ProjectDesignId { get; set; }
        
        [Required]
        public int EstimateBusinessDay { get; set; }

        public List<BookingSiteRequest>? Sites { get; set; }
        public List<BookingDocumentRequest>? Documents { get; set; }
    }
}
