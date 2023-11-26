namespace IDBMS_API.DTOs.Request
{
    public class UpdateUserRequest
    {
        public string Name { get; set; } = default!;

        public string Bio { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;

        public string Phone { get; set; } = default!;

        public string Language { get; set; } = default!;
        
        public int Status { get; set; }
        public int RoleId { get; set; }
    }
}
