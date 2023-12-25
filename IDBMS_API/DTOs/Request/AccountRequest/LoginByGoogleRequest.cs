using System.ComponentModel.DataAnnotations;

namespace IDBMS_API.DTOs.Request.AccountRequest
{
    public class LoginByGoogleRequest
    {
        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public string GoogleToken { get; set; } = default!;
    }
}
