
using API.Supporters.JwtAuthSupport;
using BLL.Services;
<<<<<<< HEAD
using BusinessObject.Models;
using IDBMS_API.Constants;
using IDBMS_API.DTOs.Request;
=======
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Request.AccountRequest;
using BusinessObject.Models;
using IDBMS_API.Constants;
using IDBMS_API.DTOs.Request.AccountRequest;
using IDBMS_API.Supporters.Utils;
>>>>>>> dev
using Repository.Interfaces;
using System.Net;
using System.Numerics;
using System.Text.RegularExpressions;
using BusinessObject.Enums;
using UnidecodeSharpFork;
using System.Security.Cryptography;

namespace API.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository;
        private readonly JwtTokenSupporter jwtTokenSupporter;
<<<<<<< HEAD
        private readonly FirebaseService firebaseService;
        private readonly IConfiguration configuration;
        public UserService(IUserRepository userRepository, JwtTokenSupporter jwtTokenSupporter, FirebaseService firebaseService, IConfiguration configuration)
=======
        public UserService(IUserRepository _repository, JwtTokenSupporter jwtTokenSupporter)
>>>>>>> dev
        {
            this.userRepository = userRepository;
            this.jwtTokenSupporter = jwtTokenSupporter;
<<<<<<< HEAD
            this.firebaseService = firebaseService;
            this.configuration = configuration;
        }
        public User? GetById(string id)
        {
            return userRepository.GetById(id);
=======
        }

        public IEnumerable<User> Filter(IEnumerable<User> list,
           string? searchParam, CompanyRole? role , UserStatus? status)
        {
            IEnumerable<User> filteredList = list;

            if (searchParam != null)
            {
                filteredList = filteredList.Where(item =>
                            (item.Name != null && item.Name.Unidecode().IndexOf(searchParam.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (item.Email != null && item.Email.Unidecode().IndexOf(searchParam.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (item.Phone != null && item.Phone.Unidecode().IndexOf(searchParam.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            if (role != null)
            {
                filteredList = filteredList.Where(item => item.Role == role);
            }

            if (status != null)
            {
                filteredList = filteredList.Where(item => item.Status == status);
            }

            return filteredList;
        }

        public User? GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<User> GetAll(string? searchParam, CompanyRole? role, UserStatus? status)
        {
            var list = _repository.GetAll();    

            return Filter(list, searchParam, role, status);
        }

        public User? GetByEmail(string email)
        {
            return _repository.GetByEmail(email);
>>>>>>> dev
        }

        public (string? token, User? user) Login(string email, string password)
        {
            var user = userRepository.GetByEmailAndPassword(email, password);
            if (user != null)
            {
                if (user.Token != null)
                {
<<<<<<< HEAD
                    return (user.Token, user);
=======
                    //if (user.Token != null)
                    //{
                    //    return (user.Token, user);
                    //}

                    var token = jwtTokenSupporter.CreateToken(user);
                    UpdateTokenForUser(user, token);
                    return (token, user);
>>>>>>> dev
                }

                var token = jwtTokenSupporter.CreateToken(user);
                UpdateTokenForUser(user, token);
                return (token, user);
            }
            return (null, null);
        }

        public (string? token, User? user) LoginByGoogle(LoginByGoogleRequest request)
        {
            var user = _repository.GetByEmail(request.Email);

            if (user != null && user.Status == UserStatus.Active && user.ExternalId == request.GoogleId)
            {
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
<<<<<<< HEAD

=======
            if (_repository.GetByEmail(request.Email) != null) return null;
            PasswordUtils.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
>>>>>>> dev
            var user = new User()
            {
                Address = request.Address,
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
                CompanyName = request.CompanyName,
                JobPosition = request.JobPosition,
                Role= request.Role,
            };

            var userCreated = _repository.Save(user);
            return userCreated;
        }

        public static string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-=_+";

            using RandomNumberGenerator rng = RandomNumberGenerator.Create();

            byte[] data = new byte[length];
            rng.GetBytes(data);

            // Convert bytes to characters
            char[] password = new char[length];
            for (int i = 0; i < length; i++)
            {
                password[i] = chars[data[i] % chars.Length];
            }

            return new string(password);
        }

        public User? GenerateUser(CreateUserRequest request)
        {
            if (_repository.GetByEmail(request.Email) != null) 
                return null;

            PasswordUtils.CreatePasswordHash(GenerateRandomPassword(8), out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User()
            {
                Address = request.Address,
                CreatedDate = DateTime.UtcNow,
                Language = request.Language,
                Id = Guid.NewGuid(),
                Email = request.Email,
                Name = request.Name,
                //Password = request.Password,
                Phone = request.Phone,
<<<<<<< HEAD
                ExternalId = request.ExternalId ?? request.ExternalId
            };

            var userCreated = userRepository.Save(user);
            return userCreated;
        }
        
        private void TryValidateRegisterRequest(CreateAccountRequest request)
=======
                Role = request.Role,
            };

            var userCreated = _repository.Save(user);

            //noti user by email

            return userCreated;
        }

        private void TryValidateRegisterRequest(CreateUserRequest request)
>>>>>>> dev
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
<<<<<<< HEAD
        public async Task UpdateUser(string userId, UpdateUserRequest request)
        {
            var user = userRepository.GetById(userId) ?? throw new Exception("User not existed");
=======
        public void UpdateUser(Guid userId, UpdateUserRequest request)
        {
            var user = _repository.GetById(userId) ?? throw new Exception("User not existed");
>>>>>>> dev

            user.Address = request.Address;
            user.UpdatedDate = DateTime.UtcNow;
            user.Language = request.Language;
            user.DateOfBirth= request.DateOfBirth;
            user.Email = request.Email;
            user.Name = request.Name;
<<<<<<< HEAD
            //user.Password = request.Password;
=======
>>>>>>> dev
            user.Phone = request.Phone;
            user.ExternalId= request.ExternalId;
            user.CompanyName = request.CompanyName;
            user.JobPosition = request.JobPosition;
            user.Role = request.Role;

            userRepository.Update(user);
        }
<<<<<<< HEAD
=======

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

        public void UpdateUserStatus(Guid id, UserStatus status)
        {
            var user = _repository.GetById(id) ?? throw new Exception("User not existed");

            user.Status= status;

            _repository.Update(user);
        }

        public void UpdateUserRole(Guid id, CompanyRole role)
        {
            var user = _repository.GetById(id) ?? throw new Exception("User not existed");

            user.Role = role;

            _repository.Update(user);
        }

>>>>>>> dev
    }
}
