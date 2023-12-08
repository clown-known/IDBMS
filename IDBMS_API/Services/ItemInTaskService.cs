using BusinessObject.Enums;
using BusinessObject.Models;
using IDBMS_API.DTOs.Request;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDBMS_API.Services
{
    public class ItemInTaskService
    {
        private readonly IItemInTaskRepository _repository;

        public ItemInTaskService(IItemInTaskRepository repository)
        {
            _repository = repository;
        }

        private IEnumerable<ItemInTask> Filter(IEnumerable<ItemInTask> list, int? itemCategoryId, ProjectTaskStatus? taskStatus)
        {
            IEnumerable<ItemInTask> filteredList = list;

            if (itemCategoryId != null)
            {
                filteredList = filteredList.Where(item => item.InteriorItem.InteriorItemCategoryId == itemCategoryId);
            }

            if (taskStatus != null)
            {
                filteredList = filteredList.Where(item => item.ProjectTask.Status == taskStatus);
            }

            return filteredList;
        }
        public IEnumerable<ItemInTask> GetAll()
        {
            return _repository.GetAll();
        }

        public ItemInTask? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }

        public IEnumerable<ItemInTask> GetByProjectId(Guid id, int? itemCategoryId, ProjectTaskStatus? taskStatus)
        {
            var list =  _repository.GetByProjectId(id);

            return Filter(list, itemCategoryId, taskStatus);
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
