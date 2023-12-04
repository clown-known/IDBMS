using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request.AccountRequest
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}
