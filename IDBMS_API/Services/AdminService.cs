using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class AdminService
    {
        private readonly IAdminRepository _repository;
        public AdminService(IAdminRepository repository)
        {
            _repository = repository;
        }   
        public IEnumerable<Admin> GetAll()
        {
            return _repository.GetAll();
        }
        public Admin? GetById(Guid id)
        {
            return _repository.GetById(id);
        }
        public Admin? CreateAdmin(AdminRequest request)
        {
            var admin = new Admin
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Username = request.Username,
                Email = request.Email,
                PasswordHash = request.PasswordHash,
                PasswordSalt = request.PasswordSalt,
                AuthenticationCode = request.AuthenticationCode,
                IsDeleted = request.IsDeleted,
                CreatorId = request.CreatorId,
            };
            var adminCreated = _repository.Save(admin);
            return adminCreated;
        }
        public void UpdateAdmin(Guid id, AdminRequest request)
        {
            var admin = _repository.GetById(id) ?? throw new Exception("This Object not existed");

            admin.Name = request.Name;
            admin.Username = request.Username;
            admin.Email = request.Email;
            admin.PasswordHash = request.PasswordHash;
            admin.PasswordSalt = request.PasswordSalt;
            admin.AuthenticationCode = request.AuthenticationCode;
            admin.IsDeleted = request.IsDeleted;
            admin.CreatorId = request.CreatorId;

            _repository.Update(admin);
        }
        public void UpdateAdminStatus(Guid id, bool IsDeleted)
        {
            var admin = _repository.GetById(id) ?? throw new Exception("This Object not existed");
            admin.IsDeleted = IsDeleted;
            _repository.Update(admin);
        }
        public void DeleteAdmin(Guid id)
        {
            _repository.DeleteById(id);
        }
    }
}
