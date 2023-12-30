using System.ComponentModel.DataAnnotations;

namespace IDBMS_API.DTOs.Request.AdvertisementRequest
{
    public class AdvertisementDescriptionRequest
    {
        public string? AdvertisementDescription { get; set; }
        public string? EnglishAdvertisementDescription { get; set; }

        public IFormFile? RepresentImage { get; set; }
    }
}
