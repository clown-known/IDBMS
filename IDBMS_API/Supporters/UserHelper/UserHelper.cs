using Azure.Core;
using BusinessObject.Models;
using IDBMS_API.DTOs.Request.AccountRequest;
using IDBMS_API.Supporters.Utils;
using Microsoft.AspNetCore.Identity;
using Repository.Implements;
using Repository.Interfaces;
using System.Text;

namespace IDBMS_API.Supporters.UserHelper
{
    public class UserHelper
    {
        public static (User? user,string password) GennarateViewerUserForProject(Guid projectId,string email)
        {
            ProjectParticipationRepository projectParticipationRepository = new ProjectParticipationRepository();
            UserRepository userRepository = new UserRepository();

            string password = GennaratePassword();

            User owner = projectParticipationRepository.GetProjectOwnerByProjectId(projectId).User;
            PasswordUtils.CreatePasswordHash(password,out byte[] hashpass,out byte[] saltpass);
            User? result = userRepository.Save(new User
            {
                Email = email,
                PasswordHash = hashpass,
                PasswordSalt = saltpass,
                CompanyName = owner.CompanyName,
                Status = BusinessObject.Enums.UserStatus.Unverified,
                Role = BusinessObject.Enums.CompanyRole.Customer,
            });
            if (result == null) return (null,null);

            return (result,password);
        }
        public static (User? user,string password) GenerateUser(CreateUserRequest request)
        {
            string password = GennaratePassword();
            PasswordUtils.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            UserRepository userRepository = new UserRepository();

            var user = new User()
            {
                Address = request.Address,
                CreatedDate = TimeHelper.TimeHelper.GetTime(DateTime.UtcNow),
                Language = request.Language,
                Id = Guid.NewGuid(),
                Email = request.Email,
                Name = request.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Phone = request.Phone,
                Role = request.Role,
            };

            var userCreated = userRepository.Save(user);

            return (userCreated, password);
        }
        private static string GennaratePassword()
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (res.Length <= 16)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
