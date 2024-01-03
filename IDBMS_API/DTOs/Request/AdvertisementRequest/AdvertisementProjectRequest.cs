using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IDBMS_API.DTOs.Request.AdvertisementRequest
{
    public class AdvertisementProjectRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public ProjectType Type { get; set; }

        [Required]
        public int ProjectCategoryId { get; set; }

        [Required]
        public string CreatedAdminUsername { get; set; } = default!;

        [Required]
        public Guid CreatedByAdminId { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal EstimatedPrice { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal FinalPrice { get; set; }

        [Required]
        public double Area { get; set; }

        [Required]
        public Language Language { get; set; }        
        
        [Required]
        public int EstimateBusinessDay { get; set; }
    }
}
