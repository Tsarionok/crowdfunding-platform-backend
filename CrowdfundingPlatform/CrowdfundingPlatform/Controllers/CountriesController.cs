using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Service;
using BusinessLogicLayer.Service.Implementation;
using CrowdfundingPlatform.Models;
using CrowdfundingPlatform.Models.City;
using CrowdfundingPlatform.Models.Country;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfundingPlatform.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        // TODO: Include automapper
        ICountryService _service;

        public CountriesController(ICountryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<CountryWithCitiesModel>>> Get()
        {
            ICollection<CountryWithCitiesModel> countries = new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<CountryDTO, CountryWithCitiesModel>()
                        .ForMember(dest => dest.Cities, opt => opt.Ignore())
                )).Map<ICollection<CountryWithCitiesModel>>(await _service.ReadAll());
            
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryWithCitiesModel>> Get(int id)
        {
            CountryWithCitiesModel country = new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<CountryDTO, CountryWithCitiesModel>()
                        .ForMember(dest => dest.Cities, opt => opt.Ignore())
                )).Map<CountryWithCitiesModel>(await _service.ReadById(id));

            return Ok(country);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CountryCreateModel model)
        {
            if (model == null || model.Name == "")
            {
                return BadRequest();
            }
            await _service.Create(new CountryDTO
            {
                Name = model.Name
            });

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(CountryModel country)
        {
            if (country == null)
            {
                return NotFound();
            }
            await _service.Update(new CountryDTO
            {
                Id = country.Id,
                Name = country.Name
            });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<CountryDTO, CountryWithCitiesModel>()
                        .ForMember(dest => dest.Cities, opt => opt.Ignore())
                )).Map<CountryWithCitiesModel>(await _service.DeleteById(id));

            return Ok();
        }
    }
}
