
using API.Supporters.JwtAuthSupport;
using BLL.Services;
using BusinessObject.Models;
using IDBMS_API.Constants;
using IDBMS_API.DTOs.Request;
using Repository;
using System.Net;
using System.Numerics;
using System.Text.RegularExpressions;

namespace API.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository;
        private readonly JwtTokenSupporter jwtTokenSupporter;
        private readonly FirebaseService firebaseService;
        private readonly IConfiguration configuration;
        public UserService(IUserRepository userRepository, JwtTokenSupporter jwtTokenSupporter, FirebaseService firebaseService, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.jwtTokenSupporter = jwtTokenSupporter;
            this.firebaseService = firebaseService;
            this.configuration = configuration;
        }
        public User? GetById(string id)
        {
            return userRepository.GetById(id);
        }

        public (string? token, User? user) Login(string email, string password)
        {
            var user = userRepository.GetByEmailAndPassword(email, password);
            if (user != null)
            {
                if (user.Token != null)
                {
                    return (user.Token, user);
                }

                var token = jwtTokenSupporter.CreateToken(user);
                UpdateTokenForUser(user, token);
                return (token, user);
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

            var user = new User()
            {
                Address = request.Address,
                Balance = 0,
                CreatedDate = DateTime.UtcNow,
                Language = request.Language,
                Id = Guid.NewGuid(),
                Email = request.Email,
                Name = request.Name,
                //Password = request.Password,
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
            var user = userRepository.GetById(userId) ?? throw new Exception("User not existed");

            user.Address = request.Address;
            user.Balance = 0;
            user.UpdatedDate = DateTime.UtcNow;
            user.Language = request.Language;
            user.Email = request.Email;
            user.Name = request.Name;
            //user.Password = request.Password;
            user.Phone = request.Phone;

            userRepository.Update(user);
        }
    }
}
