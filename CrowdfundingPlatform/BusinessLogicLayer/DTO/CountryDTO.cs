using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class CountryDTO : BaseDTO<int>
    {
        public string Name { get; set; }

        public ICollection<CityDTO> Cities { get; set; }
    }
}
