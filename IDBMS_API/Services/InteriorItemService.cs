using BusinessObject.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Newtonsoft.Json.Linq;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class InteriorItemService
    {
        private readonly IInteriorItemRepository _repository;
        public InteriorItemService(IInteriorItemRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<InteriorItem> GetAll()
        {
            return _repository.GetAll();
        }
        public InteriorItem? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public InteriorItem? CreateInteriorItem(InteriorItemRequest request)
        {
            var ii = new InteriorItem
            {
                Id = Guid.NewGuid(),
                Code = request.Code,
                Name = request.Name,
                Length = request.Length,
                Width = request.Width,
                Height = request.Height,
                CalculationUnit = request.CalculationUnit,
                Material = request.Material,
                Description = request.Description,
                Origin = request.Origin,
                EstimatePrice = request.EstimatePrice,
                LaborCost = request.LaborCost,
                InteriorItemColorId = request.InteriorItemColorId,
                InteriorItemCategoryId = request.InteriorItemCategoryId,
                Status = request.Status,
                ParentItemId = request.ParentItemId,
            };

            var iiCreated = _repository.Save(ii);
            return iiCreated;
        }
        public void UpdateInteriorItem(Guid id, InteriorItemRequest request)
        {
            var ii = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ii.Code = request.Code;
            ii.Name = request.Name;
            ii.Length = request.Length;
            ii.Width = request.Width;
            ii.Height = request.Height;
            ii.CalculationUnit = request.CalculationUnit;
            ii.Material = request.Material;
            ii.Description = request.Description;
            ii.Origin = request.Origin;
            ii.EstimatePrice = request.EstimatePrice;
            ii.LaborCost = request.LaborCost;
            ii.InteriorItemColorId = request.InteriorItemColorId;
            ii.InteriorItemCategoryId = request.InteriorItemCategoryId;
            ii.Status = request.Status;
            ii.ParentItemId = request.ParentItemId;

            _repository.Update(ii);
        }

        public void UpdateInteriorItemStatus(Guid id, InteriorItemStatus status)
        {
            var ii = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ii.Status = status;

            _repository.Update(ii);
        }

        public void DeleteInteriorItem(Guid id)
        {
            var ii = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ii.Status = InteriorItemStatus.Deleted;

            _repository.Update(ii);
        }
    }
}
