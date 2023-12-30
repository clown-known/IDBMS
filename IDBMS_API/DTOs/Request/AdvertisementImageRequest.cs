using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace IDBMS_API.DTOs.Request
{
    public class AdvertisementImageRequest
    {
        public IFormFile? file { get; set; } = default!;

        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public bool IsPublicAdvertisement { get; set; }
    }
}
