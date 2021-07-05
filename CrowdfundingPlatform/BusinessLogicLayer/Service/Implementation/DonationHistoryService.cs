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
    public class DonationHistoryService : BaseService, IDonationHistoryService
    {
        public DonationHistoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task Create(DonationHistoryDTO donationHistory)
        {
            await _unitOfWork.DonationHistories.Create(new DonationHistory 
            {
                DonationSum = donationHistory.DonationSum,
                Message = donationHistory.Message,
                Project = _unitOfWork.Projects.ReadById(donationHistory.Project.Id).Result,
                User = _unitOfWork.Users.ReadById(donationHistory.User.Id).Result
            });
        }

        public async Task<DonationHistoryDTO> DeleteById(int id)
        {
            DonationHistory deletedDonationHitory = _unitOfWork.DonationHistories.DeleteById(id).Result;

            return await Task.Run(() => new DonationHistoryDTO 
            {
                Id = deletedDonationHitory.Id,
                Message = deletedDonationHitory.Message,
                Project = new ProjectService(_unitOfWork).ReadById(deletedDonationHitory.Project.Id).Result,
                User = new UserService(_unitOfWork).ReadById(deletedDonationHitory.User.Id).Result
            });
        }

        public async Task<ICollection<DonationHistoryDTO>> ReadAll()
        {
            ICollection<DonationHistoryDTO> donationHistories = new List<DonationHistoryDTO>();

            foreach (DonationHistory readableDonationHistory in _unitOfWork.DonationHistories.ReadAll().Result)
            {
                donationHistories.Add(new DonationHistoryDTO
                {
                    Id = readableDonationHistory.Id,
                    Message = readableDonationHistory.Message,
                    Project = new ProjectService(_unitOfWork).ReadById(readableDonationHistory.Project.Id).Result,
                    User = new UserService(_unitOfWork).ReadById(readableDonationHistory.User.Id).Result
                });
            }
            return await Task.Run(() => donationHistories);
        }

        public async Task<DonationHistoryDTO> ReadById(int id)
        {
            DonationHistory readableDonationHistory = _unitOfWork.DonationHistories.ReadById(id).Result;

            return await Task.Run(() => new DonationHistoryDTO
            {
                Id = readableDonationHistory.Id,
                Message = readableDonationHistory.Message,
                Project = new ProjectService(_unitOfWork).ReadById(readableDonationHistory.Project.Id).Result,
                User = new UserService(_unitOfWork).ReadById(readableDonationHistory.User.Id).Result
            });
        }

        public async Task Update(DonationHistoryDTO donationHistory)
        {
            await _unitOfWork.DonationHistories.Update(new DonationHistory
            {
                Id = donationHistory.Id,
                DonationSum = donationHistory.DonationSum,
                Message = donationHistory.Message,
                Project = _unitOfWork.Projects.ReadById(donationHistory.Project.Id).Result,
                User = _unitOfWork.Users.ReadById(donationHistory.User.Id).Result
            });
        }

        public async Task Donate(DonationHistoryDTO donate)
        {
            // TODO: make a semblance of a transaction

            Project project = _unitOfWork.Projects.ReadById(donate.Project.Id).Result;

            project.CurrentDonationSum += donate.DonationSum;

            await _unitOfWork.Projects.Update(project);

            await _unitOfWork.DonationHistories.Create(new Mapper(
                    new MapperConfiguration(cfg => cfg.CreateMap<DonationHistoryDTO, DonationHistory>()
                            .ForMember(dest => dest.Project, opt => opt.MapFrom(
                                    source => _unitOfWork.Projects.ReadById(donate.Project.Id).Result
                                ))
                            .ForMember(dest => dest.User, opt => opt.MapFrom(
                                    source => _unitOfWork.Users.ReadById(donate.User.Id).Result
                                ))
                        ))
                .Map<DonationHistory>(donate));
        }
    }
}
