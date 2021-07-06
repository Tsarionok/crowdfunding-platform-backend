using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingPlatform.Models.User
{
    public class UserAvatarModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public byte[] Avatar { get; set; }
    }
}
