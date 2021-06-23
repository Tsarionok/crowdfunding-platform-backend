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

        public async Task Create(CountryWithCitiesDTO country)
        {
            Country addingCountry = new Country
            {
                Id = country.Id,
                Name = country.Name,
                Cities = new List<City>()
            };
            await _unitOfWork.Countries.Create(addingCountry);
        }

        public async Task<CountryWithCitiesDTO> DeleteById(int id)
        {
            Country deletedCountry = _unitOfWork.Countries.DeleteById(id).Result;

            CountryWithCitiesDTO country = new CountryWithCitiesDTO
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

        public async Task<ICollection<CountryWithCitiesDTO>> ReadAll()
        {
            ICollection<CountryWithCitiesDTO> countries = new List<CountryWithCitiesDTO>();

            foreach (Country readableCountry in _unitOfWork.Countries.ReadAll().Result)
            {
                CountryWithCitiesDTO country = new CountryWithCitiesDTO
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

        public async Task<CountryWithCitiesDTO> ReadById(int id)
        {
            Country readableCountry = _unitOfWork.Countries.ReadById(id).Result;

            CountryWithCitiesDTO country = new CountryWithCitiesDTO
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

        public async Task Update(CountryWithCitiesDTO country)
        {
            Country updatedCountry = new Country
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
                updatedCountry.Cities.Add(city);
            }
            await _unitOfWork.Countries.Update(updatedCountry);
        }

        public bool HasAnyItem(int id)
        {
            return _unitOfWork.Countries.HasAnyItem(id).Result;
        }
    }
}
