using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
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
        public void UpdateConstructionTaskDesign(int id, ConstructionTaskDesignRequest request)
        {
            var ctd = repository.GetById(id) ?? throw new Exception("This object is not existed!");
            ctd.Code = request.Code;
            ctd.Name = request.Name;
            ctd.Description = request.Description;
            ctd.CalculationUnit = request.CalculationUnit; 
            ctd.EstimatePricePerUnit = request.EstimatePricePerUnit;
            ctd.InteriorItemCategoryId = request.InteriorItemCategoryId;
            ctd.ConstructionTaskCategoryId = request.ConstructionTaskCategoryId;
            
            repository.Update(ctd);
        }
        public void DeleteConstructionTaskDesign(int id)
        {
            repository.DeleteById(id);  
        }
    }
}
