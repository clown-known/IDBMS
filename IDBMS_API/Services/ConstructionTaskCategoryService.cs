using Azure.Core;
using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class ConstructionTaskCategoryService
    {
        private readonly IConstructionTaskCategoryRepository _repository;
        public ConstructionTaskCategoryService(IConstructionTaskCategoryRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<ConstructionTaskCategory> GetAll()
        {
            return _repository.GetAll();
        }
        public ConstructionTaskCategory? GetById(int id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public ConstructionTaskCategory? CreateConstructionTaskCategory (ConstructionTaskCategoryRequest request)
        {
            var ctc = new ConstructionTaskCategory
            {
                Name = request.Name,
                Description = request.Description,
                IconImageUrl = request.IconImageUrl,
                IsDeleted = false,
            };
            var ctcCreated =_repository.Save(ctc);
            return ctcCreated; 
        }
        public void UpdateConstructionTaskCategory(int id, ConstructionTaskCategoryRequest request)
        {
            var ctc = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ctc.Name = request.Name;
            ctc.Description = request.Description;
            ctc.IconImageUrl = request.IconImageUrl;

            _repository.Update(ctc);
        }
        public void DeleteConstructionTaskCategory(int id)
        {
            var ctc = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ctc.IsDeleted = true;
            //cons task design

            _repository.Update(ctc);
        }
    }
}
