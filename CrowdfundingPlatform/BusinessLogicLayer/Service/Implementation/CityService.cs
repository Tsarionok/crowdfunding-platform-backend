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
            IConfigurationProvider configuration = new MapperConfiguration(cfg => cfg.CreateMap<CityDTO, City>());
            Mapper mapper = new Mapper(configuration);
            await _unitOfWork.Cities.Create(mapper.Map<City>(city));
        }

        public async Task<CityDTO> DeleteById(int id)
        {
            IConfigurationProvider configuration = new MapperConfiguration(cfg => cfg.CreateMap<City, CityDTO>());
            Mapper mapper = new Mapper(configuration);
            return await Task.Run(() => mapper.Map<CityDTO>(_unitOfWork.Cities.DeleteById(id).Result));
        }

        public async Task<IEnumerable<CityDTO>> ReadAll()
        {
            IConfigurationProvider configuration = new MapperConfiguration(cfg => cfg.CreateMap<City, CityDTO>());
            Mapper mapper = new Mapper(configuration);
            return await Task.Run(() => mapper.Map<IEnumerable<CityDTO>>(_unitOfWork.Cities.ReadAll().Result));
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
    }
}
