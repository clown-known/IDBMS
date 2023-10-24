using BusinessObject.DTOs.Request.CreateRequests;
using BusinessObject.Models;
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
        public InteriorItem CreateInteriorItem(InteriorItemRequest request)
        {
            var ii = new InteriorItem
            {
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
            return ii;
        }
        public void UpdateInteriorItem(InteriorItemRequest request)
        {
            var ii = new InteriorItem
            {
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
            _interiorItemRepository.Update(ii);
        }
    }
}
