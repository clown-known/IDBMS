using Azure.Core;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Request.BookingRequest;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Interfaces;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;

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
        public ProjectTask? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<ProjectTask?> GetByProjectId(Guid id)
        {
            return _repository.GetByProjectId(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<ProjectTask?> GetByPaymentStageId(Guid id)
        {
            return _repository.GetByPaymentStageId(id) ?? throw new Exception("This object is not existed!");
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
            var ctCreated = _repository.Save(ct);
            return ctCreated;
        }

        public void AssignTasksToStage(Guid paymentStageId, List<Guid> listTaskId)
        {
                foreach (var taskId in listTaskId)
                {
                    var task = _repository.GetById(taskId) ?? throw new Exception("This object is not existed!");

                    task.PaymentStageId = paymentStageId;

                    _repository.Update(task);
                }
        }
        public void StartTasksOfStage(Guid paymentStageId)
        {
            var listTask = _repository.GetByPaymentStageId(paymentStageId);
            if (listTask.Any())
            {
                foreach (var task in listTask)
                {
                    if (task!= null && task.PaymentStageId == paymentStageId)
                    {
                        task.StartedDate = DateTime.Now;
                        _repository.Update(task);
                    }
                }
            }
        }

        public void CreateBookProjectTask(Guid projectId, Guid roomId, List<BookingTaskRequest> request)
        {
            foreach (var taskRequest in request)
            {
                var projectTask = new ProjectTask
                {
                    Id = Guid.NewGuid(),
                    Name = taskRequest.Name,
                    Description = taskRequest.Description,
                    Percentage = 0,
                    CalculationUnit = taskRequest.CalculationUnit,
                    PricePerUnit = 0,
                    UnitInContract = taskRequest.UnitInContract,
                    IsIncurred = false,
                    CreatedDate = DateTime.Now,
                    ProjectId = projectId,
                    InteriorItemId = taskRequest.InteriorItemId,
                    RoomId = roomId,
                    Status = ProjectTaskStatus.Pending,
                };

                _repository.Save(projectTask);
            }
        }
        public void UpdateProjectTask(Guid id, ProjectTaskRequest request)
        {
            var ct = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

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

            _repository.Update(ct);
        }
        public void UpdateProjectTaskStatus(Guid id, ProjectTaskStatus status)
        {
            var ct = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ct.Status = status;

            _repository.Update(ct);
        }
    }
}
