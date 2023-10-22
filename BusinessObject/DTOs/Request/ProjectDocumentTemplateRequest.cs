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
        public string Name { get; set; } = default!;

        public DocumentTemplateType Type { get; set; } = default!;

        public Language Language { get; set; } = default!;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string CompanyName { get; set; } = default!;

        public string CompanyAddress { get; set; } = default!;

        public string CompanyPhone { get; set; } = default!;

        public string CompanyFax { get; set; } = default!;

        public string TaxCode { get; set; } = default!;

        public string AccountNo { get; set; } = default!;

        public string AccountName { get; set; } = default!;

        public string BankBranchName { get; set; } = default!;

        public string BankBranchAddress { get; set; } = default!;

        public string SwiftCode { get; set; } = default!;

        public string RepresentedBy { get; set; } = default!;

    }
}
