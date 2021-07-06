using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingPlatform.Models.Donation
{
    public class DonationHistoryModel
    {
        [Required]
        public decimal Amount { get; set; }

        public string Message { get; set; }
    }
}
