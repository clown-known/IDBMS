
using API.Supporters.JwtAuthSupport;
using BLL.Services;
using BusinessObject.Models;
using IDBMS_API.Constants;
using IDBMS_API.DTOs.Request;
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
        private readonly IUserRepository userRepository;
        private readonly JwtTokenSupporter jwtTokenSupporter;
        private readonly IConfiguration configuration;
        public UserService(IUserRepository userRepository, JwtTokenSupporter jwtTokenSupporter, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.jwtTokenSupporter = jwtTokenSupporter;
            this.configuration = configuration;
        }
        public User? GetById(Guid id)
        {
            return userRepository.GetById(id);
        }

        public (string? token, User? user) Login(string email, string password)
        {
            var user = userRepository.GetByEmail(email);
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
            userRepository.Update(user);
        }
        public async Task<User?> CreateUser(CreateAccountRequest request)
        {
            TryValidateRegisterRequest(request);
            PasswordUtils.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User()
            {
                Address = request.Address,
                Balance = 0,
                CreatedDate = DateTime.UtcNow,
                Language = request.Language,
                Id = Guid.NewGuid(),
                Email = request.Email,
                Name = request.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Phone = request.Phone,
                ExternalId = request.ExternalId ?? request.ExternalId
            };

            var userCreated = userRepository.Save(user);
            return userCreated;
        }
        
        private void TryValidateRegisterRequest(CreateAccountRequest request)
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
        public async Task UpdateUser(string userId, UpdateUserRequest request)
        {
            Guid.TryParse(userId,out Guid id);
            var user = userRepository.GetById(id) ?? throw new Exception("User not existed");
            PasswordUtils.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.Address = request.Address;
            user.Balance = 0;
            user.UpdatedDate = DateTime.UtcNow;
            user.Language = request.Language;
            user.Email = request.Email;
            user.Name = request.Name;
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            user.Phone = request.Phone;

            userRepository.Update(user);
        }
        
    }
}
