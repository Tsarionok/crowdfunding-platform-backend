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
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task Create(UserDTO user)
        {
            User createdUser = new Mapper(new MapperConfiguration(
                cfg => cfg.CreateMap<UserDTO, User>()
                    .ForMember(dest => dest.CityId, opt => opt.MapFrom(source => source.City.Id))
                    .ForMember(dest => dest.City, opt => opt.MapFrom(source =>
                            new Mapper(new MapperConfiguration(
                                cfg => cfg.CreateMap<CityDTO, City>()
                                    .ForMember(dest => dest.Country, opt => opt.MapFrom(source =>
                                        new Mapper(new MapperConfiguration(
                                                cfg => cfg.CreateMap<CountryDTO, Country>()
                                            )).Map<Country>(source.Country)
                                        ))
                            )).Map<City>(source.City)))
                )).Map<User>(user);

            await _unitOfWork.Users.Create(createdUser);
        }

        public async Task<UserDTO> DeleteById(string id)
        {
            UserDTO deletedUser = new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<User, UserDTO>()
                    .ForMember(dest => dest.City, opt => opt.MapFrom(source =>
                            new Mapper(new MapperConfiguration(
                                cfg => cfg.CreateMap<City, CityDTO>()
                                    .ForMember(dest => dest.Country, opt => opt.MapFrom(source =>
                                        new Mapper(new MapperConfiguration(
                                                cfg => cfg.CreateMap<Country, CountryDTO>()
                                                    .ForMember(dest => dest.Cities, opt => opt.Ignore())
                                            )).Map<CountryDTO>(source.Country)
                                        ))
                            )).Map<CityDTO>(source.City)))
                )).Map<UserDTO>(await _unitOfWork.Users.DeleteById(id));
            return await Task.Run(() => deletedUser);
        }

        public async Task<ICollection<UserDTO>> ReadAll()
        {
            ICollection<UserDTO> users = new Mapper(new MapperConfiguration(
                cfg => cfg.CreateMap<User, UserDTO>()
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(
                            source => source.UserName
                        ))
                    .ForMember(dest => dest.Sex, opt => opt.MapFrom(
                            source => source.Sex
                        ))
                    .ForMember(dest => dest.Phone, opt => opt.MapFrom(
                            source => source.PhoneNumber
                        ))
                    .ForMember(dest => dest.EncryptedPassword, opt => opt.Ignore()
                        )
                    .ForMember(dest => dest.IsTwoFactorAuthenticationEnabled, opt => opt.MapFrom(
                            source => source.TwoFactorEnabled
                        ))
                    .ForMember(dest => dest.City, opt => opt.MapFrom(source =>
                            new Mapper(new MapperConfiguration(
                                cfg => cfg.CreateMap<City, CityDTO>()
                                    .ForMember(dest => dest.Country, opt => opt.MapFrom(source =>
                                        new Mapper(new MapperConfiguration(
                                                cfg => cfg.CreateMap<Country, CountryDTO>()
                                                    .ForMember(dest => dest.Cities, opt => opt.Ignore())
                                            )).Map<CountryDTO>(source.Country)
                                        ))
                            )).Map<CityDTO>(source.City)))
                )).Map<List<UserDTO>>(await _unitOfWork.Users.ReadAll());

            return await Task.Run(() => users);
        }

        public async Task<UserDTO> ReadById(string id)
        {
            UserDTO user = new Mapper(new MapperConfiguration(
                cfg => cfg.CreateMap<User, UserDTO>()
                    .ForMember(dest => dest.City, opt => opt.MapFrom(source =>
                            new Mapper(new MapperConfiguration(
                                cfg => cfg.CreateMap<City, CityDTO>()
                                    .ForMember(dest => dest.Country, opt => opt.MapFrom(source =>
                                        new Mapper(new MapperConfiguration(
                                                cfg => cfg.CreateMap<Country, CountryDTO>()
                                                    .ForMember(dest => dest.Cities, opt => opt.Ignore())
                                            )).Map<CountryDTO>(source.Country)
                                        ))
                            )).Map<CityDTO>(source.City)))
                )).Map<UserDTO>(await _unitOfWork.Users.ReadById(id));
            return await Task.Run(() => user);
        }

        public async Task Update(UserDTO user)
        {
            DataAccessLayer.Entity.Sex sex;
            switch (user.Sex)
            {
                case DTO.Sex.Man:
                    {
                        sex = DataAccessLayer.Entity.Sex.Man;
                        break;
                    }
                case DTO.Sex.Woman:
                    {
                        sex = DataAccessLayer.Entity.Sex.Woman;
                        break;
                    }
                default:
                    {
                        sex = DataAccessLayer.Entity.Sex.Undefined;
                        break;
                    }
            }

            CityDTO city = new CityService(_unitOfWork).ReadById(user.City.Id).Result;

            City updatedCity = new City()
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.Country.Id,
                Country = _unitOfWork.Countries.ReadById(city.Country.Id).Result
            };

            User updatedUser = new User
            {
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar,
                Email = user.Email,
                Sex = sex,
                BirthDate = user.BirthDate,
                City = updatedCity
            };
            await _unitOfWork.Users.Update(updatedUser);
        }

        public async Task<UserDTO> ReadByEmail(string Email)
        {
            return new Mapper(new MapperConfiguration(
                    cfg => cfg.CreateMap<User, UserDTO>()
                        .ForMember(dest => dest.City, opt => opt.MapFrom(source =>
                            new Mapper(new MapperConfiguration(
                                cfg => cfg.CreateMap<City, CityDTO>()
                                    .ForMember(dest => dest.Country, opt => opt.MapFrom(source =>
                                        new Mapper(new MapperConfiguration(
                                                cfg => cfg.CreateMap<Country, CountryDTO>()
                                                    .ForMember(dest => dest.Cities, opt => opt.Ignore())
                                            )).Map<CountryDTO>(source.Country)
                                        ))
                            )).Map<CityDTO>(source.City)))
                )).Map<UserDTO>(await _unitOfWork.Users.ReadByEmail(Email));
        }

        public async Task UploadAvatar(UserDTO user)
        {
            User updatedUser = new Mapper(new MapperConfiguration(
                cfg => cfg.CreateMap<UserDTO, User>()
                    .ForMember(dest => dest.CityId, opt => opt.MapFrom(source => source.City.Id))
                    .ForMember(dest => dest.City, opt => opt.MapFrom(source =>
                            new Mapper(new MapperConfiguration(
                                cfg => cfg.CreateMap<CityDTO, City>()
                                    .ForMember(dest => dest.Country, opt => opt.MapFrom(source =>
                                        new Mapper(new MapperConfiguration(
                                                cfg => cfg.CreateMap<CountryDTO, Country>()
                                            )).Map<Country>(source.Country)
                                        ))
                            )).Map<City>(source.City)))
                )).Map<User>(user);

            await _unitOfWork.Users.Update(updatedUser);
        }
    }
}
