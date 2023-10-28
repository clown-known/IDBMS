using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs.Request
{
    public class AdminRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Username { get; set; } = default!;

        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public byte[] PasswordHash { get; set; } = default!;
        [Required]
        public byte[] PasswordSalt { get; set; } = default!;

        public string? AuthenticationCode { get; set; }

        public Guid? CreatorId { get; set; }
    }
}
