using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingPlatform.Models.User
{
    public class UserChangePasswordModel : BaseModel
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
