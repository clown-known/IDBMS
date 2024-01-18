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
using Azure.Core;

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
            string? search)
        {
            IEnumerable<Admin> filteredList = list;

            if (search != null)
            {
                filteredList = filteredList.Where(item =>
                            (item.Email != null && item.Email.Unidecode().IndexOf(search.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (item.Username != null && item.Username.Unidecode().IndexOf(search.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (item.Name != null && item.Name.Unidecode().IndexOf(search.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
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
                    var token = jwtTokenSupporter.CreateTokenForAdmin(admin);
                    UpdateTokenForAdmin(admin, token);
                    return (token, admin);
                }
            }
            return (null, null);
        }
        public IEnumerable<Admin> GetAll(string? search)
        {
            var list = _repository.GetAll();

            return Filter(list, search);
        }
        public Admin? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This admin id is not existed!");
        }

        public bool CheckByUsername(string username)
        {
            var admin = _repository.GetByUsername(username);

            return admin != null;
        }

        public string GenerateCode(string name)
        {
            string username = String.Empty;
            Random random = new();

            for (int attempt = 0; attempt < 10; attempt++)
            {
                // Generate the code
                username = GenerateSingleCode(name, random);


                bool usernameExists = CheckByUsername(username);

                if (usernameExists == false)
                {
                    return username;
                }
            }

            throw new Exception("Failed to generate a unique username after 10 attempts.");
        }

        public string GenerateSingleCode(string name, Random random)
        {
            string username = String.Empty;

            string[] words = name.Split(' ');

            if (words.Length > 0)
            {
                // Take the entire last name
                username += words[words.Length - 1];

                // Take the first character
                for (int i = 0; i < words.Length - 1; i++)
                {
                    if (!string.IsNullOrEmpty(words[i]))
                    {
                        username += char.ToUpper(words[i][0]);
                    }
                }
            }

            username += random.Next(10, 99);

            return username;
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

            if (_repository.GetByEmail(request.Email) != null) throw new Exception("This admin email is existed!");

            if (request.Password == null) throw new Exception("Password is required!");

            PasswordUtils.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var admin = new Admin
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Username = GenerateCode(request.Name),
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
            var admin = _repository.GetById(id) ?? throw new Exception("This admin id is not existed!");

            admin.Name = request.Name;
            admin.Email = request.Email;

            _repository.Update(admin);
        }

        public void UpdateAdminPassword(UpdatePasswordRequest request)
        {
            var admin = _repository.GetById(request.userId) ?? throw new Exception("This admin id is not existed!");

            PasswordUtils.CreatePasswordHash(request.newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            admin.PasswordHash = passwordHash;
            admin.PasswordSalt = passwordSalt;

            _repository.Update(admin);
        }

        public void UpdateTokenForAdmin(Admin admin, string token)
        {
            admin.token = token;
            _repository.Update(admin);
        }

        public void DeleteAdmin(Guid id)
        {
            var admin = _repository.GetById(id) ?? throw new Exception("This admin id is not existed!");

            admin.IsDeleted = true;

            _repository.Update(admin);
        }
    }
}
