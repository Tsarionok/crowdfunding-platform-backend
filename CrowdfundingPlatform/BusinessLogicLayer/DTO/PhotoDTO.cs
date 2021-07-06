using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class PhotoDTO : BaseDTO<int>
    {
        [Required]
        public ProjectDTO Project { get; set; }

        public byte[] Image { get; set; }
    }
}
