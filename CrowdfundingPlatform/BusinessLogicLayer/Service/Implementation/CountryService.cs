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
            Country addingCountry = new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<CountryDTO, Country>()
                        .ForMember(dest => dest.Cities, opt => opt.Ignore())
                )).Map<Country>(country);
;
            await _unitOfWork.Countries.Create(addingCountry);
        }

        public async Task<CountryDTO> DeleteById(int id)
        {
            return new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<Country, CountryDTO>()
                        .ForMember(dest => dest.Cities, opt => opt.Ignore())
                )).Map<CountryDTO>(await _unitOfWork.Countries.DeleteById(id));
        }

        public async Task<ICollection<CountryDTO>> ReadAll()
        {
            return new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<Country, CountryDTO>()
                        .ForMember(dest => dest.Cities, opt => opt.Ignore())
                )).Map<ICollection<CountryDTO>>(await _unitOfWork.Countries.ReadAll());
        }

        public async Task<CountryDTO> ReadById(int id)
        {
            return new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<Country, CountryDTO>()
                        .ForMember(dest => dest.Cities, opt => opt.Ignore())
                )).Map<CountryDTO>(await _unitOfWork.Countries.ReadById(id));
        }

        public async Task Update(CountryDTO country)
        {
            Country addingCountry = new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<CountryDTO, Country>()
                        .ForMember(dest => dest.Cities, opt => opt.Ignore())
                )).Map<Country>(country);
            ;
            await _unitOfWork.Countries.Update(addingCountry);
        }
    }
}
