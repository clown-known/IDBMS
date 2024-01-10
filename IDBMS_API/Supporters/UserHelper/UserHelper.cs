using BusinessObject.Models;
using IDBMS_API.Supporters.Utils;
using Microsoft.AspNetCore.Identity;
using Repository.Implements;
using Repository.Interfaces;
using System.Text;

namespace IDBMS_API.Supporters.UserHelper
{
    public class UserHelper
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        public UserHelper(IUserRepository userRepository,IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }
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
        private static string GennaratePassword()
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 <= 16)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
