using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingPlatform.Models.User
{
    public class UserLoginModel : BaseModel<string>
    {
        [Required]
        public string Email { get; set; }

        public string Token { get; set; }
    }
}
