using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingPlatform.Models.Project
{
    public class ProjectCreateModel
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public byte[] MainPhoto { get; set; }

        public DateTime StartFundraisingDate { get; set; }

        public DateTime FinalFundraisingDate { get; set; }

        public decimal CurrentDonationSum { get; set; }

        public decimal TotalDonationSum { get; set; }
    }
}
