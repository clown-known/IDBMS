
using API.Supporters.JwtAuthSupport;
using BLL.Services;
using BusinessObject.DTOs.Request;
using BusinessObject.DTOs.Request.AccountRequest;
using BusinessObject.Models;
using IDBMS_API.Constants;
using IDBMS_API.Supporters.Utils;
using Repository.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Numerics;
using System.Text.RegularExpressions;

namespace API.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        private readonly JwtTokenSupporter jwtTokenSupporter;
        private readonly IConfiguration configuration;
        public UserService(IUserRepository _repository, JwtTokenSupporter jwtTokenSupporter, IConfiguration configuration)
        {
            this._repository = _repository;
            this.jwtTokenSupporter = jwtTokenSupporter;
            this.configuration = configuration;
        }
        public User? GetById(Guid id)
        {
            return _repository.GetById(id);
        }
        public IEnumerable<User> GetAll()
        {
            return _repository.GetAll();
        }

        public (string? token, User? user) Login(string email, string password)
        {
            var user = _repository.GetByEmail(email);
            if (user != null)
            {
                if (PasswordUtils.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    if (user.Token != null)
                    {
                        return (user.Token, user);
                    }

                    var token = jwtTokenSupporter.CreateToken(user);
                    UpdateTokenForUser(user, token);
                    return (token, user);
                }
            }
            return (null, null);
        }

        private void UpdateTokenForUser(User user, string token)
        {
            user.Token = token;
            _repository.Update(user);
        }
        public User? CreateUser(CreateUserRequest request)
        {
            TryValidateRegisterRequest(request);
            PasswordUtils.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User()
            {
                Address = request.Address,
                Balance = 0,
                CreatedDate = DateTime.UtcNow,
                Language = request.Language,
                DateOfBirth = request.DateOfBirth,
                Id = Guid.NewGuid(),
                Email = request.Email,
                Name = request.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Phone = request.Phone,
                ExternalId = request.ExternalId,
            };

            var userCreated = _repository.Save(user);
            return userCreated;
        }
        
        private void TryValidateRegisterRequest(CreateUserRequest request)
        {
            if (new Regex(RegexCollector.PhoneRegex).IsMatch(request.Phone) == false)
            {
                throw new Exception("Phone is not a valid phone");
            }
            if (new Regex(RegexCollector.EmailRegex).IsMatch(request.Email) == false)
            {
                throw new Exception("Email is not valid.");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                throw new Exception("Fullname must not be null or empty");
            }
            if (string.IsNullOrEmpty(request.Address))
            {
                throw new Exception("Address must not be null or empty");
            }

        }
        public void UpdateUser(Guid userId, UpdateUserRequest request)
        {
            var user = _repository.GetById(userId) ?? throw new Exception("User not existed");

            user.Address = request.Address;
            user.Balance = 0;
            user.UpdatedDate = DateTime.UtcNow;
            user.Language = request.Language;
            user.DateOfBirth= request.DateOfBirth;
            user.Email = request.Email;
            user.Name = request.Name;
            user.Phone = request.Phone;
            user.ExternalId= request.ExternalId;

            _repository.Update(user);
        }

        public void UpdateUserPassword(UpdatePasswordRequest request)
        {
            var user = _repository.GetById(request.userId) ?? throw new Exception("User not existed");

            if (!PasswordUtils.VerifyPasswordHash(request.oldPassword, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Password not match!");

            PasswordUtils.CreatePasswordHash(request.newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            _repository.Update(user);
        }

    }
}
