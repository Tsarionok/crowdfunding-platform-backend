using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class DonationHistory : BaseEntity
    {
        public User User { get; set; }

        public Project Project { get; set; }

        public decimal DonationSum { get; set; }

        public string Message { get; set; }
    }
}
