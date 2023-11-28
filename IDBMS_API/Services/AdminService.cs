using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using API.Supporters.JwtAuthSupport;
using IDBMS_API.Supporters.Utils;

namespace IDBMS_API.Services
{
    public class AdminService
    {
        private readonly IAdminRepository _repository;
        private readonly JwtTokenSupporter jwtTokenSupporter;
        public AdminService(IAdminRepository repository, JwtTokenSupporter jwtTokenSupporter)
        {
            _repository = repository;
            this.jwtTokenSupporter = jwtTokenSupporter;
        }
        public (string? token, Admin? user) Login(string email, string password)
        {
            var user = _repository.GetByEmail(email);
            if (user != null)
            {
                if (PasswordUtils.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    if (user.token != null)
                    {
                        return (user.token, user);
                    }

                    var token = jwtTokenSupporter.CreateTokenForAdmin(user);
                    UpdateTokenForAdmin(user, token);
                    return (token, user);
                }
            }
            return (null, null);
        }
        public IEnumerable<Admin> GetAll()
        {
            return _repository.GetAll();
        }
        public Admin? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public Admin? CreateAdmin(AdminRequest request)
        {
            var admin = new Admin
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Username = request.Username,
                Email = request.Email,
                PasswordHash = request.PasswordHash,
                PasswordSalt = request.PasswordSalt,
                AuthenticationCode = request.AuthenticationCode,
                IsDeleted = false,
                CreatorId = request.CreatorId,
            };
            var adminCreated = _repository.Save(admin);
            return adminCreated;
        }
        public void UpdateAdmin(Guid id, AdminRequest request)
        {
            var admin = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            admin.Name = request.Name;
            admin.Username = request.Username;
            admin.Email = request.Email;
            admin.PasswordHash = request.PasswordHash;
            admin.PasswordSalt = request.PasswordSalt;
            admin.AuthenticationCode = request.AuthenticationCode;
            admin.CreatorId = request.CreatorId;

            _repository.Update(admin);
        }
        public void UpdateTokenForAdmin(Admin admin, string token)
        {
            admin.token = token;
            _repository.Update(admin);
        }
        public void DeleteAdmin(Guid id)
        {
            var admin = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            admin.IsDeleted = true;

            _repository.Update(admin);
        }
    }
}
