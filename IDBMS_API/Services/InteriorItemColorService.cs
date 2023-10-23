using BusinessObject.DTOs.Request.CreateRequests;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class InteriorItemColorService
    {
        private readonly IInteriorItemColorRepository _repository;
        public InteriorItemColorService(IInteriorItemColorRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<InteriorItemColor> GetAll()
        {
            return _repository.GetAll();
        }
        public InteriorItemColor? GetById(int id)
        {
            return _repository.GetById(id);
        }
        public InteriorItemColor? CreateInteriorItemColor(InteriorItemColorRequest request)
        {
            var iic = new InteriorItemColor
            {
                Name = request.Name,
                Type = request.Type,
                PrimaryColor = request.PrimaryColor,
                SecondaryColor = request.SecondaryColor,
            };
            var iicCreated = _repository.Save(iic);
            return iicCreated;
        }
        public void UpdateInteriorItemColor(InteriorItemColorRequest request)
        {
            var iic = new InteriorItemColor
            {
                Name = request.Name,
                Type = request.Type,
                PrimaryColor = request.PrimaryColor,
                SecondaryColor = request.SecondaryColor,
            };
           _repository.Update(iic);
        }
        public void DeleteInteriorItemColor(int id)
        {
            _repository.DeleteById(id);
        }
    }
}
