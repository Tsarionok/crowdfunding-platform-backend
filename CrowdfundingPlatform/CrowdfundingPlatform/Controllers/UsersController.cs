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
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfundingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;
        ICityService _cityService;

        //TODO: relocate to DAL
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UsersController(IUserService userService, 
            ICityService cityService,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userService = userService;
            _cityService = cityService;
            _userManager = userManager;
            _signInManager = signInManager;
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
        public async Task<ActionResult> Register(UserRegistrationModel user)
        {
            if (ModelState.IsValid)
            {
                User registeredUser = new Mapper(new MapperConfiguration(
                        cfg => cfg.CreateMap<UserRegistrationModel, User>()
                            .ForMember(dest => dest.UserName, opt => opt.MapFrom(
                                    source => source.Email
                                ))
                            .ForMember(dest => dest.Email, opt => opt.MapFrom(
                                    source => source.Email
                                ))
                            .ForAllOtherMembers(opt => opt.Ignore())
                    )).Map<User>(user);
                // добавляем пользователя
                var result = await _userManager.CreateAsync(registeredUser, user.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(registeredUser, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

/*            await _userService.Create(new Mapper(new MapperConfiguration(
                cfg => cfg.CreateMap<UserRegistrationModel, UserDTO>()
                )).Map<UserDTO>(user));*/
            return Ok();
        }

        // TODO: fix edit endpoint
        [HttpPut("edit")]
        public async Task<ActionResult> Put(UserEditModel user)
        {
            await _userService.Update(new Mapper(new MapperConfiguration(
                        cfg => cfg.CreateMap<UserEditModel, UserDTO>()
                            .ForMember(dest => dest.City, opt => opt.MapFrom(source => _cityService.ReadById(source.CityId)))
                            .ForMember(dest => dest.Avatar, opt => opt.Ignore())
                    )).Map<UserDTO>(user)
                );
            return Ok();
        }

        [HttpPut("changePass")]
        public async Task<ActionResult> Put(UserChangePasswordModel user)
        {
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _userService.DeleteById(id);
            return Ok();
        }
    }
}
