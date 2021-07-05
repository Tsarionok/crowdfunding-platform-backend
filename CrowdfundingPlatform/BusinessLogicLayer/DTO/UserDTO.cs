using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class UserDTO : BaseDTO<string>
    {
        public string Email { get; set; }

        public byte[] Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public Sex? Sex { get; set; }

        public string Phone { get; set; }

        public string EncryptedPassword { get; set; }

        public bool IsTwoFactorAuthenticationEnabled { get; set; } = false;

        public CityDTO City { get; set; }
    }
}
