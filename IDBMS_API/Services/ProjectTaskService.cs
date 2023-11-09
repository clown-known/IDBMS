using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class ProjectTaskService
    {
        private readonly IProjectTaskRepository _repository;
        public ProjectTaskService(IProjectTaskRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<ProjectTask> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
