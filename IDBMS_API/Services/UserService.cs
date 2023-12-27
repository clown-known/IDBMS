
using API.Supporters.JwtAuthSupport;
using BLL.Services;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Request.AccountRequest;
using BusinessObject.Models;
using IDBMS_API.Constants;
using IDBMS_API.DTOs.Request.AccountRequest;
using IDBMS_API.Supporters.Utils;
using Repository.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Numerics;
using System.Text.RegularExpressions;
using BusinessObject.Enums;
using UnidecodeSharpFork;

namespace API.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        private readonly JwtTokenSupporter jwtTokenSupporter;
        public UserService(IUserRepository _repository, JwtTokenSupporter jwtTokenSupporter)
        {
            this._repository = _repository;
            this.jwtTokenSupporter = jwtTokenSupporter;
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

        public (string? token, User? user) Login(string email, string password)
        {
            var user = _repository.GetByEmail(email);
            if (user != null)
            {
                if (PasswordUtils.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    //if (user.Token != null)
                    //{
                    //    return (user.Token, user);
                    //}

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
            if (_repository.GetByEmail(request.Email) != null) return null;
            PasswordUtils.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
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
            user.UpdatedDate = DateTime.UtcNow;
            user.Language = request.Language;
            user.DateOfBirth= request.DateOfBirth;
            user.Email = request.Email;
            user.Name = request.Name;
            user.Phone = request.Phone;
            user.ExternalId= request.ExternalId;
            user.CompanyName = request.CompanyName;
            user.JobPosition = request.JobPosition;
            user.Role = request.Role;

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

    }
}
