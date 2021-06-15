using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class UserProject : BaseEntity
    {
        public User User { get; set; } 

        public Project Project { get; set; }

        public bool IsFavourites { get; set; }

        public int Evaluation { get; set; }

        public bool IsOwner { get; set; }
    }
}
