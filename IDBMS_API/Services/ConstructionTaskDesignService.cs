using BusinessObject.DTOs.Request.CreateRequests;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class ConstructionTaskDesignService
    {
        private readonly IConstructionTaskDesignRepository repository;
        public ConstructionTaskDesignService(IConstructionTaskDesignRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<ConstructionTaskDesign> GetAll()
        {
            return repository.GetAll();
        }
        public ConstructionTaskDesign? GetById(int id)
        {
            return repository.GetById(id);
        }
        public ConstructionTaskDesign? CreateConstructionTaskDesign (ConstructionTaskDesignRequest request)
        {
            var ctd = new ConstructionTaskDesign
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                CalculationUnit = request.CalculationUnit,
                EstimatePricePerUnit = request.EstimatePricePerUnit,
                InteriorItemCategoryId = request.InteriorItemCategoryId,
                ConstructionTaskCategoryId = request.ConstructionTaskCategoryId,
            };
            var ctdCreated = repository.Save(ctd);
            return ctdCreated;
        }
        public void UpdateConstructionTaskDesign(ConstructionTaskDesignRequest request)
        {
            var ctd = new ConstructionTaskDesign
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                CalculationUnit = request.CalculationUnit,
                EstimatePricePerUnit = request.EstimatePricePerUnit,
                InteriorItemCategoryId = request.InteriorItemCategoryId,
                ConstructionTaskCategoryId = request.ConstructionTaskCategoryId,
            };
            repository.Update(ctd);
        }
        public void DeleteConstructionTaskDesign(int id)
        {
            repository.DeleteById(id);  
        }
    }
}
