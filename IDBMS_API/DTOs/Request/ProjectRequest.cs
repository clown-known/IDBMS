using BusinessObject.Enums;
using BusinessObject.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IDBMS_API.DTOs.Request
{
    public class ProjectRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Required]
        public ProjectType Type { get; set; }

        [Required]
        public int ProjectCategoryId { get; set; }

        [Required]
        public string CreatedAdminUsername { get; set; } = default!;

        [Required]
        public Guid CreatedByAdminId { get; set; }

        public decimal? EstimatedPrice { get; set; }

        public decimal? FinalPrice { get; set; }

        public decimal? TotalWarrantyPaid { get; set; }

        [Required]
        public double Area { get; set; }

        public int? EstimateBusinessDay { get; set; }

        [Required]
        public int Language { get; set; }

        [Required]
        public ProjectStatus Status { get; set; }

        [Required]
        public AdvertisementStatus AdvertisementStatus { get; set; }

        public Guid? BasedOnDecorProjectId { get; set; }

        public int? ProjectDesignId { get; set; }

        [Required]
        public Guid SiteId { get; set; }
    }
}