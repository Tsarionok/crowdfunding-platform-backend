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
            IConfigurationProvider configuration = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, Category>());
            Mapper mapper = new Mapper(configuration);
            await _unitOfWork.Categories.Create(mapper.Map<Category>(category));
        }

        public async Task<CategoryDTO> DeleteById(int id)
        {
            IConfigurationProvider configuration = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>());
            Mapper mapper = new Mapper(configuration);
            return await Task.Run(() => mapper.Map<CategoryDTO>(_unitOfWork.Categories.DeleteById(id).Result));
        }

        public async Task<ICollection<CategoryDTO>> ReadAll()
        {
            IConfigurationProvider configuration = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>());
            Mapper mapper = new Mapper(configuration);
            return await Task.Run(() => mapper.Map<ICollection<CategoryDTO>>(_unitOfWork.Categories.ReadAll().Result));
        }

        public async Task<CategoryDTO> ReadById(int id)
        {
            IConfigurationProvider configuration = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>());
            Mapper mapper = new Mapper(configuration);
            return await Task.Run(() => mapper.Map<CategoryDTO>(_unitOfWork.Categories.ReadById(id).Result));
        }

        public async Task Update(CategoryDTO category)
        {
            await _unitOfWork.Categories.Update(new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, Category>()))
                .Map<Category>(category));
        }
    }
}
