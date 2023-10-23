using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs.Request.CreateRequests
{
    public class CreateAccountRequest
    {
        public string Name { get; set; } = default!;

        public string Bio { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;

        public string Phone { get; set; } = default!;

        public Enums.Language Language { get; set; } = default!;

        public int Status { get; set; }
        public string ExternalId { get; set; } = default!;

    }
}
