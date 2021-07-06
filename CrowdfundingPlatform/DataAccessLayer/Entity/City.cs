using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class City : BaseEntity
    {
        public int CountryId { get; set; }
        public Country Country { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
