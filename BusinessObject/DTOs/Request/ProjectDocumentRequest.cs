﻿using BusinessObject.Enums;
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
        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Required]
        public string Url { get; set; } = default!;

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public ProjectDocumentCategory Category { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public int ProjectDocumentTemplateId { get; set; }

        [Required]
        public bool IsPublicAdvertisement { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

    }
}