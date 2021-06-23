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
    public class CitiesController : ControllerBase
    {
        ICityService _service;

        public CitiesController(ICityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDTO>>> Get()
        {
            return Ok(await _service.ReadAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDTO>> Get(int id)
        {
            return Ok(await _service.ReadById(id));
        }

        [HttpPost]
        public async Task<ActionResult<CityDTO>> Post(CityDTO city)
        {
            if (city == null)
            {
                return BadRequest();
            }
            await _service.Create(city);
            return Ok(city);
        }

        [HttpPut]
        public async Task<ActionResult> Put(CityDTO city)
        {
            if (city == null)
            {
                return BadRequest();
            }
            if (!_service.HasAnyItem(city.Id))
            {
                return NotFound();
            }
            await _service.Update(city);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CityDTO>> Delete(int id)
        {
            CityDTO city = null;
            if (_service.HasAnyItem(id))
            {
                city = _service.ReadById(id).Result;
            }
            await _service.DeleteById(id);
            return Ok(city);
        }
    }
}
