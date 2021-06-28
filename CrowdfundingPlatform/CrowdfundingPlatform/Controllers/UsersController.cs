using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Service;
using CrowdfundingPlatform.Models.City;
using CrowdfundingPlatform.Models.Country;
using CrowdfundingPlatform.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfundingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<UserModel>>> Get()
        {
            Mapper mapper = new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<UserDTO, UserModel>()
                        .ForMember(dest => dest.City, opt => opt.MapFrom(source =>
                            new Mapper(new MapperConfiguration(
                                cfg => cfg.CreateMap<CityDTO, CityWithCountryModel>()
                                    .ForMember(dest => dest.Country, opt => opt.MapFrom(source =>
                                        new Mapper(new MapperConfiguration(
                                                cfg => cfg.CreateMap<CountryDTO, CountryModel>()
                                            )).Map<CountryModel>(source.Country)
                                        ))
                            )).Map<CityWithCountryModel>(source.City)))
                ));
            return Ok(mapper.Map<List<UserModel>>(await _userService.ReadAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            Mapper mapper = new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<UserDTO, UserModel>()
                        .ForMember(dest => dest.City, opt => opt.MapFrom(source =>
                            new Mapper(new MapperConfiguration(
                                cfg => cfg.CreateMap<CityDTO, CityWithCountryModel>()
                                    .ForMember(dest => dest.Country, opt => opt.MapFrom(source =>
                                        new Mapper(new MapperConfiguration(
                                                cfg => cfg.CreateMap<CountryDTO, CountryModel>()
                                            )).Map<CountryModel>(source.Country)
                                        ))
                            )).Map<CityWithCountryModel>(source.City)))
                ));
            return Ok(mapper.Map<UserModel>(await _userService.ReadById(id)));
        }

        [HttpPost]
        public async Task<ActionResult> Post(UserRegistrationModel user)
        {
            await _userService.Create(new Mapper(new MapperConfiguration(
                cfg => cfg.CreateMap<UserRegistrationModel, UserDTO>()
                )).Map<UserDTO>(user));
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(UserLoginModel user)
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
