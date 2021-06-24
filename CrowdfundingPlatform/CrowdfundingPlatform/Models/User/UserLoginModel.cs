using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingPlatform.Models.User
{
    public class UserLoginModel : BaseModel
    {
        public string Email { get; set; }

        public string EncryptedPassword { get; set; }
    }
}
