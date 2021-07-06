using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Service;
using BusinessLogicLayer.Service.Implementation;
using CrowdfundingPlatform.Models.City;
using CrowdfundingPlatform.Models.Country;
using CrowdfundingPlatform.Models.User;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Authorization;
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
        IEmailService _emailService;
        IJwtService _jwtService;

        //TODO: relocate to DAL
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UsersController(IUserService userService, 
            ICityService cityService,
            IEmailService emailService,
            IJwtService jwtService,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userService = userService;
            _cityService = cityService;
            _emailService = emailService;
            _jwtService = jwtService;
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
        public async Task<ActionResult<UserModel>> Get(string id)
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

                var result = await _userManager.CreateAsync(registeredUser, user.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(registeredUser);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Users",
                        new { userId = registeredUser.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    await _emailService.SendEmailAsync(registeredUser.Email, "Confirm your account",
                        $"Confirm your registration by following the link: <a href='{callbackUrl}'>link</a>");

                    return Content("To complete registration, check your email and follow the link provided in the letter");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserAuthenticateModel user)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);
                if (result.Succeeded)
                {
                    UserLoginModel userLogin = new Mapper(new MapperConfiguration(
                            cfg => cfg.CreateMap<UserAuthenticateModel, UserLoginModel>()
                                .ForMember(dest => dest.Token, opt => opt.MapFrom(
                                        source => _jwtService.CreateToken(source.Email)
                                    ))
                                // TODO: complete Id's field
                        )).Map<UserLoginModel>(user);

                    return Ok(userLogin);
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Incorrect username and (or) password");
                }
            }
            return Unauthorized(ModelState);
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

        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword(UserChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id.ToString());
                if (user != null)
                {
                    IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User is not found!");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _userService.DeleteById(id);
            return Ok();
        }

        [HttpPost("upload-avatar")]
        public async Task<IActionResult> UploadAvatar([Bind("Id,PostMode,Message,Image,AccountId,Created,Status")] UserAvatarModel post, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                using (var ms = new MemoryStream())
                {
                    Image.CopyTo(ms);
                    post.Avatar = ms.ToArray();
                }

                UserDTO user = _userService.ReadById(post.UserId);
                user.Avatar = post.Avatar;
                await _userService.UploadAvatar(user);
                return Ok(post);
            }
            return BadRequest(ModelState);
        }
    }
}
