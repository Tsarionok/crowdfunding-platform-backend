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
            IConfigurationProvider configuration = new MapperConfiguration(cfg => cfg.CreateMap<CountryDTO, Country>());
            Mapper mapper = new Mapper(configuration);
            await _unitOfWork.Countries.Create(mapper.Map<Country>(country));
        }

        public async Task<CountryDTO> DeleteById(int id)
        {
            IConfigurationProvider configuration = new MapperConfiguration(cfg => cfg.CreateMap<Country, CountryDTO>());
            Mapper mapper = new Mapper(configuration);
            return await Task.Run(() => mapper.Map<CountryDTO>(_unitOfWork.Countries.DeleteById(id).Result));
        }

        public async Task<IEnumerable<CountryDTO>> ReadAll()
        {
            IConfigurationProvider configuration = new MapperConfiguration(cfg => cfg.CreateMap<Country, CountryDTO>());
            Mapper mapper = new Mapper(configuration);
            return await Task.Run(() => mapper.Map<IEnumerable<CountryDTO>>(_unitOfWork.Countries.ReadAll().Result));
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
