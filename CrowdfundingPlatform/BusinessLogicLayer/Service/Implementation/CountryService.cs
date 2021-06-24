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
                Name = country.Name,
                Cities = new List<City>()
            };
            await _unitOfWork.Countries.Create(addingCountry);
        }

        public async Task<CountryDTO> DeleteById(int id)
        {
            Country deletedCountry = _unitOfWork.Countries.DeleteById(id).Result;

            CountryDTO country = new CountryDTO
            {
                Id = deletedCountry.Id,
                Name = deletedCountry.Name,
                Cities = new List<CityDTO>()
            };

            foreach (City deletedCity in deletedCountry.Cities)
            {
                CityDTO city = new CityDTO()
                {
                    Id = deletedCity.Id,
                    Name = deletedCity.Name
                };
                country.Cities.Add(city);
            }

            return await Task.Run(() => country);
        }

        public async Task<ICollection<CountryDTO>> ReadAll()
        {
            ICollection<CountryDTO> countries = new List<CountryDTO>();

            foreach (Country readableCountry in _unitOfWork.Countries.ReadAll().Result)
            {
                CountryDTO country = new CountryDTO
                {
                    Id = readableCountry.Id,
                    Name = readableCountry.Name,
                    Cities = new List<CityDTO>()
                };
                foreach (City city in readableCountry.Cities)
                {
                    CityDTO cityDTO = new CityDTO
                    {
                        Id = city.Id,
                        Name = city.Name
                    };
                    country.Cities.Add(cityDTO);
                }
                countries.Add(country);
            }
            return await Task.Run(() => countries);
        }

        public async Task<CountryDTO> ReadById(int id)
        {
            Country readableCountry = _unitOfWork.Countries.ReadById(id).Result;

            CountryDTO country = new CountryDTO
            {
                Id = readableCountry.Id,
                Name = readableCountry.Name,
                Cities = new List<CityDTO>()
            };
            foreach (City city in readableCountry.Cities)
            {
                CityDTO cityDTO = new CityDTO
                {
                    Id = city.Id,
                    Name = city.Name
                };
                country.Cities.Add(cityDTO);
            }
            return await Task.Run(() => country);
        }

        public async Task Update(CountryDTO country)
        {
            Country updatedCountry = new Country
            {
                Id = country.Id,
                Name = country.Name
            };
            await _unitOfWork.Countries.Update(updatedCountry);
        }
    }
}
