using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class DonationHistory : BaseEntity
    {
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [Range(1, 5000)]
        public decimal DonationSum { get; set; }

        [MaxLength(800)]
        public string Message { get; set; }
    }
}
