using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IDBMS_API.DTOs.Request
{
    public class CreateAccountRequest
    {
        public string Name { get; set; } = default!;

        public string Bio { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;

        public string Phone { get; set; } = default!;

        public string Language { get; set; } = default!;
        
        public int Status { get; set; }
        public string ExternalId { get; set; } = default!;

    }
}
