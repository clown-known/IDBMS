using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace IDBMS_API.DTOs.Request
{
    public class AdvertisementImageRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public IFormFile? file { get; set; } = default!;

        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public bool IsPublicAdvertisement { get; set; }
    }
}
