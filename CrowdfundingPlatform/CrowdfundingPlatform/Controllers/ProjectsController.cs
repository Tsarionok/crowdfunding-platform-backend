﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Service;
using CrowdfundingPlatform.Models.Category;
using CrowdfundingPlatform.Models.Project;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfundingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        IProjectService _projectService;
        ICategoryService _categoryService;

        public ProjectsController(IProjectService projectService, ICategoryService categoryService)
        {
            _projectService = projectService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ProjectModel>>> Get()
        {
            Mapper mapper = new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<ProjectDTO, ProjectModel>()
                        .ForMember(dest => dest.Category, opt => opt.MapFrom(
                                source => new Mapper(new MapperConfiguration(
                                        cfg => cfg.CreateMap<CategoryDTO, CategoryModel>()
                                    )).Map<CategoryModel>(source.Category)
                            ))
                ));
            return Ok(mapper.Map<List<ProjectModel>>(await _projectService.ReadAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectModel>> Get(int id)
        {
            Mapper mapper = new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<ProjectDTO, ProjectModel>()
                        .ForMember(dest => dest.Category, opt => opt.MapFrom(
                                source => new Mapper(new MapperConfiguration(
                                        cfg => cfg.CreateMap<CategoryDTO, CategoryModel>()
                                    )).Map<CategoryModel>(source.Category)
                            ))
                ));
            return Ok(mapper.Map<ProjectModel>(await _projectService.ReadById(id)));
        }


        // TODO: fix adding project
        [HttpPost]
        public async Task<ActionResult> Post(ProjectCreateModel project)
        {
            await _projectService.Create(new Mapper(new MapperConfiguration(
                cfg => cfg.CreateMap<ProjectCreateModel, ProjectDTO>()
                    .ForMember(dest => dest.Category, opt => opt.MapFrom(
                            source => _categoryService.ReadById(source.CategoryId).Result
                        ))
                    .ForMember(dest => dest.MainPhoto, opt => opt.Ignore())
                )).Map<ProjectDTO>(project));
            return Ok();
        }

        // TODO: fix editing project
        [HttpPut]
        public async Task<ActionResult> Put(ProjectUpdateModel project)
        {
            await _projectService.Update(new Mapper(new MapperConfiguration(
                        cfg => cfg.CreateMap<ProjectUpdateModel, ProjectDTO>()
                    )).Map<ProjectDTO>(project)
                );
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _projectService.DeleteById(id);
            return Ok();
        }
    }
}
