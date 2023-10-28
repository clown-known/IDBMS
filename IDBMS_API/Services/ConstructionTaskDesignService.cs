using BusinessObject.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class ConstructionTaskDesignService
    {
        private readonly IConstructionTaskDesignRepository _repository;
        public ConstructionTaskDesignService(IConstructionTaskDesignRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<ConstructionTaskDesign> GetAll()
        {
            return _repository.GetAll();
        }
        public ConstructionTaskDesign? GetById(int id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
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
                IsDeleted = false,
                InteriorItemCategoryId = request.InteriorItemCategoryId,
                ConstructionTaskCategoryId = request.ConstructionTaskCategoryId,
            };

            var ctdCreated = _repository.Save(ctd);
            return ctdCreated;
        }
        public void UpdateConstructionTaskDesign(int id, ConstructionTaskDesignRequest request)
        {
            var ctd = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            ctd.Code = request.Code;
            ctd.Name = request.Name;
            ctd.Description = request.Description;
            ctd.CalculationUnit = request.CalculationUnit; 
            ctd.EstimatePricePerUnit = request.EstimatePricePerUnit;
            ctd.InteriorItemCategoryId = request.InteriorItemCategoryId;
            ctd.ConstructionTaskCategoryId = request.ConstructionTaskCategoryId;
            
            _repository.Update(ctd);
        }
        public void DeleteConstructionTaskDesign(int id)
        {
            var ctd = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ctd.IsDeleted= true;

            _repository.Update(ctd);  
        }
    }
}
