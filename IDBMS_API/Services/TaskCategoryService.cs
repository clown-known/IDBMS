using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class TaskCategoryService
    {
        private readonly ITaskCategoryRepository _repository;

        public TaskCategoryService(ITaskCategoryRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TaskCategory> GetAll()
        {
            return _repository.GetAll();
        }
    }

}
