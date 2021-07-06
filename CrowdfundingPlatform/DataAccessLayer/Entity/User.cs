using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Entity
{
    public class User : IdentityUser
    {

        public byte[] Avatar { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public Sex? Sex { get; set; }

        public int? CityId { get; set; }

        public City City { get; set; }
    }
}
