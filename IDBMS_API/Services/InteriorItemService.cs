using BusinessObject.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Newtonsoft.Json.Linq;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class InteriorItemService
    {
        private readonly IInteriorItemRepository _interiorItemRepository;
        public InteriorItemService(IInteriorItemRepository interiorItemRepository)
        {
            _interiorItemRepository = interiorItemRepository;
        }
        public IEnumerable<InteriorItem> GetAll()
        {
            return _interiorItemRepository.GetAll();
        }
        public InteriorItem? GetById(Guid id)
        {
            return _interiorItemRepository.GetById(id);
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
            var iiCreated = _interiorItemRepository.Save(ii);
            return iiCreated;
        }
        public void UpdateInteriorItem(Guid id, InteriorItemRequest request)
        {
            var ii = _interiorItemRepository.GetById(id) ?? throw new Exception("This object is not existed!");

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
            
            _interiorItemRepository.Update(ii);
        }
        public void UpdateInteriorItemStatus(Guid id, int status)
        {
            var ii = _interiorItemRepository.GetById(id) ?? throw new Exception("This object is not existed!");
            bool isValueDefined = Enum.IsDefined(typeof(InteriorItemStatus), status);
            if (isValueDefined)
            {
                ii.Status = (InteriorItemStatus)status;
                _interiorItemRepository.Update(ii);
            }
            else throw new Exception("The input is invalid!");
        }
    }
}
