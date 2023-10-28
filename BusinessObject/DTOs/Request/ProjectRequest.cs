using BusinessObject.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs.Request
{
    public class ProjectRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string CompanyName { get; set; } = default!;

        [Required]
        public string Location { get; set; } = default!;

        public string? Description { get; set; }

        [Required]
        public ProjectType Type { get; set; }

        [Required]
        public int ProjectCategoryId { get; set; }

        public int? NoStage { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal EstimatedPrice { get; set; }

        [DataType(DataType.Currency)]
        public decimal? FinalPrice { get; set; }

        public Guid? CurrentStageId { get; set; }

        [Required]
        public int Language { get; set; }

        [Required]
        public ProjectStatus Status { get; set; }

        public string? AdminNote { get; set; }

        public Guid? BasedOnDecorProjectId { get; set; }

        public int? DecorProjectDesignId { get; set; }
    }
}