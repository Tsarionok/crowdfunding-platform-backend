using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class CityDTO : BaseDTO
    {
        public string Name { get; set; }

        public CountryDTO Country { get; set; }
    }
}
