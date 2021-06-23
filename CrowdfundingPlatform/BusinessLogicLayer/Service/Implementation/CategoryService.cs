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
    public class CategoryService : BaseService, ICategoryService
    {

        public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task Create(CategoryDTO category)
        {
            await _unitOfWork.Categories.Create(new Category
            {
                Name = category.Name
            });
        }

        public async Task<CategoryDTO> DeleteById(int id)
        {
            Category deletedCategory = _unitOfWork.Categories.DeleteById(id).Result;

            return await Task.Run(() => new CategoryDTO
            {
                Id = deletedCategory.Id,
                Name = deletedCategory.Name
            });
        }

        public async Task<ICollection<CategoryDTO>> ReadAll()
        {
            ICollection<CategoryDTO> categories = new List<CategoryDTO>();

            foreach (Category readableCategory in _unitOfWork.Categories.ReadAll().Result)
            {
                categories.Add(new CategoryDTO
                {
                    Id = readableCategory.Id,
                    Name = readableCategory.Name
                });
            }

            return await Task.Run(() => categories);
        }

        public async Task<CategoryDTO> ReadById(int id)
        {
            Category readableCategory = _unitOfWork.Categories.ReadById(id).Result;

            return await Task.Run(() => new CategoryDTO
            {
                Id = readableCategory.Id,
                Name = readableCategory.Name
            });
        }

        public async Task Update(CategoryDTO category)
        {
            await _unitOfWork.Categories.Update(new Category
            {
                Id = category.Id,
                Name = category.Name
            });
        }
    }
}
