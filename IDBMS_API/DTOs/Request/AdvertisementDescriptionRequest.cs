using System.ComponentModel.DataAnnotations;

namespace IDBMS_API.DTOs.Request
{
    public class AdvertisementDescriptionRequest
    {
        [Required]
        public string Description { get; set; } = default!;
        public string? EnglishDescription { get; set; }

        public IFormFile? RepresentImage { get; set; }
    }
}
