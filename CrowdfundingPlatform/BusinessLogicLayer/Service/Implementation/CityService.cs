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
            await _unitOfWork.Cities.Create(new City
            {
                Name = city.Name,
                CountryId = city.Country.Id,
                Country = _unitOfWork.Countries.ReadById(city.Country.Id).Result
            });
        }

        public async Task<CityDTO> DeleteById(int id)
        {
            City city = _unitOfWork.Cities.DeleteById(id).Result;

            return await Task.Run(() => new CityDTO
            {
                Id = city.Id,
                Name = city.Name,
                Country = new CountryService(_unitOfWork).ReadById(city.CountryId).Result
            });
        }

        public async Task<ICollection<CityDTO>> ReadAll()
        {
            ICollection<CityDTO> cities = new List<CityDTO>();

            foreach (City readableCity in _unitOfWork.Cities.ReadAll().Result)
            {
                cities.Add(new CityDTO
                {
                    Id = readableCity.Id,
                    Name = readableCity.Name,
                    Country = new CountryService(_unitOfWork).ReadById(readableCity.CountryId).Result
                });
            }

            return await Task.Run(() => cities);
        }

        public async Task<CityDTO> ReadById(int id)
        {
            City readableCity = _unitOfWork.Cities.ReadById(id).Result;

            return await Task.Run(() => new CityDTO
            {
                Id = readableCity.Id,
                Name = readableCity.Name,
                Country = new CountryService(_unitOfWork).ReadById(readableCity.CountryId).Result
            });
        }

        public async Task Update(CityDTO city)
        {
            await _unitOfWork.Cities.Update(new City
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.Country.Id,
                Country = _unitOfWork.Countries.ReadById(city.Country.Id).Result
            });
        }

        public bool HasAnyItem(int id)
        {
            return _unitOfWork.Cities.HasAny(id).Result;
        }
    }
}
