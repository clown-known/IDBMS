using BusinessObject.Enums;
using BusinessObject.Models;
using IDBMS_API.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class ItemInTaskService
    {
        private readonly IItemInTaskRepository _itemInTaskRepo;
        private readonly IInteriorItemRepository _itemRepo;

        public ItemInTaskService(IItemInTaskRepository itemInTaskRepo, IInteriorItemRepository itemRepo)
        {
            _itemInTaskRepo = itemInTaskRepo;
            _itemRepo = itemRepo;
        }

        private IEnumerable<ItemInTask> Filter(IEnumerable<ItemInTask> list,
            string? itemCodeOrName, int? itemCategoryId, ProjectTaskStatus? taskStatus)
        {
            IEnumerable<ItemInTask> filteredList = list;

            if (itemCodeOrName != null)
            {
                filteredList = filteredList.Where(item =>
                            (item.InteriorItem.Code != null && item.InteriorItem.Code.Unidecode().IndexOf(itemCodeOrName.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (item.InteriorItem.Name != null && item.InteriorItem.Name.Unidecode().IndexOf(itemCodeOrName.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

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
            return _itemInTaskRepo.GetAll();
        }

        public ItemInTask? GetById(Guid id)
        {
            return _itemInTaskRepo.GetById(id) ?? throw new Exception("This object is not existed!");
        }

        public IEnumerable<ItemInTask> GetByProjectId(Guid id, string? itemCodeOrName, int? itemCategoryId, ProjectTaskStatus? taskStatus)
        {
            var list =  _itemInTaskRepo.GetByProjectId(id);

            return Filter(list, itemCodeOrName, itemCategoryId, taskStatus);
        }

        public IEnumerable<ItemInTask> GetByRoomId(Guid id)
        {
            return _itemInTaskRepo.GetByRoomId(id);
        }

        public IEnumerable<ItemInTask> GetByTaskId(Guid id, string? itemCodeOrName, int? itemCategoryId, ProjectTaskStatus? taskStatus)
        {
            var list = _itemInTaskRepo.GetByTaskId(id);

            return Filter(list, itemCodeOrName, itemCategoryId, taskStatus);
        }

        public async Task<ItemInTask?> CreateItemInTask(ItemInTaskRequest request)
        {
            var itemInTask = new ItemInTask
            {
                Id = Guid.NewGuid(),
                Quantity= request.Quantity,
                ProjectId= request.ProjectId,
                ProjectTaskId= request.ProjectTaskId,
            };

            if (request.InteriorItemId.HasValue)
            {
                InteriorItemService itemService = new(_itemRepo, null);
                var item = itemService.GetById(request.InteriorItemId.Value);

                itemInTask.InteriorItemId = request.InteriorItemId.Value;
                itemInTask.EstimatePrice = item.EstimatePrice;
            }
            else
            {
                if (request.InteriorItem == null) throw new Exception("New item null!");

                InteriorItemService itemService = new(_itemRepo, null);

                var newItem = await itemService.CreateInteriorItem(request.InteriorItem);

                if (newItem != null)
                {
                    itemInTask.InteriorItemId = newItem.Id;
                    itemInTask.EstimatePrice = newItem.EstimatePrice;
                }
                else
                {
                    throw new Exception("Create new item fail!");
                }
            }

            var itemInTaskCreated = _itemInTaskRepo.Save(itemInTask);
            return itemInTaskCreated;
        }

        public async Task CreateItemsByTaskId(List<ItemInTaskRequest> request)
        {
            foreach (var itemInTask in request)
            {
                var newCreate = new ItemInTask
                {
                    Id = Guid.NewGuid(),
                    Quantity = itemInTask.Quantity,
                    ProjectId = itemInTask.ProjectId,
                    ProjectTaskId = itemInTask.ProjectTaskId,
                };

                if (itemInTask.InteriorItemId.HasValue)
                {
                    InteriorItemService itemService = new(_itemRepo, null);
                    var item = itemService.GetById(itemInTask.InteriorItemId.Value);

                    newCreate.InteriorItemId = itemInTask.InteriorItemId.Value;
                    newCreate.EstimatePrice = item.EstimatePrice;
                } 
                else
                {
                    if (itemInTask.InteriorItem == null) throw new Exception("New item null!");

                    InteriorItemService itemService = new(_itemRepo, null);

                    var newItem = await itemService.CreateInteriorItem(itemInTask.InteriorItem);

                    if (newItem != null)
                    {
                        newCreate.InteriorItemId = newItem.Id;
                        newCreate.EstimatePrice = newItem.EstimatePrice;
                    }
                    else
                    {
                        throw new Exception("Create new item fail!");
                    }
                }

                _itemInTaskRepo.Save(newCreate);
            }
        }

        public async Task UpdateItemInTask(Guid id, ItemInTaskRequest request)
        {
            var itemInTask = _itemInTaskRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            itemInTask.Quantity = request.Quantity;
            itemInTask.ProjectTaskId = request.ProjectTaskId;
            itemInTask.ProjectId = request.ProjectId;

            if (request.InteriorItemId.HasValue)
            {
                InteriorItemService itemService = new(_itemRepo, null);
                var item = itemService.GetById(request.InteriorItemId.Value);

                itemInTask.InteriorItemId = request.InteriorItemId.Value;
                itemInTask.EstimatePrice = item.EstimatePrice;
            }
            else
            {
                if (request.InteriorItem == null) throw new Exception("New item null!");

                InteriorItemService itemService = new(_itemRepo, null);

                var newItem = await itemService.CreateInteriorItem(request.InteriorItem);

                if (newItem != null)
                {
                    itemInTask.InteriorItemId = newItem.Id;
                    itemInTask.EstimatePrice = newItem.EstimatePrice;
                }
                else
                {
                    throw new Exception("Create new item fail!");
                }
            }

            _itemInTaskRepo.Update(itemInTask);
        }

        public void UpdateItemInTaskQuantity(Guid id, int quantity)
        {
            var itemInTask = _itemInTaskRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            itemInTask.Quantity = quantity;

            _itemInTaskRepo.Update(itemInTask);
        }

        public void DeleteItemInTask(Guid id)
        {
            var itemInTask = _itemInTaskRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            _itemInTaskRepo.DeleteById(id);
        }
    }
}
