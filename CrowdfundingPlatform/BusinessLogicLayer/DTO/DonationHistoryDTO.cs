using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class DonationHistoryDTO : BaseDTO
    {
        public UserDTO User { get; set; }

        public ProjectDTO Project { get; set; }

        public decimal DonationSum { get; set; }

        public string Message { get; set; }
    }
}
