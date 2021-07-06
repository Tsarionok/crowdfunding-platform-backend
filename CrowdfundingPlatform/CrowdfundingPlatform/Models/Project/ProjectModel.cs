using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfundingPlatform.Models.Category;

namespace CrowdfundingPlatform.Models.Project
{
    public class ProjectModel : BaseModel<int>
    {
        public string Name { get; set; }

        public CategoryModel Category { get; set; }

        public string Description { get; set; }

        public byte[] MainPhoto { get; set; }

        public DateTime StartFundraisingDate { get; set; }

        public DateTime FinalFundraisingDate { get; set; }

        public decimal CurrentDonationSum { get; set; }

        public decimal TotalDonationSum { get; set; }
    }
}
