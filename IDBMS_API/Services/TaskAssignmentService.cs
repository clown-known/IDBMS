using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class TaskAssignmentService
    {
        private readonly ITaskAssignmentRepository _repository;

        public TaskAssignmentService(ITaskAssignmentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TaskAssignment> GetAll()
        {
            return _repository.GetAll();
        }
        public TaskAssignment? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<TaskAssignment?> GetByProjectId(Guid id)
        {
            return _repository.GetByProjectId(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<TaskAssignment?> GetByUserId(Guid id)
        {
            return _repository.GetByUserId(id) ?? throw new Exception("This object is not existed!");
        }
        public TaskAssignment? CreateTaskAssignment(TaskAssignmentRequest request)
        {
            var ta = new TaskAssignment
            {
                Id = Guid.NewGuid(),
                ProjectParticipationId = request.ProjectParticipationId,
                ProjectTaskId = request.ProjectTaskId,
                CreatedDate = DateTime.Now,
            };

            var taCreated = _repository.Save(ta);
            return taCreated;        
        }

        public void DeleteTaskAssignment(Guid id)
        {
            var ta = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            _repository.DeleteById(id);
        }
    }

}
