using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingPlatform.Models.Donation
{
    public class DonateModel
    {
        public string UserId { get; set; }

        public int ProjectId { get; set; }

        public decimal Amount { get; set; }

        public string Message { get; set; }
    }
}
