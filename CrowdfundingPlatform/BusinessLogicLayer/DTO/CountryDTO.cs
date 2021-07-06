using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class CountryDTO : BaseDTO<int>
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public ICollection<CityDTO> Cities { get; set; }
    }
}
