using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingPlatform.Models.Project
{
    public class ProjectOwnerModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public bool IsOwner { get; set; }
    }
}
