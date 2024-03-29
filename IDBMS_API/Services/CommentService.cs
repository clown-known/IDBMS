﻿using IDBMS_API.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using UnidecodeSharpFork;
using IDBMS_API.Supporters.TimeHelper;

namespace IDBMS_API.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _repository;
        public CommentService(ICommentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Comment> Filter(IEnumerable<Comment> list, CommentStatus? status, string? content)
        {
            IEnumerable<Comment> filteredList = list;

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

        public IEnumerable<Comment> GetByProjectTaskId(Guid id, CommentStatus? status, string? content)
        {
            var list = _repository.GetByTaskId(id);

            return Filter(list, status, content);
        }

        public IEnumerable<Comment> GetByProjectId(Guid id, CommentStatus? status, string? content)
        {
            var list = _repository.GetByProjectId(id);

            return Filter(list, status, content);
        }
        public Comment? CreateComment(CommentRequest request)
        {

            var cmt = new Comment
            {
                Id = Guid.NewGuid(),
                ProjectTaskId = request.ProjectTaskId,
                ProjectId = request.ProjectId,
                UserId = request.UserId,
                CreatedTime = TimeHelper.GetTime(DateTime.Now),
                Status = CommentStatus.Sent,
                Content = request.Content,
                IsDeleted = false,
                ReplyCommentId = request.ReplyCommentId,
            };

            var cmtCreated = _repository.Save(cmt);
            return cmtCreated;
        }
        public void UpdateComment(Guid id, CommentRequest request)
        {
            var cmt = _repository.GetById(id) ?? throw new Exception("This comment id is not existed!");

            cmt.ProjectTaskId = request.ProjectTaskId;
            cmt.ProjectId = request.ProjectId;
            cmt.UserId = request.UserId;
            cmt.LastModifiedTime = TimeHelper.GetTime(DateTime.Now);
            cmt.Status = CommentStatus.Edited;
            cmt.Content = request.Content;

            _repository.Update(cmt);
        }

        public void UpdateCommentStatus(Guid id, CommentStatus status)
        {
            var cmt = _repository.GetById(id) ?? throw new Exception("This comment id is not existed!");

            cmt.Status = status;
            cmt.LastModifiedTime = TimeHelper.GetTime(DateTime.Now);

            _repository.Update(cmt);
        }

        public void DeleteComment(Guid id)
        {
            var cmt = _repository.GetById(id) ?? throw new Exception("This comment id is not existed!");

            cmt.IsDeleted = true;
            cmt.LastModifiedTime = TimeHelper.GetTime(DateTime.Now);

            _repository.Update(cmt);
        }
    }
}
