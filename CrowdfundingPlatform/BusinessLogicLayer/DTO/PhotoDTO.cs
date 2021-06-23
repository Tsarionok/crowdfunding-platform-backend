using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class PhotoDTO : BaseDTO
    {
        public ProjectDTO Project { get; set; }

        public byte[] Image { get; set; }
    }
}
