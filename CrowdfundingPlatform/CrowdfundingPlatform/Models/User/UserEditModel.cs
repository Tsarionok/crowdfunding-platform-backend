using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingPlatform.Models.User
{
    public class UserEditModel : BaseModel
    {
        public string Email { get; set; }

        public byte[] Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public Sex Sex { get; set; }

        public string Phone { get; set; }

        public int CityId { get; set; }
    }
}
