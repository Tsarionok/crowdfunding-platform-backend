using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Service;
using CrowdfundingPlatform.Models.Donation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfundingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationsController : ControllerBase
    {
        IDonationHistoryService _donationService;
        IProjectService _projectService;
        IUserService _userService;

        public DonationsController(
            IDonationHistoryService donationService, 
            IProjectService projectService,
            IUserService userService)
        {
            _donationService = donationService;
            _projectService = projectService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> Donate(DonateModel donate)
        {
            if(ModelState.IsValid)
            {
                await _donationService.Donate(new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<DonateModel, DonationHistoryDTO>()
                        .ForMember(dest => dest.Project, opt => opt.MapFrom(
                                // TODO: check for existence
                                source => _projectService.ReadById(source.ProjectId).Result
                            ))
                        .ForMember(dest => dest.User, opt => opt.MapFrom(
                                // TODO: check for existence
                                source => _userService.ReadById(source.UserId).Result
                            ))
                        .ForMember(dest => dest.DonationSum, opt => opt.MapFrom(
                                source => source.Amount
                            ))
                        .ForMember(dest => dest.Id, opt => opt.Ignore())
                )).Map<DonationHistoryDTO>(donate));

                return Ok(donate);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
