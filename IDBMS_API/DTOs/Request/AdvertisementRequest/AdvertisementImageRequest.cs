using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace IDBMS_API.DTOs.Request.AdvertisementRequest
{
    public class AdvertisementImageRequest
    {
        public IFormFile? File { get; set; } = default!;

        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public bool IsPublicAdvertisement { get; set; }
    }
}
