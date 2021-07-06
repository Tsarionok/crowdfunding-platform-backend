using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
                IsOwner = userProject.IsOwner
            });
        }

        public async Task<UserProjectDTO> DeleteById(int id)
        {
            UserProject deletedUserProject = _unitOfWork.UserProjects.DeleteById(id).Result;

            return await Task.Run(() => new UserProjectDTO
            {
                Id = deletedUserProject.Id,
                Evaluation = deletedUserProject.Evaluation.Value,
                IsFavourites = deletedUserProject.IsFavourites,
                IsOwner = deletedUserProject.IsOwner
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
                    Evaluation = readableUserProject.Evaluation.Value,
                    IsFavourites = readableUserProject.IsFavourites,
                    IsOwner = readableUserProject.IsOwner
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
                Evaluation = readableUserProject.Evaluation.Value,
                IsFavourites = readableUserProject.IsFavourites,
                IsOwner = readableUserProject.IsOwner
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
                Project = _unitOfWork.Projects.ReadById(userProject.ProjectId).Result,
                User = _unitOfWork.Users.ReadById(userProject.UserId).Result
            });
        }

        public async Task Estimate(UserProjectDTO evaluation)
        {
            await _unitOfWork.UserProjects.Estimate(new Mapper(
                    new MapperConfiguration(cfg => cfg.CreateMap<UserProjectDTO, UserProject>()
                    .ForMember(dest => dest.Project, opt => opt.Ignore())
                    .ForMember(dest => dest.User, opt => opt.Ignore())
                )).Map<UserProject>(evaluation));
        }

        public async Task AddToFavourites(UserProjectDTO favourite)
        {
            await _unitOfWork.UserProjects.AddToFavourites(new Mapper(
                    new MapperConfiguration(cfg => cfg.CreateMap<UserProjectDTO, UserProject>()
                    .ForMember(dest => dest.Project, opt => opt.Ignore())
                    .ForMember(dest => dest.User, opt => opt.Ignore())
                )).Map<UserProject>(favourite));
        }

        public async Task UpdateOwner(UserProjectDTO owner)
        {
            await _unitOfWork.UserProjects.UpdateOwner(new Mapper(
                    new MapperConfiguration(cfg => cfg.CreateMap<UserProjectDTO, UserProject>()
                    .ForMember(dest => dest.Project, opt => opt.Ignore())
                    .ForMember(dest => dest.User, opt => opt.Ignore())
                )).Map<UserProject>(owner));
        }
    }
}
