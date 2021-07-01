using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingPlatform.Models.User
{
    public class UserRegistrationModel
    {
        public string Id { get; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
