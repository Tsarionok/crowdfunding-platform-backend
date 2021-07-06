using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class DonationHistoryDTO : BaseDTO<int>
    {
        [Required]
        public UserDTO User { get; set; }

        [Required]
        public ProjectDTO Project { get; set; }

        [Range(1, 5000)]
        public decimal DonationSum { get; set; }

        [MaxLength(800)]
        public string Message { get; set; }
    }
}
