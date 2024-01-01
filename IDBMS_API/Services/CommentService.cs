using IDBMS_API.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _repository;
        public CommentService(ICommentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Comment> Filter(IEnumerable<Comment> list, CommentType? type, CommentStatus? status, string? content)
        {
            IEnumerable<Comment> filteredList = list;

            if (type != null)
            {
                filteredList = filteredList.Where(comment => comment.Type == type);
            }

            if (status != null)
            {
                filteredList = filteredList.Where(comment => comment.Status == status);
            }

            if (content != null)
            {
                filteredList = filteredList
                    .Where(comment =>
                        comment.Content != null &&
                        comment.Content.Unidecode().IndexOf(content.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return filteredList;
        }


        public IEnumerable<Comment> GetAll()
        {
            return _repository.GetAll();
        }

        public Comment? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This comment id is not existed!");
        }

        public IEnumerable<Comment> GetByProjectTaskId(Guid id, CommentType? type, CommentStatus? status, string? content)
        {
            var list = _repository.GetByTaskId(id);

            return Filter(list, type, status, content);
        }

        public IEnumerable<Comment> GetByProjectId(Guid id, CommentType? type, CommentStatus? status, string? content)
        {
            var list = _repository.GetByProjectId(id);

            return Filter(list, type, status, content);
        }
        public async Task<Comment?> CreateComment(CommentRequest request)
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
        public async void UpdateComment(Guid id, CommentRequest request)
        {
            var cmt = _repository.GetById(id) ?? throw new Exception("This comment id is not existed!");

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
            var cmt = _repository.GetById(id) ?? throw new Exception("This comment id is not existed!");

            cmt.Status = status;

            _repository.Update(cmt);
        }

        public void DeleteComment(Guid id)
        {
            var cmt = _repository.GetById(id) ?? throw new Exception("This comment id is not existed!");

            cmt.IsDeleted = true;
            cmt.LastModifiedTime = DateTime.Now;

            _repository.Update(cmt);
        }
    }
}
