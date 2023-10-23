using BusinessObject.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs.Request.UpdateRequests
{
    public class UpdateProjectRequest
    {
        [Required]
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string CompanyName { get; set; } = default!;

        public string Location { get; set; } = default!;

        public string? Description { get; set; }

        public ProjectType Type { get; set; }

        public int ProjectCategoryId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int? NoStage { get; set; }

        [DataType(DataType.Currency)]
        public decimal EstimatedPrice { get; set; }

        [DataType(DataType.Currency)]
        public decimal? FinalPrice { get; set; }

        public Guid? CurrentStageId { get; set; }

        public int Language { get; set; }

        public ProjectStatus Status { get; set; }

        public bool IsAdvertisement { get; set; }

        public string? AdminNote { get; set; }

        public Guid? BasedOnDecorProjectId { get; set; }

        public int? DecorProjectDesignId { get; set; }
    }
}
