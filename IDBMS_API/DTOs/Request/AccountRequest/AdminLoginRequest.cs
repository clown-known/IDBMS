using System.ComponentModel.DataAnnotations;

namespace IDBMS_API.DTOs.Request.AccountRequest
{
    public class AdminLoginRequest
    {
        [Required]
        public string Username { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}
