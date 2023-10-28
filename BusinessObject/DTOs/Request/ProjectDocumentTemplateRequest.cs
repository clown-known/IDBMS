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
    public class ProjectDocumentTemplateRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public DocumentTemplateType Type { get; set; } = default!;

        [Required]
        public Language Language { get; set; } = default!;

        [Required]
        public string CompanyName { get; set; } = default!;

        [Required]
        public string CompanyAddress { get; set; } = default!;

        [Required]
        public string CompanyPhone { get; set; } = default!;

        [Required]
        public string CompanyFax { get; set; } = default!;

        [Required]
        public string TaxCode { get; set; } = default!;

        [Required]
        public string AccountNo { get; set; } = default!;

        [Required]
        public string AccountName { get; set; } = default!;

        [Required]
        public string BankBranchName { get; set; } = default!;

        [Required]
        public string BankBranchAddress { get; set; } = default!;

        [Required]
        public string SwiftCode { get; set; } = default!;

        [Required]
        public string RepresentedBy { get; set; } = default!;

    }
}
