using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfundingPlatform.Models.Country;

namespace CrowdfundingPlatform.Models.City
{
    public class CityWithCountryModel : CityModel
    {
        public CountryModel Country { get; set; }
    }
}
