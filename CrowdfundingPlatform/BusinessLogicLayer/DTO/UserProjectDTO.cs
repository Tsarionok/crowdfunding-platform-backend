using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class UserProjectDTO : BaseDTO
    {
        public UserDTO User { get; set; }

        public ProjectDTO Project { get; set; }

        public bool IsFavourites { get; set; }

        public int Evaluation { get; set; }

        public bool IsOwner { get; set; }
    }
}
