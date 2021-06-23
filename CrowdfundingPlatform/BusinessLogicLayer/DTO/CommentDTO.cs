using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class CommentDTO : BaseDTO
    {
        public UserDTO User { get; set; }

        public ProjectDTO Project { get; set; }

        public string Message { get; set; }
    }
}
