using BusinessObject.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class CommentService
    {
        private readonly ICommentRepository repository;
        public CommentService(ICommentRepository repository)
        {
            this.repository = repository;
        }   
        public IEnumerable<Comment> GetAll()
        {
            return repository.GetAll();
        }
        public Comment? GetById(Guid id)
        {
            return repository.GetById(id);
        }
        public Comment? CreateComment(CommentRequest comment)
        {
            var cmt = new Comment
            {
                Id = Guid.NewGuid(),
                Content = comment.Content,
                ConstructionTaskId = comment.ConstructionTaskId,
                DecorProgressReportId = comment.ConstructionTaskId,
                CreatedDate = comment.CreatedDate,
                UserId = comment.UserId,
                FileUrl = comment.FileUrl,
                CreatedTime = comment.CreatedTime,
                LastModifiedTime = comment.LastModifiedTime,
                Status = comment.Status,
            };
            var cmtCreated = repository.Save(cmt);
            return cmtCreated;
        }
        public void UpdateComment(Guid id, CommentRequest comment)
        {
            var cmt = repository.GetById(id) ?? throw new Exception("This object is not existed!");

            cmt.Content = comment.Content;
            cmt.ConstructionTaskId = comment.ConstructionTaskId;
            cmt.DecorProgressReportId = comment.ConstructionTaskId;
            cmt.CreatedDate = comment.CreatedDate;
            cmt.UserId = comment.UserId;
            cmt.FileUrl = comment.FileUrl;
            cmt.CreatedTime = comment.CreatedTime;
            cmt.LastModifiedTime = comment.LastModifiedTime;
            cmt.Status = comment.Status;
            
            repository.Update(cmt);
        }
        public void UpdateCommentStatus(Guid id, int status)
        {
            var cmt = repository.GetById(id) ?? throw new Exception("This object is not existed!");
            bool isValueDefined = Enum.IsDefined(typeof(CommentStatus), status);
            if (isValueDefined)
            {
                cmt.Status = (CommentStatus)status;
                repository.Update(cmt);
            }
            else throw new Exception("The input is invalid!");
        }
    }
}
