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
            await _unitOfWork.Categories.Create(new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<CategoryDTO, Category>()
                        .ForMember(dest => dest.Projects, opt => opt.Ignore())
                )).Map<Category>(category));
        }

        public async Task<CategoryDTO> DeleteById(int id)
        {
            return new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<Category, CategoryDTO>()
                )).Map<CategoryDTO>(await _unitOfWork.Categories.DeleteById(id)); 
        }

        public async Task<ICollection<CategoryDTO>> ReadAll()
        {
            return new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<Category, CategoryDTO>()
                )).Map<ICollection<CategoryDTO>>(await _unitOfWork.Categories.ReadAll());
        }

        public async Task<CategoryDTO> ReadById(int id)
        {
            return new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<Category, CategoryDTO>()
                )).Map<CategoryDTO>(await _unitOfWork.Categories.ReadById(id));
        }

        public async Task Update(CategoryDTO category)
        {
            await _unitOfWork.Categories.Update(new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<CategoryDTO, Category>()
                        .ForMember(dest => dest.Projects, opt => opt.Ignore())
                )).Map<Category>(category));
        }
    }
}
