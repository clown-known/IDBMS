using IDBMS_API.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _repository;
        public CommentService(ICommentRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<Comment> GetAll()
        {
            return _repository.GetAll();
        }
        public Comment? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<Comment?> GetByProjectTaskId(Guid ctId)
        {
            return _repository.GetByTaskId(ctId) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<Comment?> GetByProjectId(Guid dprId)
        {
            return _repository.GetByProjectId(dprId) ?? throw new Exception("This object is not existed!");
        }
        public async Task<Comment?> CreateComment([FromForm] CommentRequest request)
        {

            var cmt = new Comment
            {
                Id = Guid.NewGuid(),
                ProjectTaskId = request.ProjectTaskId,
                ProjectId = request.ProjectId,
                UserId = request.UserId,
                CreatedTime = DateTime.Now,
                Status = CommentStatus.Sent,
            };

            if (request.Type == CommentType.Text)
            {
                cmt.Type= CommentType.Text;
                cmt.Content = request.Content;
            }

            if (request.Type == CommentType.File)
            {
                FirebaseService s = new FirebaseService();
                string link = "";
                if (request.File != null)
                {
                    link = await s.UploadCommentFile(request.File, request.ProjectId, request.ProjectTaskId);
                }

                cmt.Type = CommentType.File;
                cmt.FileUrl= link;
            }

            if (request.Type == CommentType.ItemSuggestion)
            {
                cmt.Type = CommentType.ItemSuggestion;
                cmt.ItemId = request.ItemId;
            }

            var cmtCreated = _repository.Save(cmt);
            return cmtCreated;
        }
        public async void UpdateComment(Guid id, [FromForm] CommentRequest request)
        {
            var cmt = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            cmt.ProjectTaskId = request.ProjectTaskId;
            cmt.ProjectId = request.ProjectId;
            cmt.UserId = request.UserId;
            cmt.LastModifiedTime = DateTime.Now;
            cmt.Status = CommentStatus.Edited;

            if (request.Type == CommentType.Text)
            {
                cmt.Type = CommentType.Text;
                cmt.Content = request.Content;
            }

            if (request.Type == CommentType.File)
            {
                FirebaseService s = new FirebaseService();
                string link = "";
                if (request.File != null)
                {
                    link = await s.UploadCommentFile(request.File, request.ProjectId, request.ProjectTaskId);
                }

                cmt.Type = CommentType.File;
                cmt.FileUrl = link;
            }

            if (request.Type == CommentType.ItemSuggestion)
            {
                cmt.Type = CommentType.ItemSuggestion;
                cmt.ItemId = request.ItemId;
            }

            _repository.Update(cmt);
        }

        public void UpdateCommentStatus(Guid id, CommentStatus status)
        {
            var cmt = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            cmt.Status = status;

            _repository.Update(cmt);
        }

        public void DeleteComment(Guid id)
        {
            var cmt = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            cmt.IsDeleted = true;
            cmt.LastModifiedTime = DateTime.Now;

            _repository.Update(cmt);
        }
    }
}
