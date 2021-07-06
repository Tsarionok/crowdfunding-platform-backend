using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class Country : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
