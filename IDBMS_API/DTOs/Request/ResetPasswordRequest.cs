using System.ComponentModel.DataAnnotations;

namespace IDBMS_API.DTOs.Request
{
    public class ResetPasswordRequest
    {
        [Required]
        public Guid userId { get; set; }

        [Required]
        public string newPassword { get; set; }
    }
}
