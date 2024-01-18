using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request.AccountRequest
{
    public class UpdatePasswordRequest
    {
        [Required]
        public Guid userId { get; set; }

        [Required]
        public string newPassword { get; set; }

        public string? oldPassword { get; set; }
    }
}
