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
    public class ProjectDocumentRequest
    {
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public string Url { get; set; } = default!;

        public DateTime CreatedDate { get; set; }

        public ProjectDocumentCategory Category { get; set; }

        public Guid ProjectId { get; set; }

        public Guid? ConstructionTaskReportId { get; set; }

        public Guid? DecorProgressReportId { get; set; }

        public int ProjectDocumentTemplateId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
