using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace IDBMS_API.DTOs.Request
{
    public class CreateParticipationListRequest
    {
        [Required]
        public ParticipationRole Role { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public List<Guid> ListUserId { get; set; }
    }
}
