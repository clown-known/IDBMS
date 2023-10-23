using BusinessObject.DTOs.Request.CreateRequests;
using BusinessObject.Models;
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
        public void UpdateConstructionTaskCategory(ConstructionTaskCategoryRequest request)
        {
            var ctc = new ConstructionTaskCategory
            {
                Name = request.Name,
                Description = request.Description,
                IconImageUrl = request.IconImageUrl,
                IsDeleted = request.IsDeleted,
            };
            _constructionTaskCategoryRepository.Update(ctc);
        }
    }
}
