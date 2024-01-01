using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request
{
    public class SiteRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public string? CompanyCode { get; set; }

        [Required]
        public string ContactName { get; set; } = default!;

        [Required]
        public string ContactEmail { get; set; } = default!;

        [Required]
        public string ContactPhone { get; set; } = default!;

        [Required]
        public string ContactLocation { get; set; } = default!;

        [Required]
        public string Address { get; set; } = default!;

    }
}
