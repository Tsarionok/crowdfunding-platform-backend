using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;

namespace BusinessLogicLayer.Service.Implementation
{
    public class CommentService : BaseService, ICommentService
    {
        public CommentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task Create(CommentDTO comment)
        {
            await _unitOfWork.Comments.Create(new Comment
            {
                Message = comment.Message,
                Project = _unitOfWork.Projects.ReadById(comment.Project.Id).Result,
                User = _unitOfWork.Users.ReadById(comment.User.Id).Result
            });
        }

        public async Task<CommentDTO> DeleteById(int id)
        {
            Comment deletedComment = _unitOfWork.Comments.DeleteById(id).Result;

            return await Task.Run(() => new CommentDTO
            {
                Id = deletedComment.Id,
                Message = deletedComment.Message,
                Project = new ProjectService(_unitOfWork).ReadById(deletedComment.Project.Id).Result,
                User = new UserService(_unitOfWork, null, null).ReadById(deletedComment.User.Id).Result
            });
        }

        public async Task<ICollection<CommentDTO>> ReadAll()
        {
            ICollection<CommentDTO> comments = new List<CommentDTO>();

            foreach (Comment readableComment in _unitOfWork.Comments.ReadAll().Result)
            {
                comments.Add(new CommentDTO
                {
                    Id = readableComment.Id,
                    Message = readableComment.Message,
                    Project = new ProjectService(_unitOfWork).ReadById(readableComment.Project.Id).Result,
                    User = new UserService(_unitOfWork, null, null).ReadById(readableComment.User.Id).Result
                });
            }
            return await Task.Run(() => comments);
        }

        public async Task<CommentDTO> ReadById(int id)
        {
            Comment readableComment = _unitOfWork.Comments.ReadById(id).Result;

            return await Task.Run(() => new CommentDTO
            {
                Id = readableComment.Id,
                Message = readableComment.Message,
                Project = new ProjectService(_unitOfWork).ReadById(readableComment.Project.Id).Result,
                User = new UserService(_unitOfWork, null, null).ReadById(readableComment.User.Id).Result
            });
        }

        public async Task Update(CommentDTO comment)
        {
            await _unitOfWork.Comments.Update(new Comment
            {
                Id = comment.Id,
                Message = comment.Message,
                Project = _unitOfWork.Projects.ReadById(comment.Project.Id).Result,
                User = _unitOfWork.Users.ReadById(comment.User.Id).Result
            });
        }
    }
}
