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
    public class CityService : BaseService, ICityService
    {
        public CityService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task Create(CityDTO city)
        {
            City newCity = new City
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.Country.Id,
                Country = _unitOfWork.Countries.ReadById(city.Country.Id).Result
            };
            await _unitOfWork.Cities.Create(newCity);
        }

        public async Task<CityDTO> DeleteById(int id)
        {
            IConfigurationProvider configuration = new MapperConfiguration(cfg => cfg.CreateMap<City, CityDTO>());
            Mapper mapper = new Mapper(configuration);
            return await Task.Run(() => mapper.Map<CityDTO>(_unitOfWork.Cities.DeleteById(id).Result));
        }

        public async Task<ICollection<CityDTO>> ReadAll()
        {
            IEnumerable<City> cities = await _unitOfWork.Cities.ReadAll();
            ICollection<CityDTO> convertedCities = new List<CityDTO>();
            foreach (City city in cities)
            {
                CityDTO convertedCity = new CityDTO
                {
                    Id = city.Id,
                    Name = city.Name
                };
                convertedCities.Add(convertedCity);
            }
            return convertedCities;
        }

        public async Task<CityDTO> ReadById(int id)
        {
            IConfigurationProvider configuration = new MapperConfiguration(cfg => cfg.CreateMap<City, CityDTO>());
            Mapper mapper = new Mapper(configuration);
            return await Task.Run(() => mapper.Map<CityDTO>(_unitOfWork.Cities.ReadById(id).Result));
        }

        public async Task Update(CityDTO city)
        {
            await _unitOfWork.Cities.Update(new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<CityDTO, City>()))
                .Map<City>(city));
        }

        public bool HasAnyItem(int id)
        {
            return _unitOfWork.Cities.HasAny(id).Result;
        }
    }
}
