using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using API.Supporters.JwtAuthSupport;
using IDBMS_API.Supporters.Utils;
using IDBMS_API.Constants;
using IDBMS_API.DTOs.Request.AccountRequest;
using System.Text.RegularExpressions;

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

        public bool CheckByUsername(string username)
        {
            var admin = _repository.GetByUsername(username);

            return admin != null;
        }

        private void TryValidateRequest(AdminRequest request)
        {
            if (new Regex(RegexCollector.EmailRegex).IsMatch(request.Email) == false)
            {
                throw new Exception("Email is not valid.");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                throw new Exception("Fullname must not be null or empty");
            }

        }

        public Admin? CreateAdmin(AdminRequest request)
        {
            TryValidateRequest(request);
            if (_repository.GetByEmail(request.Email) != null) return null;
            PasswordUtils.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var admin = new Admin
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsDeleted = false,
                CreatorId = request.CreatorId,
            };
            var adminCreated = _repository.Save(admin);
            return adminCreated;
        }
        public void UpdateAdmin(Guid id, AdminRequest request)
        {
            TryValidateRequest(request);
            var admin = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            PasswordUtils.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            admin.Name = request.Name;
            admin.Username = request.Username;
            admin.Email = request.Email;
            admin.PasswordHash = passwordHash;
            admin.PasswordSalt = passwordSalt;
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
