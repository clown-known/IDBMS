using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;
using BusinessObject.Enums;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class InteriorItemColorService
    {
        private readonly IInteriorItemColorRepository _repository;
        public InteriorItemColorService(IInteriorItemColorRepository repository)
        {
            _repository = repository;
        }

        private IEnumerable<InteriorItemColor> Filter(IEnumerable<InteriorItemColor> list,
            ColorType? type, string? name)
        {
            IEnumerable<InteriorItemColor> filteredList = list;

            if (type != null)
            {
                filteredList = filteredList.Where(item => item.Type == type);
            }

            if (name != null)
            {
                filteredList = filteredList.Where(item => item.Name == name);
            }

            return filteredList;
        }

        public IEnumerable<InteriorItemColor> GetAll(ColorType? type, string? name)
        {
            var list = _repository.GetAll();

            return Filter(list, type, name);
        }
        public InteriorItemColor? GetById(int id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<InteriorItemColor?> GetByCategoryId(int id, ColorType? type, string? name)
        {
            var list = _repository.GetByCategoryId(id) ?? throw new Exception("This object is not existed!");

            return Filter(list, type, name);
        }
        public InteriorItemColor? CreateInteriorItemColor(InteriorItemColorRequest request)
        {
            var iic = new InteriorItemColor
            {
                Name = request.Name,
                EnglishName = request.EnglishName,
                Type = request.Type,
                PrimaryColor = request.PrimaryColor,
                SecondaryColor = request.SecondaryColor,
            };

            var iicCreated = _repository.Save(iic);
            return iicCreated;
        }
        public void UpdateInteriorItemColor(int id, InteriorItemColorRequest request)
        {
            var iic = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            iic.Name = request.Name;
            iic.EnglishName = request.EnglishName;
            iic.Type = request.Type;
            iic.PrimaryColor = request.PrimaryColor;
            iic.SecondaryColor = request.SecondaryColor;

           _repository.Update(iic);
        }
        public void DeleteInteriorItemColor(int id)
        {
            var iic = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            //interior item

            _repository.DeleteById(id);
        }
    }
}
