using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            City createdCity = new City()
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.Country.Id,
                Country = _unitOfWork.Countries.ReadById(city.Country.Id).Result
            };

            User createdUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar,
                Email = user.Email,
                Phone = user.Phone,
                Sex = sex,
                BirthDate = user.BirthDate,
                CityId = createdCity,
                EncryptedPassword = user.EncryptedPassword,
                IsTwoFactorAuthenticationEnabled = user.IsTwoFactorAuthenticationEnabled
            };
            await _unitOfWork.Users.Create(createdUser);
        }

        public async Task<UserDTO> DeleteById(int id)
        {
            User user = _unitOfWork.Users.ReadById(id).Result;
            DTO.Sex sex;
            switch (user.Sex)
            {
                case DataAccessLayer.Entity.Sex.Man:
                    {
                        sex = DTO.Sex.Man;
                        break;
                    }
                case DataAccessLayer.Entity.Sex.Woman:
                    {
                        sex = DTO.Sex.Woman;
                        break;
                    }
                default:
                    {
                        sex = DTO.Sex.Undefined;
                        break;
                    }
            }

            City city = _unitOfWork.Cities.ReadById(user.CityId.Id).Result;

            CityDTO createdCity = new CityDTO()
            {
                Id = city.Id,
                Name = city.Name,
                Country = new CountryService(_unitOfWork).ReadById(city.Country.Id).Result
            };

            UserDTO deletedUser = new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar,
                Email = user.Email,
                Phone = user.Phone,
                Sex = sex,
                BirthDate = user.BirthDate,
                City = createdCity,
                EncryptedPassword = user.EncryptedPassword,
                IsTwoFactorAuthenticationEnabled = user.IsTwoFactorAuthenticationEnabled
            };
            return await Task.Run(() => deletedUser);
        }

        public async Task<ICollection<UserDTO>> ReadAll()
        {
            ICollection<UserDTO> users = new List<UserDTO>();
            foreach (User readUser in _unitOfWork.Users.ReadAll().Result)
            {
                DTO.Sex sex;
                switch (readUser.Sex)
                {
                    case DataAccessLayer.Entity.Sex.Man:
                        {
                            sex = DTO.Sex.Man;
                            break;
                        }
                    case DataAccessLayer.Entity.Sex.Woman:
                        {
                            sex = DTO.Sex.Woman;
                            break;
                        }
                    default:
                        {
                            sex = DTO.Sex.Undefined;
                            break;
                        }
                }

                City city = _unitOfWork.Cities.ReadById(readUser.CityId.Id).Result;

                CityDTO createdCity = new CityDTO()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Country = new CountryService(_unitOfWork).ReadById(city.Country.Id).Result
                };

                users.Add(new UserDTO
                {
                    FirstName = readUser.FirstName,
                    LastName = readUser.LastName,
                    Avatar = readUser.Avatar,
                    Email = readUser.Email,
                    Phone = readUser.Phone,
                    Sex = sex,
                    BirthDate = readUser.BirthDate,
                    City = createdCity,
                    EncryptedPassword = readUser.EncryptedPassword,
                    IsTwoFactorAuthenticationEnabled = readUser.IsTwoFactorAuthenticationEnabled
                });
            }

            return await Task.Run(() => users);
        }

        public Task<UserDTO> ReadById(int id)
        {
            User user = _unitOfWork.Users.ReadById(id).Result;
            DTO.Sex sex;
            switch (user.Sex)
            {
                case DataAccessLayer.Entity.Sex.Man:
                    {
                        sex = DTO.Sex.Man;
                        break;
                    }
                case DataAccessLayer.Entity.Sex.Woman:
                    {
                        sex = DTO.Sex.Woman;
                        break;
                    }
                default:
                    {
                        sex = DTO.Sex.Undefined;
                        break;
                    }
            }

            City city = _unitOfWork.Cities.ReadById(user.CityId.Id).Result;

            CityDTO createdCity = new CityDTO()
            {
                Id = city.Id,
                Name = city.Name,
                Country = new CountryService(_unitOfWork).ReadById(city.Country.Id).Result
            };

            return Task.Run(() => new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar,
                Email = user.Email,
                Phone = user.Phone,
                Sex = sex,
                BirthDate = user.BirthDate,
                City = createdCity,
                EncryptedPassword = user.EncryptedPassword,
                IsTwoFactorAuthenticationEnabled = user.IsTwoFactorAuthenticationEnabled
            });
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
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar,
                Email = user.Email,
                Phone = user.Phone,
                Sex = sex,
                BirthDate = user.BirthDate,
                CityId = updatedCity,
                EncryptedPassword = user.EncryptedPassword,
                IsTwoFactorAuthenticationEnabled = user.IsTwoFactorAuthenticationEnabled
            };
            await _unitOfWork.Users.Update(updatedUser);
        }
    }
}
