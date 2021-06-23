using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;

namespace BusinessLogicLayer.Service.Implementation
{
    public class CountryService : BaseService, ICountryService
    {

        public CountryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task Create(CountryDTO country)
        {
            Country addingCountry = new Country
            {
                Id = country.Id,
                Name = country.Name,
                Cities = new List<City>()
            };
            foreach (CityDTO citydto in country.Cities)
            {
                City city = new City()
                {
                    Id = citydto.Id,
                    Name = citydto.Name
                };
                addingCountry.Cities.Add(city);
            }
            await _unitOfWork.Countries.Create(addingCountry);
        }

        public async Task<CountryDTO> DeleteById(int id)
        {
            IConfigurationProvider configuration = new MapperConfiguration(cfg => cfg.CreateMap<Country, CountryDTO>());
            Mapper mapper = new Mapper(configuration);
            return await Task.Run(() => mapper.Map<CountryDTO>(_unitOfWork.Countries.DeleteById(id).Result));
        }

        public async Task<ICollection<CountryDTO>> ReadAll()
        {
            IEnumerable<Country> countries = await _unitOfWork.Countries.ReadAll();
            ICollection<CountryDTO> convertedCountries = new List<CountryDTO>();
            foreach (Country country in countries)
            {
                CountryDTO convertedCountry = new CountryDTO
                {
                    Id = country.Id,
                    Name = country.Name,
                    Cities = new List<CityDTO>()
                };
                foreach (City city in country.Cities)
                {
                    CityDTO cityDTO = new CityDTO
                    {
                        Id = city.Id,
                        Name = city.Name
                    };
                    convertedCountry.Cities.Add(cityDTO);
                }
                convertedCountries.Add(convertedCountry);
            }
            return convertedCountries;
        }

        public async Task<CountryDTO> ReadById(int id)
        {
            IConfigurationProvider configuration = new MapperConfiguration(cfg => cfg.CreateMap<Country, CountryDTO>());
            Mapper mapper = new Mapper(configuration);
            return await Task.Run(() => mapper.Map<CountryDTO>(_unitOfWork.Countries.ReadById(id).Result));
        }

        public async Task Update(CountryDTO country)
        {
            await _unitOfWork.Countries.Update(new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<CountryDTO, Country>()))
                .Map<Country>(country));
        }

        public bool HasAny(int id)
        {
            return _unitOfWork.Countries.HasAny(id).Result;
        }
    }
}
