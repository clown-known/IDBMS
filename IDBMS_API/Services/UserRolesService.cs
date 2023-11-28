
using BusinessObject.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using IDBMS_API.Constants;
using IDBMS_API.DTOs.Request;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class UserRolesService
    {
        private readonly IUserRoleRepository _repository;
        public UserRolesService(IUserRoleRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<UserRole> GetByUserId(Guid userid)
        {
            return _repository.GetByUserId(userid) ?? new List<UserRole>();
        }
        public UserRole? GetById(int id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }

        public UserRole? AddRoleToUser(UserRoleRequest request)
        {
            var userRole = new UserRole
            {
                UserId = request.UserId,
                Role = request.Role
            };
            var userRoleCreated = _repository.Save(userRole);
            return userRoleCreated;
        }
        public void UpdateUserRoles(int id, UserRoleRequest request)
        {
            var userRole = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            userRole.UserId = request.UserId;
            userRole.Role = request.Role;

            _repository.Update(userRole);
        }

        public void DeleteUserRoles(int id)
        {
            var trans = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            _repository.DeleteById(id);
        }
    }
}
