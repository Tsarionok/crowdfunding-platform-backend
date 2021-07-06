using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;

namespace BusinessLogicLayer.Service
{
    public interface IDonationHistoryService : ICrudService<DonationHistoryDTO, int>
    {
        public Task<ICollection<DonationHistoryDTO>> ReadAllByUserId(string userId);

        public Task Donate(DonationHistoryDTO donationHistory);
    }
}
