using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Service;
using CrowdfundingPlatform.Models.Photo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfundingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        IPhotoService _photoService;

        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
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

        [HttpPost]
        public async Task<ActionResult> Post(PhotoCreateModel photo)
        {
            return NotFound();
        }

        [HttpPut]
        public async Task<ActionResult> Put(PhotoModel photo)
        {
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return NotFound();
        }
    }
}
