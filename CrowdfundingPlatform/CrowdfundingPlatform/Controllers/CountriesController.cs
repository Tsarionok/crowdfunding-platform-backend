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
        public ICollection<CountryDTO> Get()
        {
            return _service.ReadAll();
        }

        [HttpGet("{id}")]
        public CountryDTO Get(int id)
        {
            return _service.ReadById(id);
        }

        [HttpPost]
        public void Post(CountryDTO country)
        {
            _service.Create(country);
        }

        [HttpPut]
        public void Put(CountryDTO country)
        {
            _service.Update(country);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.DeleteById(id);
        }
    }
}
