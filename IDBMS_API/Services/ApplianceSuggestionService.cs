using BusinessObject.DTOs.Request.CreateRequests;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class ApplianceSuggestionService
    {
        private readonly IApplianceSuggestionRepository _repository;
        public ApplianceSuggestionService(IApplianceSuggestionRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<ApplianceSuggestion> GetAll()
        {
            return _repository.GetAll();
        }
        public ApplianceSuggestion? GetById(Guid id)
        {
            return _repository.GetById(id);
        }
        public ApplianceSuggestion? CreateApplianceSuggestion(ApplianceSuggestionRequest request)
        {
            var applianceSuggestion = new ApplianceSuggestion
            {
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                InteriorItemId = request.InteriorItemId,
                RoomId = request.RoomId,
            };
            var asCreated = _repository.Save(applianceSuggestion);
            return asCreated;
        }
        public void UpdateApplianceSuggestion(ApplianceSuggestionRequest request)
        {
            var applianceSuggestion = new ApplianceSuggestion
            {
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                InteriorItemId = request.InteriorItemId,
                RoomId = request.RoomId,
            };
            _repository.Update(applianceSuggestion);
        }
    }
}
