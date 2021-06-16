using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfundingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        ICountryService _service;

        public CountriesController(ICountryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> Get()
        {
            return Ok(await _service.ReadAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDTO>> Get(int id)
        {
            return Ok(await _service.ReadById(id));
        }

        [HttpPost]
        public async Task<ActionResult<CountryDTO>> Post(CountryDTO country)
        {
            if (country == null)
            {
                return BadRequest();
            }
            await _service.Create(country);
            return Ok(country);
        }

        [HttpPut]
        public async Task<ActionResult> Put(CountryDTO country)
        {
            if (country == null)
            {
                return BadRequest();
            }
            if (!_service.HasAny(country.Id))
            {
                return NotFound();
            }
            await _service.Update(country);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CountryDTO>> Delete(int id)
        {
            CountryDTO country = null;
            if (_service.HasAny(id))
            {
                country = _service.ReadById(id).Result;
            }
            await _service.DeleteById(id);
            return Ok(country);
        }
    }
}
