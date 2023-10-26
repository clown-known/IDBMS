using Azure.Core;
using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class ConstructionTaskCategoryService
    {
        private readonly IConstructionTaskCategoryRepository _constructionTaskCategoryRepository;
        public ConstructionTaskCategoryService(IConstructionTaskCategoryRepository constructionTaskCategoryRepository)
        {
            _constructionTaskCategoryRepository = constructionTaskCategoryRepository;
        }
        public IEnumerable<ConstructionTaskCategory> GetAll()
        {
            return _constructionTaskCategoryRepository.GetAll();
        }
        public ConstructionTaskCategory? GetById(int id)
        {
            return _constructionTaskCategoryRepository.GetById(id);
        }
        public ConstructionTaskCategory? CreateConstructionTaskCategory (ConstructionTaskCategoryRequest request)
        {
            var ctc = new ConstructionTaskCategory
            {
                Name = request.Name,
                Description = request.Description,
                IconImageUrl = request.IconImageUrl,
                IsDeleted = request.IsDeleted,
            };
            var ctcCreated =_constructionTaskCategoryRepository.Save(ctc);
            return ctcCreated; 
        }
        public void UpdateConstructionTaskCategory(int id, ConstructionTaskCategoryRequest request)
        {
            var ctc = _constructionTaskCategoryRepository.GetById(id) ?? throw new Exception("This Construction Task Category not existed");
            ctc.Name = request.Name;
            ctc.Description = request.Description;
            ctc.IconImageUrl = request.IconImageUrl;
            ctc.IsDeleted = request.IsDeleted;
            _constructionTaskCategoryRepository.Update(ctc);
        }
        public void UpdateConstructionTaskCategoryStatus(int id, bool isDeleted)
        {
            var ctc = _constructionTaskCategoryRepository.GetById(id) ?? throw new Exception("This Construction Task Category not existed");
            ctc.IsDeleted = isDeleted;
            _constructionTaskCategoryRepository.Update(ctc);
        }
    }
}
