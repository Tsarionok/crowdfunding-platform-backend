using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Service;
using CrowdfundingPlatform.Models.Photo;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfundingPlatform.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        IPhotoService _photoService;
        IProjectService _projectService;

        public PhotosController(IPhotoService photoService, IProjectService projectService)
        {
            _photoService = photoService;
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<PhotoModel>>> Get()
        {
            Mapper mapper = new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<PhotoDTO, PhotoModel>()
                        .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(
                                source => source.Project.Id
                            ))
                ));
            return Ok(mapper.Map<List<PhotoModel>>(await _photoService.ReadAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoModel>> Get(int id)
        {
            Mapper mapper = new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<PhotoDTO, PhotoModel>()
                        .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(
                                source => source.Project.Id
                            ))
                ));
            return Ok(mapper.Map<PhotoModel>(await _photoService.ReadById(id)));
        }

        // TODO: fix creating Photo
        [HttpPost]
        public async Task<ActionResult> Post(PhotoCreateModel photo)
        {
            await _photoService.Create(new Mapper(new MapperConfiguration(
                cfg => cfg.CreateMap<PhotoCreateModel, PhotoDTO>()
                    .ForMember(dest => dest.Project, opt => opt.MapFrom(
                            source => _projectService.ReadById(source.ProjectId).Result
                        ))
                )).Map<PhotoDTO>(photo));
            return Ok();
        }

        // TODO: fix editting Photo
        [HttpPut]
        public async Task<ActionResult> Put(PhotoModel photo)
        {
            await _photoService.Update(new Mapper(new MapperConfiguration(
                       cfg => cfg.CreateMap<PhotoModel, PhotoDTO>()
                   )).Map<PhotoDTO>(photo)
               );
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _photoService.DeleteById(id);
            return Ok();
        }
    }
}
