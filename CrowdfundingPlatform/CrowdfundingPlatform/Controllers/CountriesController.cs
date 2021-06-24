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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfundingPlatform.Controllers
{
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
            ICollection<CountryWithCitiesModel> countries = new List<CountryWithCitiesModel>();

            foreach (CountryDTO readableCountry in await _service.ReadAll())
            {
                ICollection<CityModel> cities = new List<CityModel>();

                foreach(CityDTO city in readableCountry.Cities)
                {
                    cities.Add(new CityModel
                    {
                        Id = city.Id,
                        Name = city.Name
                    });
                }
                countries.Add(new CountryWithCitiesModel
                {
                    Id = readableCountry.Id,
                    Name = readableCountry.Name,
                    Cities = cities
                });
            }
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryWithCitiesModel>> Get(int id)
        {
            CountryDTO readableCountry = await _service.ReadById(id);

            ICollection<CityModel> cities = new List<CityModel>();

            foreach (CityDTO city in readableCountry.Cities)
            {
                cities.Add(new CityModel
                {
                    Id = city.Id,
                    Name = city.Name
                });
            }

            return Ok(new CountryWithCitiesModel
            {
                Id = readableCountry.Id,
                Name = readableCountry.Name,
                Cities = cities
            });
        }

        [HttpPost]
        public async Task<ActionResult> Post(OnlyNameModel model)
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
            CountryDTO country = await _service.DeleteById(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
