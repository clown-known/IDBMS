using System.ComponentModel.DataAnnotations;

namespace IDBMS_API.DTOs.Request
{
    public class ResetPasswordRequest
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string newPassword { get; set; }
    }
}
