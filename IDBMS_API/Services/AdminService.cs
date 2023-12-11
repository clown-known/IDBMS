using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using API.Supporters.JwtAuthSupport;
using IDBMS_API.Supporters.Utils;
using IDBMS_API.Constants;
using IDBMS_API.DTOs.Request.AccountRequest;
using System.Text.RegularExpressions;
using UnidecodeSharpFork;
using BusinessObject.Enums;

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

        private IEnumerable<Admin> Filter(IEnumerable<Admin> list,
            string? search, AdminStatus? status)
        {
            IEnumerable<Admin> filteredList = list;

            if (search != null)
            {
                filteredList = filteredList.Where(item =>
                            (item.Email != null && item.Email.Unidecode().IndexOf(search.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (item.Username != null && item.Username.Unidecode().IndexOf(search.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (item.Name != null && item.Name.Unidecode().IndexOf(search.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            if (status != null)
            {
                filteredList = filteredList.Where(item => item.Status == status);
            }

            return filteredList;
        }

        public (string? token, Admin? admin) Login(string username, string password)
        {
            var admin = _repository.GetByUsername(username);
            if (admin != null)
            {
                if (PasswordUtils.VerifyPasswordHash(password, admin.PasswordHash, admin.PasswordSalt))
                {
                    if (admin.token != null)
                    {
                        return (admin.token, admin);
                    }

                    var token = jwtTokenSupporter.CreateTokenForAdmin(admin);
                    UpdateTokenForAdmin(admin, token);
                    return (token, admin);
                }
            }
            return (null, null);
        }
        public IEnumerable<Admin> GetAll(string? search, AdminStatus? status)
        {
            var list = _repository.GetAll();

            return Filter(list, search, status);
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
