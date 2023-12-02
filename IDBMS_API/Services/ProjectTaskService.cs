using Azure.Core;
using IDBMS_API.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Interfaces;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Services
{
    public class ProjectTaskService
    {
        private readonly IProjectTaskRepository _taskRepo;
        private readonly IInteriorItemRepository _itemRepo;
        public ProjectTaskService(IProjectTaskRepository taskRepo, IInteriorItemRepository itemRepo)
        {
            _taskRepo = taskRepo;
            _itemRepo = itemRepo;
        }
        public IEnumerable<ProjectTask> GetAll()
        {
            return _taskRepo.GetAll();
        }
        public ProjectTask? GetById(Guid id)
        {
            return _taskRepo.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<ProjectTask?> GetByProjectId(Guid id)
        {
            return _taskRepo.GetByProjectId(id);
        }

        public IEnumerable<ProjectTask?> GetByRoomId(Guid id)
        {
            return _taskRepo.GetByRoomId(id);
        }

        public IEnumerable<ProjectTask?> GetByPaymentStageId(Guid id)
        {
            return _taskRepo.GetByPaymentStageId(id);
        }

        public IEnumerable<ProjectTask?> GetSuggestionTasksByProjectId(Guid id)
        {
            return _taskRepo.GetSuggestionTasksByProjectId(id);
        }        
        
        public IEnumerable<ProjectTask?> GetSuggestionTasksByRoomId(Guid id)
        {
            return _taskRepo.GetSuggestionTasksByRoomId(id);
        }

        public ProjectTask? CreateProjectTask(ProjectTaskRequest request)
        {
            var ct = new ProjectTask
            {
                Id = Guid.NewGuid(),
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                Percentage = request.Percentage,
                CalculationUnit = request.CalculationUnit,
                PricePerUnit = request.PricePerUnit,
                UnitInContract = request.UnitInContract,
                UnitUsed = request.UnitUsed,
                IsIncurred = request.IsIncurred,
                StartedDate = request.StartedDate,
                EndDate = request.EndDate,
                NoDate = request.NoDate,
                CreatedDate = DateTime.Now,
                ProjectId = request.ProjectId,
                PaymentStageId = request.PaymentStageId,
                InteriorItemId = request.InteriorItemId,
                RoomId = request.RoomId,
                Status = request.Status,
            };
            var ctCreated = _taskRepo.Save(ct);
            return ctCreated;
        }

        public void AssignTasksToStage(Guid paymentStageId, List<Guid> listTaskId)
        {
                foreach (var taskId in listTaskId)
                {
                    var task = _taskRepo.GetById(taskId) ?? throw new Exception("This object is not existed!");

                    task.PaymentStageId = paymentStageId;

                    _taskRepo.Update(task);
                }
        }
        public void StartTasksOfStage(Guid paymentStageId)
        {
            var listTask = _taskRepo.GetByPaymentStageId(paymentStageId);
            if (listTask.Any())
            {
                foreach (var task in listTask)
                {
                    if (task!= null && task.PaymentStageId == paymentStageId)
                    {
                        task.StartedDate = DateTime.Now;
                        _taskRepo.Update(task);
                    }
                }
            }
        }

        public void UpdateProjectTask(Guid id, ProjectTaskRequest request)
        {
            var ct = _taskRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            ct.Code = request.Code;
            ct.Name = request.Name;
            ct.Description = request.Description;
            ct.Percentage = request.Percentage;
            ct.CalculationUnit = request.CalculationUnit;
            ct.PricePerUnit = request.PricePerUnit;
            ct.UnitInContract = request.UnitInContract;
            ct.UnitUsed = request.UnitUsed;
            ct.IsIncurred = request.IsIncurred;
            ct.UpdatedDate= DateTime.Now;
            ct.EndDate = request.EndDate;
            ct.NoDate = request.NoDate;
            ct.ProjectId = request.ProjectId;
            ct.PaymentStageId = request.PaymentStageId;
            ct.InteriorItemId = request.InteriorItemId;
            ct.RoomId = request.RoomId;
            ct.Status = request.Status;

            _taskRepo.Update(ct);
        }
        public void UpdateProjectTaskStatus(Guid id, ProjectTaskStatus status)
        {
            var ct = _taskRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            ct.Status = status;

            _taskRepo.Update(ct);
        }

        public ProjectTask CreateProjectTaskWithCustomItem(CustomItemSuggestionRequest request)
        {
            var ct = new ProjectTask
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Percentage = request.Percentage,
                CalculationUnit = request.CalculationUnit,
                PricePerUnit = request.PricePerUnit,
                UnitInContract = request.UnitInContract,
                UnitUsed = request.UnitUsed,
                IsIncurred = request.IsIncurred,
                StartedDate = request.StartedDate,
                EndDate = request.EndDate,
                NoDate = request.NoDate,
                CreatedDate = DateTime.Now,
                ProjectId = request.ProjectId,
                PaymentStageId = request.PaymentStageId,
                RoomId = request.RoomId,
                Status = request.Status,
            };

            InteriorItemService itemService = new InteriorItemService(_itemRepo);

            var item = itemService.CreateInteriorItem(request.itemRequest);

            if (item.Result != null)
            {
                ct.InteriorItemId = item.Result.Id;
            }

            var ctCreated = _taskRepo.Save(ct);
            return ctCreated;
        }
    }
}
