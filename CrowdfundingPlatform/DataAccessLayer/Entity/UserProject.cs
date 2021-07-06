using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class UserProject : BaseEntity
    {
        [Required]
        public string UserId { get; set; }
        public User User { get; set; } 

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public bool IsFavourites { get; set; } = false;

        [Range(1, 5)]
        public int? Evaluation { get; set; }

        public bool IsOwner { get; set; } = false;
    }
}
