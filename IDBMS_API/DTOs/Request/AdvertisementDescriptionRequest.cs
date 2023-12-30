using System.ComponentModel.DataAnnotations;

namespace IDBMS_API.DTOs.Request
{
    public class AdvertisementDescriptionRequest
    {
        public IFormFile? RepresentImage { get; set; }
    }
}
