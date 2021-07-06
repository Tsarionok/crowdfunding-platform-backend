using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingPlatform.Models.Project
{
    public class ProjectFavouritesModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public bool IsFavourites { get; set; }
    }
}
