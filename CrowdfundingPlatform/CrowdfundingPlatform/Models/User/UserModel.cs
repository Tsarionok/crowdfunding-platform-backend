using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfundingPlatform.Models.City;

namespace CrowdfundingPlatform.Models.User
{
    public class UserModel : BaseModel<string>
    {
        public string Email { get; set; }

        public byte[] Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public Sex Sex { get; set; }

        public string Phone { get; set; }

        public bool IsTwoFactorAuthenticationEnabled { get; set; }

        public CityWithCountryModel City { get; set; }
    }
}
