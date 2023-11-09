using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class TaskReportService
    {
        private readonly ITaskReportRepository _repository;

        public TaskReportService(ITaskReportRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TaskReport> GetAll()
        {
            return _repository.GetAll();
        }
    }

}
