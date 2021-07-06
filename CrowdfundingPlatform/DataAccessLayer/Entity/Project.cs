using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class Project : BaseEntity
    {
        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(3000)]
        public string Description { get; set; }

        public byte[] MainPhoto { get; set; }

        public DateTime? StartFundraisingDate { get; set; }

        public DateTime? FinalFundraisingDate { get; set; }

        public decimal CurrentDonationSum { get; set; } = 0;

        [Range(0, 1_000_000)]
        public decimal? TotalDonationSum { get; set; }
    }
}
