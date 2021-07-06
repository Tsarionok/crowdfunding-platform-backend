using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdfundingPlatform.Models.Project
{
    public class ProjectEvaluationModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Range(1, 5)]
        public int Evaluation { get; set; }
    }
}
