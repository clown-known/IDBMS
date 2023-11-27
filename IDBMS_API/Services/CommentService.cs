using IDBMS_API.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;
using BLL.Services;

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
        public async Task<Comment?> CreateComment(CommentRequest comment)
        {
            FirebaseService s = new FirebaseService();
            string link = "";
            if (comment.File != null) { 
                link = await s.UploadCommentFile(comment.File,comment.ProjectId,comment.ProjectTaskId); 
            }
            var cmt = new Comment
            {
                Id = Guid.NewGuid(),
                Content = comment.Content,
                ProjectTaskId = comment.ProjectTaskId,
                ProjectId = comment.ProjectId,
                UserId = comment.UserId,
                FileUrl = link,
                CreatedTime = DateTime.Now,
                Status = comment.Status,
            };
            var cmtCreated = _repository.Save(cmt);
            return cmtCreated;
        }
        public async void UpdateComment(Guid id, CommentRequest comment)
        {
            var cmt = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            FirebaseService s = new FirebaseService();
            string link = "";
            if (comment.File != null)
            {
                link = await s.UploadCommentFile(comment.File, comment.ProjectId, comment.ProjectTaskId);
            }
            cmt.Content = comment.Content;
            cmt.ProjectTaskId = comment.ProjectTaskId;
            cmt.ProjectId = comment.ProjectId;
            cmt.UserId = comment.UserId;
            cmt.FileUrl = link;
            cmt.LastModifiedTime = DateTime.Now;
            cmt.Status = CommentStatus.Edited;

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
