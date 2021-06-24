using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfundingPlatform.Models.City;

namespace CrowdfundingPlatform.Models.Country
{
    public class CountryWithCitiesModel : CountryModel
    {
        public ICollection<CityModel> Cities { get; set; }
    }
}
