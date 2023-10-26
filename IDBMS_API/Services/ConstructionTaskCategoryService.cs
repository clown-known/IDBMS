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
            var ctcCheck = _constructionTaskCategoryRepository.GetById(id) ?? throw new Exception("This Construction Task Category not existed");
            var ctc = new ConstructionTaskCategory
            {
                Name = request.Name,
                Description = request.Description,
                IconImageUrl = request.IconImageUrl,
                IsDeleted = request.IsDeleted,
            };
            _constructionTaskCategoryRepository.Update(ctc);
        }
        public void UpdateConstructionTaskCategoryStatus(int id, bool isDeleted)
        {
            var request = _constructionTaskCategoryRepository.GetById(id) ?? throw new Exception("This Construction Task Category not existed");
            var ctc = new ConstructionTaskCategory
            {
                Name = request.Name,
                Description = request.Description,
                IconImageUrl = request.IconImageUrl,
                IsDeleted = isDeleted,
            };
            _constructionTaskCategoryRepository.Update(ctc);
        }
    }
}
