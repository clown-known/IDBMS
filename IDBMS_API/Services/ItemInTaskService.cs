using BusinessObject.Models;
using IDBMS_API.DTOs.Request;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class ItemInTaskService
    {
        private readonly IItemInTaskRepository _repository;

        public ItemInTaskService(IItemInTaskRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ItemInTask> GetAll()
        {
            return _repository.GetAll();
        }

        public ItemInTask? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }

        public IEnumerable<ItemInTask> GetByProjectId(Guid id)
        {
            return _repository.GetByProjectId(id);
        }

        public IEnumerable<ItemInTask> GetByRoomId(Guid id)
        {
            return _repository.GetByRoomId(id);
        }

        public IEnumerable<ItemInTask> GetByTaskId(Guid id)
        {
            return _repository.GetByTaskId(id);
        }

        public ItemInTask? CreateItemInTask(ItemInTaskRequest request)
        {
            var itemInTask = new ItemInTask
            {
                Id = Guid.NewGuid(),
                EstimatePrice = request.EstimatePrice,
                Quantity= request.Quantity,
                ProjectId= request.ProjectId,
                ProjectTaskId= request.ProjectTaskId,
                InteriorItemId= request.InteriorItemId,
            };
            var itemInTaskCreated = _repository.Save(itemInTask);
            return itemInTaskCreated;
        }

        public void UpdateItemInTask(Guid id, ItemInTaskRequest request)
        {
            var itemInTask = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            itemInTask.EstimatePrice = request.EstimatePrice;
            itemInTask.Quantity = request.Quantity;
            itemInTask.ProjectTaskId = request.ProjectTaskId;
            itemInTask.ProjectId = request.ProjectId;
            itemInTask.InteriorItemId = request.InteriorItemId;

            _repository.Update(itemInTask);
        }

        public void DeleteItemInTask(Guid id)
        {
            var itemInTask = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            _repository.DeleteById(id);
        }
    }
}
