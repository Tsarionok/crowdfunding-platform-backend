using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class UserDTO : BaseDTO<string>
    {
        [MaxLength(200)]
        public string Email { get; set; }

        public byte[] Avatar { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string FirstName { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public Sex? Sex { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\+\d{3})(\d{2})(\d{3})(\d{2})(\d{2})$")]
        public string Phone { get; set; }

        public string EncryptedPassword { get; set; }

        public bool IsTwoFactorAuthenticationEnabled { get; set; } = false;

        public CityDTO City { get; set; }
    }
}
