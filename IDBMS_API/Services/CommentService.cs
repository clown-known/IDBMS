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
            var cmtCreated = _repository.Save(cmt);
            return cmtCreated;
        }
        public void UpdateComment(Guid id, CommentRequest comment)
        {
            var cmt = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            cmt.Content = comment.Content;
            cmt.ConstructionTaskId = comment.ConstructionTaskId;
            cmt.DecorProgressReportId = comment.ConstructionTaskId;
            cmt.CreatedDate = comment.CreatedDate;
            cmt.UserId = comment.UserId;
            cmt.FileUrl = comment.FileUrl;
            cmt.CreatedTime = comment.CreatedTime;
            cmt.LastModifiedTime = comment.LastModifiedTime;
            cmt.Status = comment.Status;
            
            _repository.Update(cmt);
        }
        public void UpdateCommentStatus(Guid id, int status)
        {
            var cmt = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            bool isValueDefined = Enum.IsDefined(typeof(CommentStatus), status);
            if (isValueDefined)
            {
                cmt.Status = (CommentStatus)status;
                _repository.Update(cmt);
            }
            else throw new Exception("The input is invalid!");
        }
    }
}
