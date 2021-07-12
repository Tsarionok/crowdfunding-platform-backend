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
            await _unitOfWork.Cities.Create(new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<CityDTO, City>()
                        .ForMember(dest => dest.Country, opt => opt.MapFrom(
                                source => _unitOfWork.Countries.ReadById(city.Country.Id).Result
                            ))
                        .ForMember(dest => dest.CountryId, opt => opt.MapFrom(
                                source => source.Country.Id
                            ))
                )).Map<City>(city));
        }

        public async Task<CityDTO> DeleteById(int id)
        {
            return new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<City, CityDTO>()
                        .ForMember(dest => dest.Country, opt => opt.MapFrom(
                                source => new CountryService(_unitOfWork).ReadById(source.CountryId).Result
                            ))
                )).Map<CityDTO>(await _unitOfWork.Cities.DeleteById(id));
        }

        public async Task<ICollection<CityDTO>> ReadAll()
        {
            return new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<City, CityDTO>()
                        .ForMember(dest => dest.Country, opt => opt.MapFrom(
                                source => new CountryService(_unitOfWork).ReadById(source.CountryId).Result
                            ))
                )).Map<ICollection<CityDTO>>(await _unitOfWork.Cities.ReadAll());
        }

        public async Task<CityDTO> ReadById(int id)
        {
            if (!_unitOfWork.Cities.HasAnyItem(id).Result)
            {
                return null;
            }

            return new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<City, CityDTO>()
                        .ForMember(dest => dest.Country, opt => opt.MapFrom(
                                source => new CountryService(_unitOfWork).ReadById(source.CountryId).Result
                            ))
                )).Map<CityDTO>(await _unitOfWork.Cities.ReadById(id));
        }

        public async Task Update(CityDTO city)
        {
            await _unitOfWork.Cities.Update(new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<CityDTO, City>()
                        .ForMember(dest => dest.Country, opt => opt.MapFrom(
                                source => _unitOfWork.Countries.ReadById(city.Country.Id).Result
                            ))
                        .ForMember(dest => dest.CountryId, opt => opt.MapFrom(
                                source => source.Country.Id
                            ))
                )).Map<City>(city));
        }
    }
}
