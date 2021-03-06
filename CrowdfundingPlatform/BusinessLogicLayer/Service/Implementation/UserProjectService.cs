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
    public class UserProjectService : BaseService, IUserProjectService
    {
        public UserProjectService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task Create(UserProjectDTO userProject)
        {
            await _unitOfWork.UserProjects.Create(new UserProject
            {
                Evaluation = userProject.Evaluation,
                IsFavourites = userProject.IsFavourites,
                IsOwner = userProject.IsOwner,
                Project = _unitOfWork.Projects.ReadById(userProject.Project.Id).Result,
                User = _unitOfWork.Users.ReadById(userProject.User.Id).Result
            });
        }

        public async Task<UserProjectDTO> DeleteById(int id)
        {
            UserProject deletedUserProject = _unitOfWork.UserProjects.DeleteById(id).Result;

            return await Task.Run(() => new UserProjectDTO
            {
                Id = deletedUserProject.Id,
                Evaluation = deletedUserProject.Evaluation,
                IsFavourites = deletedUserProject.IsFavourites,
                IsOwner = deletedUserProject.IsOwner,
                Project = new ProjectService(_unitOfWork).ReadById(deletedUserProject.Project.Id).Result,
                User = new UserService(_unitOfWork).ReadById(deletedUserProject.User.Id).Result
            });
        }

        public async Task<ICollection<UserProjectDTO>> ReadAll()
        {
            ICollection<UserProjectDTO> userProjects = new List<UserProjectDTO>();

            foreach (UserProject readableUserProject in _unitOfWork.UserProjects.ReadAll().Result)
            {
                userProjects.Add(new UserProjectDTO
                {
                    Id = readableUserProject.Id,
                    Evaluation = readableUserProject.Evaluation,
                    IsFavourites = readableUserProject.IsFavourites,
                    IsOwner = readableUserProject.IsOwner,
                    Project = new ProjectService(_unitOfWork).ReadById(readableUserProject.Project.Id).Result,
                    User = new UserService(_unitOfWork).ReadById(readableUserProject.User.Id).Result
                });
            }

            return await Task.Run(() => userProjects);
        }

        public async Task<UserProjectDTO> ReadById(int id)
        {
            UserProject readableUserProject = _unitOfWork.UserProjects.ReadById(id).Result;

            return await Task.Run(() => new UserProjectDTO
            {
                Id = readableUserProject.Id,
                Evaluation = readableUserProject.Evaluation,
                IsFavourites = readableUserProject.IsFavourites,
                IsOwner = readableUserProject.IsOwner,
                Project = new ProjectService(_unitOfWork).ReadById(readableUserProject.Project.Id).Result,
                User = new UserService(_unitOfWork).ReadById(readableUserProject.User.Id).Result
            });
        }

        public async Task Update(UserProjectDTO userProject)
        {
            await _unitOfWork.UserProjects.Update(new UserProject
            {
                Id = userProject.Id,
                Evaluation = userProject.Evaluation,
                IsFavourites = userProject.IsFavourites,
                IsOwner = userProject.IsOwner,
                Project = _unitOfWork.Projects.ReadById(userProject.Project.Id).Result,
                User = _unitOfWork.Users.ReadById(userProject.User.Id).Result
            });
        }
    }
}
