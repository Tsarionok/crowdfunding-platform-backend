using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class Comment : BaseEntity
    {
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [MaxLength(800)]
        public string Message { get; set; }
    }
}
