using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using BusinessObject.Enums;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class TaskAssignmentService
    {
        private readonly ITaskAssignmentRepository _repository;

        public TaskAssignmentService(ITaskAssignmentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TaskAssignment> Filter(IEnumerable<TaskAssignment> list,
           string? name)
        {
            IEnumerable<TaskAssignment> filteredList = list;

            if (name != null)
            {
                filteredList = filteredList.Where(item => (item.ProjectParticipation.User.Name != null && item.ProjectParticipation.User.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public IEnumerable<TaskAssignment> GetAll(string? name)
        {
            var list =_repository.GetAll();

            return Filter(list, name);
        }
        public TaskAssignment? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<TaskAssignment?> GetByProjectId(Guid id, string? name)
        {
            var list = _repository.GetByProjectId(id) ?? throw new Exception("This object is not existed!");

            return Filter(list, name);
        }
        public IEnumerable<TaskAssignment?> GetByUserId(Guid id, string? name)
        {
            var list = _repository.GetByUserId(id) ?? throw new Exception("This object is not existed!");

            return Filter(list, name);
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
