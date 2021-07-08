using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Service;
using BusinessLogicLayer.Service.Implementation;
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
    public class CitiesController : ControllerBase
    {
        ICityService _service;
        ICountryService _countryService;

        public CitiesController(ICityService service, ICountryService countryService)
        {
            _service = service;
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<CityWithCountryModel>>> Get()
        {
            ICollection<CityWithCountryModel> cities = new List<CityWithCountryModel>();
            foreach (CityDTO readableCity in await _service.ReadAll())
            {
                cities.Add(new CityWithCountryModel
                {
                    Id = readableCity.Id,
                    Name = readableCity.Name,
                    Country = new CountryModel
                    {
                        Id = readableCity.Country.Id,
                        Name = readableCity.Country.Name
                    }
                });
            }
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityWithCountryModel>> Get(int id)
        {
            CityDTO readableCity = await _service.ReadById(id);

            if (readableCity == null)
            {
                return NotFound();
            }

            return Ok(new CityWithCountryModel
            {
                Id = readableCity.Id,
                Name = readableCity.Name,
                Country = new CountryModel
                {
                    Id = readableCity.Country.Id,
                    Name = readableCity.Country.Name
                }
            });
        }

        [HttpPost]
        public async Task<ActionResult> Post(CityCreatedModel city)
        {
            if (city == null || city.Name == "")
            {
                return BadRequest();
            }
            await _service.Create(new CityDTO
            {
                Name = city.Name,
                Country = _countryService.ReadAll().Result.Where(c => c.Id == city.CountryId).FirstOrDefault()
            });
            return Ok(city);
        }

        [HttpPut]
        public async Task<ActionResult> Put(CityUpdatedModel city)
        {
            if (city == null)
            {
                return NotFound();
            }
            await _service.Update(new CityDTO
            {
                Id = city.Id,
                Name = city.Name,
                Country = await _countryService.ReadById(city.CountryId)
            });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            CityDTO city = await _service.DeleteById(id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
