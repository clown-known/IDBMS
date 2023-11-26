using BusinessObject.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;

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
/*        public IEnumerable<Comment?> GetByConstructionTaskId(Guid ctId)
        {
            return _repository.GetByTaskId(ctId) ?? throw new Exception("This object is not existed!");
        }*/
/*        public IEnumerable<Comment?> GetByDecorProgressReportId(Guid dprId)
        {
            return _repository.GetByDecorProgressReportId(dprId) ?? throw new Exception("This object is not existed!");
        }*/
        public Comment? CreateComment(CommentRequest comment)
        {
            var cmt = new Comment
            {
                Id = Guid.NewGuid(),
                Content = comment.Content,
                ProjectTaskId = comment.ProjectTaskId,
                ProjectId = comment.ProjectId,
                UserId = comment.UserId,
                FileUrl = comment.FileUrl,
                CreatedTime = DateTime.Now,
                Status = comment.Status,
            };
            var cmtCreated = _repository.Save(cmt);
            return cmtCreated;
        }
        public void UpdateComment(Guid id, CommentRequest comment)
        {
            var cmt = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            cmt.Content = comment.Content;
            cmt.ProjectTaskId = comment.ProjectTaskId;
            cmt.ProjectId = comment.ProjectId;
            cmt.UserId = comment.UserId;
            cmt.FileUrl = comment.FileUrl;
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
