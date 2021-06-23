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
    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task Create(ProjectDTO project)
        {
            CategoryDTO category = project.Category;
            Category createdCategory = new Category
            {
                Id = category.Id,
                Name = category.Name
            };

            Project createdProject = new Project
            {
                Name = project.Name,
                Description = project.Description,
                StartFundraisingDate = project.StartFundraisingDate,
                FinalFundraisingDate = project.FinalFundraisingDate,
                CurrentDonationSum = project.CurrentDonationSum,
                TotalDonationSum = project.TotalDonationSum,
                Category = createdCategory,
                MainPhoto = project.MainPhoto
            };

            await _unitOfWork.Projects.Create(createdProject);
        }

        public async Task<ProjectDTO> DeleteById(int id)
        {
            Project project = _unitOfWork.Projects.DeleteById(id).Result;

            Category category = project.Category;
            CategoryDTO createdCategory = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };

            ProjectDTO deletedProject = new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartFundraisingDate = project.StartFundraisingDate,
                FinalFundraisingDate = project.FinalFundraisingDate,
                CurrentDonationSum = project.CurrentDonationSum,
                TotalDonationSum = project.TotalDonationSum,
                Category = createdCategory,
                MainPhoto = project.MainPhoto
            };
            return await Task.Run(() => deletedProject);
        }

        public async Task<ICollection<ProjectDTO>> ReadAll()
        {
            ICollection<ProjectDTO> projects = new List<ProjectDTO>();

            foreach (Project readableProject in _unitOfWork.Projects.ReadAll().Result)
            {
                Category category = readableProject.Category;
                CategoryDTO createdCategory = new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name
                };

                projects.Add(new ProjectDTO {
                    Id = readableProject.Id,
                    Name = readableProject.Name,
                    Description = readableProject.Description,
                    StartFundraisingDate = readableProject.StartFundraisingDate,
                    FinalFundraisingDate = readableProject.FinalFundraisingDate,
                    CurrentDonationSum = readableProject.CurrentDonationSum,
                    TotalDonationSum = readableProject.TotalDonationSum,
                    Category = createdCategory,
                    MainPhoto = readableProject.MainPhoto
                });
            }
            return await Task.Run(() => projects);
        }

        public async Task<ProjectDTO> ReadById(int id)
        {
            Project readableProject = _unitOfWork.Projects.ReadById(id).Result;
            Category category = readableProject.Category;
            CategoryDTO createdCategory = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };

            ProjectDTO project = new ProjectDTO
            {
                Id = readableProject.Id,
                Name = readableProject.Name,
                Description = readableProject.Description,
                StartFundraisingDate = readableProject.StartFundraisingDate,
                FinalFundraisingDate = readableProject.FinalFundraisingDate,
                CurrentDonationSum = readableProject.CurrentDonationSum,
                TotalDonationSum = readableProject.TotalDonationSum,
                Category = createdCategory,
                MainPhoto = readableProject.MainPhoto
            };

            return await Task.Run(() => project);
        }

        public async Task Update(ProjectDTO project)
        {
            CategoryDTO category = project.Category;
            Category updatedCategory = new Category
            {
                Id = category.Id,
                Name = category.Name
            };

            Project updatedProject = new Project
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartFundraisingDate = project.StartFundraisingDate,
                FinalFundraisingDate = project.FinalFundraisingDate,
                CurrentDonationSum = project.CurrentDonationSum,
                TotalDonationSum = project.TotalDonationSum,
                Category = updatedCategory,
                MainPhoto = project.MainPhoto
            };

            await _unitOfWork.Projects.Update(updatedProject);
        }
    }
}
