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

        public void Create(CountryDTO country)
        {
            IConfigurationProvider configuration = new MapperConfiguration(cfg => cfg.CreateMap<CountryDTO, Country>());
            Mapper mapper = new Mapper(configuration);
            _unitOfWork.Countries.Create(mapper.Map<Country>(country));

        }

        public void DeleteById(int id)
        {
            _unitOfWork.Countries.DeleteById(id);
        }

        public ICollection<CountryDTO> ReadAll()
        {
            IConfigurationProvider configuration = new MapperConfiguration(cfg => cfg.CreateMap<Country, CountryDTO>());
            Mapper mapper = new Mapper(configuration);
            return mapper.Map<ICollection<CountryDTO>>(_unitOfWork.Countries.ReadAll());

        }

        public CountryDTO ReadById(int id)
        {
            IConfigurationProvider configuration = new MapperConfiguration(cfg => cfg.CreateMap<Country, CountryDTO>());
            Mapper mapper = new Mapper(configuration);
            return mapper.Map<CountryDTO>(_unitOfWork.Countries.ReadById(id));
        }

        public void Update(CountryDTO country)
        {
            _unitOfWork.Countries.Update(new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<CountryDTO, Country>()
                )).Map<Country>(country));
        }
    }
}
