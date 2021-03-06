using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class ProjectDTO : BaseDTO
    {
        public CategoryDTO Category { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] MainPhoto { get; set; }

        public DateTime StartFundraisingDate { get; set; }

        public DateTime FinalFundraisingDate { get; set; }

        public decimal CurrentDonationSum { get; set; }

        public decimal TotalDonationSum { get; set; }
    }
}
