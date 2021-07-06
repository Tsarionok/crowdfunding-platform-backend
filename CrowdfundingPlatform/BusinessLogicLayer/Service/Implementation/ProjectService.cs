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
    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task Create(ProjectDTO project)
        {
            Project createdProject = new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<ProjectDTO, Project>()
                        .ForMember(dest => dest.Category, opt => opt.MapFrom(
                                source => new Mapper(new MapperConfiguration(
                                        cfg => cfg.CreateMap<CategoryDTO, Category>()
                                            .ForMember(dest => dest.Projects, opt => opt.Ignore())
                                    )).Map<Category>(source.Category)
                            ))
                )).Map<Project>(project);
            await _unitOfWork.Projects.Create(createdProject);
        }

        public async Task<ProjectDTO> DeleteById(int id)
        {
            ProjectDTO deletedProject = new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<Project, ProjectDTO>()
                        .ForMember(dest => dest.Category, opt => opt.MapFrom(
                                source => new Mapper(new MapperConfiguration(
                                        cfg => cfg.CreateMap<Category, CategoryDTO>()
                                    )).Map<CategoryDTO>(source.Category) 
                            ))
                )).Map<ProjectDTO>(await _unitOfWork.Projects.DeleteById(id));
            return await Task.Run(() => deletedProject);
        }

        public async Task<ICollection<ProjectDTO>> ReadAll()
        {
            ICollection<ProjectDTO> projects = new List<ProjectDTO>();

            foreach (Project readableProject in _unitOfWork.Projects.ReadAll().Result)
            {
                CategoryDTO createdCategory = new CategoryDTO
                {
                    Id = readableProject.CategoryId.Value,
                    Name = readableProject.Category.Name
                };

                projects.Add(new ProjectDTO {
                    Id = readableProject.Id,
                    Name = readableProject.Name,
                    Description = readableProject.Description,
                    StartFundraisingDate = readableProject.StartFundraisingDate.Value,
                    FinalFundraisingDate = readableProject.FinalFundraisingDate.Value,
                    CurrentDonationSum = readableProject.CurrentDonationSum,
                    TotalDonationSum = readableProject.TotalDonationSum.Value,
                    Category = createdCategory,
                    MainPhoto = readableProject.MainPhoto
                });
            }
            return await Task.Run(() => projects);
        }

        public async Task<ProjectDTO> ReadById(int id)
        {
            ProjectDTO project = new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<Project, ProjectDTO>()
                        .ForMember(dest => dest.Category, opt => opt.MapFrom(
                                source => new Mapper(new MapperConfiguration(
                                        cfg => cfg.CreateMap<Category, CategoryDTO>()
                                    )).Map<CategoryDTO>(source.Category)
                            ))
                )).Map<ProjectDTO>(await _unitOfWork.Projects.ReadById(id));
            return await Task.Run(() => project);
        }

        public async Task Update(ProjectDTO project)
        {
            Project updatedProject = new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<ProjectDTO, Project>()
                        .ForMember(dest => dest.Category, opt => opt.MapFrom(
                                source => new Mapper(new MapperConfiguration(
                                        cfg => cfg.CreateMap<CategoryDTO, Category>()
                                        .ForMember(dest => dest.Projects, opt => opt.Ignore())
                                    )).Map<Category>(source.Category)
                            ))
                )).Map<Project>(project);
            await _unitOfWork.Projects.Update(updatedProject);
        }
    }
}
